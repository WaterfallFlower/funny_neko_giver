using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using funny_neko_giver.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace funny_neko_giver
{
    internal class FileDescription
    {
        [JsonProperty("artist_href")] public string ArtistHref { get; set; }
        [JsonProperty("artist_name")] public string ArtistName { get; set; }
        [JsonProperty("source_url")] public string SourceUrl { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
    }

    internal class ResultsFileDescription
    {
        [JsonProperty("results")] public IEnumerable<FileDescription> Results;
    }
    
    public class NekosBestApiProvider : ImageApiDescription {

        public NekosBestApiProvider()
        {
            Name = "NEKOS.BEST (v2)";
            UrlSimple = "https://nekos.best/";
        }
        
        public override IImageProviderApi CreateInstance()
        {
            return new NekosBestApi();
        }
    }

    public class NekosBestApi : IImageProviderApi
    {
        private readonly HttpClient _localHttpClient = new HttpClient();
        private IEnumerable<CategoryImage> _categoryList;

        public async void Init(Action<string> onError, Action<IImageProviderApi> onSuccess)
        {
            var cancelOperation = new CancellationTokenSource();
            _categoryList = await BuildCategoryList(cancelOperation);
            if (cancelOperation.IsCancellationRequested)
            {
                onError(null); //TODO: Remove this
            }
            else
            {
                onSuccess(this);
            }
        }

        public async void LoadCategoryImage(
            CategoryImage category, int amount,
            Action<string> onError, Action<ResultImage> onSuccess,
            Action<string> doProgress, Action onFinal
        )
        {
            var c = new CancellationTokenSource();
            doProgress(Resources.progress_connectapi);
            var message = await GetMessageAsync(c, $"https://nekos.best/api/v2/{category.Name}?amount={amount}");
            if (c.IsCancellationRequested)
            {
                onError(Resources.error_accessapi);
                return;
            }

            doProgress(Resources.progress_fetching);
            var listDescription = JsonConvert.DeserializeObject<ResultsFileDescription>(message);
            var i = 1;
            var k = listDescription.Results.Count();
            foreach (var description in listDescription.Results)
            {
                doProgress(string.Format(Resources.progress_downloadimage, i, k));
                i++;

                /* Description Name */
                var idx = description.Url.LastIndexOf('/');
                var imageName =idx != -1 ? description.Url.Substring(idx + 1).Split('.')[0] : description.Url;
                Image imageItself = null;

                var response = await _localHttpClient.GetAsync(description.Url);
                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        imageItself = Image.FromStream(stream);
                    }
                }
                else
                {
                    onError(Resources.error_downloadimage);
                }
                
                onSuccess(new ResultImage
                {
                    ImageName = imageName,
                    ImageItself = imageItself,
                    FormattedDescription = string.Format(Resources.result_search_filled, description.ArtistName, description.ArtistName,description.SourceUrl)
                });
            }

            onFinal();
        }

        public IEnumerable<CategoryImage> GetCategories()
        {
            return _categoryList;
        }

        private async Task<IEnumerable<CategoryImage>> BuildCategoryList(CancellationTokenSource c)
        {
            var message = await GetMessageAsync(c, "https://nekos.best/api/v2/endpoints");
            if (c.IsCancellationRequested)
            {
                MessageBox.Show(message, Resources.dialog_messages_error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            var images = JObject.Parse(message).Properties().Select(v => new CategoryImage
                { Name = v.Name, Type = v.Value["format"].ToObject<string>() });
            return images;
        }

        private async Task<string> GetMessageAsync(CancellationTokenSource c, string uri)
        {
            var response = await _localHttpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            c?.Cancel();
            return $"Error accessing: {response.ReasonPhrase}";
        }
    }
}
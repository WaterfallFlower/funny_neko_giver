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

namespace funny_neko_giver.ImageApi
{
    internal class FileDescription
    {
        [JsonProperty("artist_href")] public string ArtistHref { get; set; }
        [JsonProperty("artist_name")] public string ArtistName { get; set; }
        [JsonProperty("source_url")] public string SourceUrl { get; set; }
        [JsonProperty("anime_name")] public string AnimeName { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
    }

    internal class ResponseResultList
    {
        [JsonProperty("results")] public IEnumerable<FileDescription> Results;
    }

    public class NekosBestApiProvider : ApiDescription
    {
        public NekosBestApiProvider()
        {
            Name = "Nekos.best (v2)";
            UrlSimple = "https://nekos.best/";
        }

        public override IImageProviderApi CreateInstance()
        {
            return new NekosBestApi();
        }
    }

    public class NekosBestApi : IImageProviderApi
    {
        private IEnumerable<CategoryImage> _categoryList;
        private HttpClient _localHttpClient;

        public async void Init(HttpClient client, Action<string> onError, Action<IImageProviderApi> onSuccess)
        {
            _localHttpClient = client;
            var token = new CancellationTokenSource();
            _categoryList = await BuildCategoryList(token);
            if (token.IsCancellationRequested)
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
            Action<string> onError, Action<ResultImage> pushReadyImage,
            Action<string> callProgressBar, Action onFinal
        )
        {
            var token = new CancellationTokenSource();
            callProgressBar(Resources.progress_connectapi);
            var message = await GeneralAccess.GetMessageAsync(token, _localHttpClient,
                $"https://nekos.best/api/v2/{category.Name}?amount={amount}");
            if (token.IsCancellationRequested)
            {
                onError(Resources.error_accessapi);
                return;
            }

            callProgressBar(Resources.progress_fetching);
            var listResults = JsonConvert.DeserializeObject<ResponseResultList>(message);
            
            var i = 1;
            var k = listResults.Results.Count();
            
            foreach (var description in listResults.Results)
            {
                callProgressBar(string.Format(Resources.progress_downloadimage, i++, k));

                Image image;
                var response = await _localHttpClient.GetAsync(description.Url);
                
                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        image = Image.FromStream(stream);
                        stream.Dispose();
                        stream.Close();
                    }
                }
                else
                {
                    onError(Resources.error_downloadimage);
                    continue;
                }
                
                pushReadyImage(new ResultImage
                {
                    ImageName = GeneralAccess.GetNameFromImageUrl(description.Url),
                    ImageItself = image,
                    SourceUrl = description.Url,
                    NeedAnimation = description.Url.EndsWith(".gif"),
                    FormattedDescription = GeneralAccess.GetAllPropertiesList(description)
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
            var message =
                await GeneralAccess.GetMessageAsync(c, _localHttpClient, "https://nekos.best/api/v2/endpoints");
            if (c.IsCancellationRequested)
            {
                MessageBox.Show(message, Resources.dialog_messages_error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            var images = JObject.Parse(message).Properties().Select(v => new CategoryImage
                { Name = v.Name, Type = v.Value["format"].ToObject<string>().ToUpper() });
            return images;
        }
    }
}
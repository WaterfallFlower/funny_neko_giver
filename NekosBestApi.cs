using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        [JsonProperty("anime_name")] public string AnimeName { get; set; }
        [JsonProperty("url")] public string Url { get; set; }
    }

    internal class ResultsFileDescription
    {
        [JsonProperty("results")] public IEnumerable<FileDescription> Results;
    }

    public class NekosBestApiProvider : ImageApiDescription
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
            var message = await GeneralAccess.GetMessageAsync(c, _localHttpClient,
                $"https://nekos.best/api/v2/{category.Name}?amount={amount}");
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
                var imageName = idx != -1 ? description.Url.Substring(idx + 1).Split('.')[0] : description.Url;
                Image imageItself = null;

                var response = await _localHttpClient.GetAsync(description.Url);
                if (response.IsSuccessStatusCode)
                {
                    var imageData = await response.Content.ReadAsByteArrayAsync();
                    using (var stream = new MemoryStream(imageData))
                    {
                        imageItself = Image.FromStream(stream);
                    }
                }
                else
                {
                    onError(Resources.error_downloadimage);
                }

                var builder = new StringBuilder();
                if (!string.IsNullOrEmpty(description.AnimeName))
                    builder.Append("Anime Name: ").Append(description.AnimeName).Append("\n");
                if (!string.IsNullOrEmpty(description.ArtistName))
                    builder.Append("Author: ").Append(description.ArtistName).Append("\n");
                if (!string.IsNullOrEmpty(description.ArtistHref))
                    builder.Append("Author URL: ").Append(description.ArtistHref).Append("\n");
                if (!string.IsNullOrEmpty(description.SourceUrl))
                    builder.Append("Source URL: ").Append(description.ArtistHref).Append("\n");


                onSuccess(new ResultImage
                {
                    ImageName = imageName,
                    ImageItself = imageItself,
                    SourceUrl = description.Url,
                    NeedAnimation = description.Url.EndsWith(".gif"),
                    FormattedDescription = builder.ToString()
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
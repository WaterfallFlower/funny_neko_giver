using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace funny_neko_giver
{
    public class CategoryImage
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Type})";
        }
    }

    public class FileDescription
    {
        [JsonProperty("artist_href")] public string ArtistHref { get; set; }
        [JsonProperty("artist_name")] public string ArtistName { get; set; }
        [JsonProperty("source_url")] public string SourceUrl { get; set; }
        [JsonProperty("url")] public string Url { get; set; }

        [JsonIgnore] public Image ImageItself { get; set; }
        [JsonIgnore] public string DescriptionName { get; set; }

        public override string ToString()
        {
            return DescriptionName;
        }
    }

    public class ResultsFileDescription
    {
        [JsonProperty("results")] public IEnumerable<FileDescription> Results;
    }

    public class NekoAccess
    {
        private readonly HttpClient _localHttpClient = new HttpClient();
        private IEnumerable<CategoryImage> _categoryList;

        public async void Init(Action<string> onError, Action<NekoAccess> onSuccess)
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
            Action<string> onError, Action<FileDescription> onSuccess,
            Action<string> doProgress, Action onFinal
        )
        {
            var c = new CancellationTokenSource();
            doProgress("Accessing API...");
            var message = await GetMessageAsync(c, $"https://nekos.best/api/v2/{category.Name}?amount={amount}");
            if (c.IsCancellationRequested)
            {
                onError("Error while accessing API...");
                return;
            }

            doProgress("Fetching results...");
            var list_description = JsonConvert.DeserializeObject<ResultsFileDescription>(message);
            int i = 1;
            int k = list_description.Results.Count();
            foreach (var description in list_description.Results)
            {
                doProgress($"Downloading an image... {i}/{k}");
                i++;
                //Description Name
                int idx = description.Url.LastIndexOf('/');
                description.DescriptionName =
                    idx != -1 ? description.Url.Substring(idx + 1).Split('.')[0] : description.Url;

                var response = await _localHttpClient.GetAsync(description.Url);
                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        description.ImageItself = Image.FromStream(stream);
                    }
                }
                else
                {
                    onError("Error while downloading image...");
                }

                onSuccess(description);
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
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
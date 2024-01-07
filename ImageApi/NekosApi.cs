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
using ImageMagick;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace funny_neko_giver.ImageApi
{
    internal class UnpackedResponse
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("id_v2")] public string IdV2 { get; set; }
        [JsonProperty("image_url")] public string ImageUrl { get; set; }
        [JsonProperty("sample_url")] public string SampleUrl { get; set; }
        [JsonProperty("source")] public string Source { get; set; }
        [JsonProperty("source_id")] public string SourceId { get; set; }
        [JsonProperty("rating")] public string Rating { get; set; }
        [JsonProperty("verification")] public string Verification { get; set; }
        [JsonProperty("hash_md5")] public string HasMd5 { get; set; }
        [JsonProperty("hash_perceptual")] public string HashPerceptual { get; set; }
        [JsonProperty("is_original")] public bool IsOriginal { get; set; }
        [JsonProperty("is_screenshot")] public bool IsScreenshot { get; set; }
        [JsonProperty("is_flagged")] public bool IsFlagged { get; set; }
        [JsonProperty("is_animated")] public bool IsAnimated { get; set; }
        [JsonProperty("artist")] public ArtistInformation Artist { get; set; }
        [JsonProperty("created_at")] public decimal CreatedAt { get; set; }
        [JsonProperty("updated_at")] public decimal UpdatedAt { get; set; }
    }

    internal class ArtistInformation
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("aliases")] public IEnumerable<string> Aliases { get; set; }
        [JsonProperty("image_url")] public string ImageUrl { get; set; }
        [JsonProperty("links")] public IEnumerable<string> Links { get; set; }
        [JsonProperty("policy_repost")] public string PolicyRepost { get; set; }
        [JsonProperty("policy_credit")] public bool PolicyCredit { get; set; }
        [JsonProperty("policy_ai")] public bool PolicyAi { get; set; }


        public override string ToString()
        {
            return "\n" + GeneralAccess.GetAllPropertiesList(this);
        }
    }
    
    public class NekosApiProvider : ApiDescription
    {
        public NekosApiProvider()
        {
            Name = "Nekos API (3.4.2)";
            UrlSimple = "https://nekosapi.com/";
        }

        public override IImageProviderApi CreateInstance()
        {
            return new NekosApi();
        }
    }

    public class NekosApi : IImageProviderApi
    {
        private IEnumerable<CategoryImage> _categoryList;
        private HttpClient _localHttpClient;

        public IEnumerable<CategoryImage> GetCategories()
        {
            return _categoryList;
        }

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
            Action<string> onError, Action<ResultImage> pushReadyImage,
            Action<string> callProgressBar, Action onFinal
        )
        {
            var token = new CancellationTokenSource();
            callProgressBar(Resources.progress_connectapi);
            var message = await GeneralAccess.GetMessageAsync(
                token, _localHttpClient,
                "https://api.nekosapi.com/v3/images/" +
                (category.Type == "rnd" ? $"random?limit={amount}" : $"tags/{category.Id}/images?limit={amount}")
            );
            if (token.IsCancellationRequested)
            {
                onError(Resources.error_accessapi);
                return;
            }

            callProgressBar(Resources.progress_fetching);

            var mainRequest = JObject.Parse(message);

            if (mainRequest["count"].ToObject<int>() == 0)
            {
                onError(Resources.error_emptycatalogue);
                return;
            }

            var callResults = mainRequest["items"].Select(o =>
                JsonConvert.DeserializeObject<UnpackedResponse>(o.ToString())).ToArray();
            var i = 1;
            var k = callResults.Length;
            foreach (var description in callResults)
            {
                callProgressBar(string.Format(Resources.progress_downloadimage, i++, k));

                var imageReady = await DownloadScaryFormatImage(token, description.ImageUrl);
                
                if (token.IsCancellationRequested)
                {
                    onError(Resources.error_downloadimage);
                }
                

                pushReadyImage(new ResultImage
                {
                    ImageName = GeneralAccess.GetNameFromImageUrl(description.ImageUrl),
                    ImageItself = imageReady,
                    SourceUrl = description.ImageUrl,
                    NeedAnimation = description.ImageUrl.EndsWith(".gif"),
                    FormattedDescription = GeneralAccess.GetAllPropertiesList(description)
                });
            }

            onFinal();
        }

        private async Task<IEnumerable<CategoryImage>> BuildCategoryList(CancellationTokenSource c)
        {
            var message = await GeneralAccess.GetMessageAsync(c, _localHttpClient, "https://api.nekosapi.com/v3/images/tags");
            if (c.IsCancellationRequested)
            {
                MessageBox.Show(message, Resources.dialog_messages_error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            var images = new List<CategoryImage> { new CategoryImage { Name = "Random Category", Type = "rnd" } };
            images.AddRange(JObject.Parse(message)["items"].Select(v => new CategoryImage
            {
                Name = v["name"].ToObject<string>(), Type = v["sub"].ToObject<string>(),
                IsSafe = !v["is_nsfw"].ToObject<bool>(), Id = v["id"].ToObject<int>()
            }));
            return images;
        }

        private async Task<Image> DownloadScaryFormatImage(CancellationTokenSource c, string url)
        {
            byte[] buffer;
            try
            {
                buffer = await _localHttpClient.GetByteArrayAsync(url);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error temp replace this TODO");
                c.Cancel();
                return null;
            }

            Bitmap bitmap;
            using (var magickImages = new MagickImageCollection(buffer))
            {
                var ms = new MemoryStream();
                await magickImages.WriteAsync(ms, magickImages.Count > 1 ? MagickFormat.Gif : MagickFormat.Png);
                bitmap = new Bitmap(ms);
                bitmap.Tag = ms;
            }

            return bitmap;
        }
    }
}
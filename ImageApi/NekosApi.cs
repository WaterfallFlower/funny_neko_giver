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
using Newtonsoft.Json.Linq;

namespace funny_neko_giver.ImageApi
{
    internal class ExtensiveCallResult
    {
        public int Id { get; set; }
        public string IdV2 { get; set; }
        public string ImageUrl { get; set; }
        public string SampleUrl { get; set; }
        
        public string Source { get; set; }
        public string SourceId { get; set; }
        public string Rating { get; set; }
        public string Verification { get; set; }
        public string HashMd5 { get; set; }
        public string HashPerceptual { get; set; }
        public bool IsOriginal { get; set; }
        public bool IsScreenshot { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsAnimated { get; set; }
        public string Artist { get; set; }
        
        public decimal CreatedAt { get; set; }
        public decimal UpdatedAt { get; set; }

        
    }
    
    public class NekosApiProvider : ImageApiDescription
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
            Action<string> onError, Action<ResultImage> onSuccess,
            Action<string> doProgress, Action onFinal
            )
        {
            var c = new CancellationTokenSource();
            doProgress(Resources.progress_connectapi);
            var message = await GeneralAccess.GetMessageAsync(c, _localHttpClient,
                category.Name == "RANDOM" ? $"https://api.nekosapi.com/v3/images/random?limit={amount}" :
                $"https://api.nekosapi.com/v3/images/tags/{category.Id}/images?limit={amount}");
            if (c.IsCancellationRequested)
            {
                onError(Resources.error_accessapi);
                return;
            }

            doProgress(Resources.progress_fetching);

            var mainRequest = JObject.Parse(message);

            if (mainRequest["count"].ToObject<int>() == 0)
            {
                onError(Resources.error_emptycatalogue);
                return;
            }
            
            var callResults = mainRequest["items"].Select(o =>
                new ExtensiveCallResult
                {
                    Id = o["id"].ToObject<int>(),
                    IdV2 = o["id_v2"].ToObject<string>(),
                    ImageUrl = o["image_url"].ToObject<string>(),
                    SampleUrl = o["sample_url"].ToObject<string>(),
                    Source = o["source"].ToObject<string>(),
                    SourceId = o["source_id"].ToObject<string>(),
                    Rating = o["rating"].ToObject<string>(),
                    Verification = o["verification"].ToObject<string>(),
                    HashMd5 = o["hash_md5"].ToObject<string>(),
                    HashPerceptual = o["hash_perceptual"].ToObject<string>(),
                    IsOriginal = o["is_original"].ToObject<bool>(),
                    IsScreenshot = o["is_screenshot"].ToObject<bool>(),
                    IsFlagged = o["is_flagged"].ToObject<bool>(),
                    IsAnimated = o["is_animated"].ToObject<bool>(),
                    Artist = BuildArtistString(o["artist"]),
                    CreatedAt = o["created_at"].ToObject<decimal>(),
                    UpdatedAt = o["updated_at"].ToObject<decimal>(),
                });
            var i = 1;
            var k = callResults.Count();
            foreach (var description in callResults)
            {
                doProgress(string.Format(Resources.progress_downloadimage, i, k));
                i++;

                /* Description Name */
                var idx = description.ImageUrl.LastIndexOf('/');
                var imageName = idx != -1 ? description.ImageUrl.Substring(idx + 1).Split('.')[0] : description.ImageUrl;
                Image imageItself = await DownloadScaryFormatImage(c, description.ImageUrl);

                if (c.IsCancellationRequested)
                {
                    onError(Resources.error_downloadimage);
                }

                var builder = new StringBuilder();
                foreach (var prop in description.GetType().GetProperties())
                {
                    builder.Append(prop.Name.ToLower()).Append(": ").Append(prop.GetValue(description)).Append("\n");
                }

                onSuccess(new ResultImage
                {
                    ImageName = imageName,
                    ImageItself = imageItself,
                    SourceUrl = description.ImageUrl,
                    NeedAnimation = description.ImageUrl.EndsWith(".gif"),
                    FormattedDescription = builder.ToString()
                });
            }

            onFinal();
        }
        
        private async Task<IEnumerable<CategoryImage>> BuildCategoryList(CancellationTokenSource c)
        {
            var message =
                await GeneralAccess.GetMessageAsync(c, _localHttpClient, "https://api.nekosapi.com/v3/images/tags");
            if (c.IsCancellationRequested)
            {
                MessageBox.Show(message, Resources.dialog_messages_error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            var images = new List<CategoryImage> { new CategoryImage {Name = "RANDOM"} };
            images.AddRange(JObject.Parse(message)["items"].Select(v => new CategoryImage
                { Name = v["name"].ToObject<string>(), Type = v["sub"].ToObject<string>(), IsSafe = !v["is_nsfw"].ToObject<bool>(), Id = v["id"].ToObject<int>()}));
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
            using (var magickImages = new MagickImageCollection(buffer)) {
                var ms = new MemoryStream();
                await magickImages.WriteAsync(ms, magickImages.Count > 1 ? MagickFormat.Gif : MagickFormat.Png);
                bitmap = new Bitmap(ms);
                bitmap.Tag = ms;
            }
            return bitmap;
        }

        private static string BuildArtistString(JToken artist)
        {
            if (!artist.HasValues)
            {
                return null;
            }
            
            var builder = new StringBuilder();
            builder.Append("Artist Name: ").Append(artist["name"].ToObject<string>()).Append("\n");
            builder.Append("Artist Aliases:\n");
            foreach (var obj1 in artist["aliases"].Children())
            {
                builder.Append(obj1.ToObject<string>()).Append("\n");
            }

            builder.Append("Image Original URL: ").Append(artist["image_url"].ToObject<string>()).Append("\n");
            builder.Append("Links:\n");
            foreach (var obj1 in artist["links"].Children())
            {
                builder.Append(obj1.ToObject<string>()).Append("\n");
            }

            builder.Append("Policy Repost: ").Append(artist["policy_repost"].ToObject<string>()).Append("\n");
            builder.Append("Policy Credit: ").Append(artist["policy_credit"].ToObject<bool>()).Append("\n");
            builder.Append("Policy AI: ").Append(artist["policy_ai"].ToObject<bool>()).Append("\n");
            return builder.ToString();
        }
    }
}
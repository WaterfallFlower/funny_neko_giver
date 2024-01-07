using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading;
using funny_neko_giver.Properties;
using Newtonsoft.Json;

namespace funny_neko_giver.ImageApi
{
    public class NekosFunApiProvider : ApiDescription
    {
        public NekosFunApiProvider()
        {
            Name = "Nekos.Fun";
            UrlSimple = "https://www.nekos.fun/";
        }

        public override IImageProviderApi CreateInstance()
        {
            return new NekosFunApi();
        }
    }

    internal class JsonImageResult
    {
        public string Image { get; set; }
    }

    public class NekosFunApi : IImageProviderApi
    {
        /* Well, the tags list is offline. */
        private static readonly IEnumerable<CategoryImage> LocalCategories = new[]
        {
            new CategoryImage { Name = "Random Category", Type = "rnd" },
            new CategoryImage { Name = "kiss" },
            new CategoryImage { Name = "lick" },
            new CategoryImage { Name = "hug" },
            new CategoryImage { Name = "baka" },
            new CategoryImage { Name = "cry" },
            new CategoryImage { Name = "poke" },
            new CategoryImage { Name = "smug" },
            new CategoryImage { Name = "slap" },
            new CategoryImage { Name = "tickle" },
            new CategoryImage { Name = "pat" },
            new CategoryImage { Name = "laugh" },
            new CategoryImage { Name = "feed" },
            new CategoryImage { Name = "cuddle" },
            new CategoryImage { Name = "4k", IsSafe = false },
            new CategoryImage { Name = "ass", IsSafe = false },
            new CategoryImage { Name = "blowjob", IsSafe = false },
            new CategoryImage { Name = "boobs", IsSafe = false },
            new CategoryImage { Name = "cum", IsSafe = false },
            new CategoryImage { Name = "feet", IsSafe = false },
            new CategoryImage { Name = "hentai", IsSafe = false },
            new CategoryImage { Name = "wallpapers", IsSafe = false },
            new CategoryImage { Name = "spank", IsSafe = false },
            new CategoryImage { Name = "gasm", IsSafe = false },
            new CategoryImage { Name = "lesbian", IsSafe = false },
            new CategoryImage { Name = "lewd", IsSafe = false },
            new CategoryImage { Name = "pussy", IsSafe = false }
        };

        private HttpClient _localHttpClient;

        public IEnumerable<CategoryImage> GetCategories()
        {
            return LocalCategories;
        }

        public void Init(HttpClient client, Action<string> onError, Action<IImageProviderApi> onSuccess)
        {
            _localHttpClient = client;
            onSuccess(this);
        }

        public async void LoadCategoryImage(
            CategoryImage category, int amount,
            Action<string> onError, Action<ResultImage> pushReadyImage,
            Action<string> callProgressBar, Action onFinal)
        {
            for (var i = 0; i < amount; i++)
            {
                var token = new CancellationTokenSource();
                callProgressBar(Resources.progress_connectapi);

                var categoryLoad = category.Name;
                
                if (category.Type == "rnd")
                {
                    var rnd = new Random();
                    categoryLoad = LocalCategories.ElementAt(rnd.Next(1, LocalCategories.Count() - 1)).Name;
                }
                
                var message = await GeneralAccess.GetMessageAsync(token, _localHttpClient,$"http://api.nekos.fun:8080/api/{categoryLoad}");
                if (token.IsCancellationRequested)
                {
                    onError(Resources.error_accessapi);
                    continue;
                }

                callProgressBar(Resources.progress_fetching);
                var content = JsonConvert.DeserializeObject<JsonImageResult>(message);
                callProgressBar(string.Format(Resources.progress_downloadimage, i, amount));

                Image image;
                var response = await _localHttpClient.GetAsync(content.Image, token.Token);
                
                if (response.IsSuccessStatusCode && !token.IsCancellationRequested)
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
                    ImageName = GeneralAccess.GetNameFromImageUrl(content.Image),
                    ImageItself = image,
                    SourceUrl = content.Image,
                    NeedAnimation = content.Image.EndsWith(".gif"),
                    FormattedDescription = content.Image
                });
            }

            onFinal();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading;
using funny_neko_giver.Properties;
using Newtonsoft.Json;

namespace funny_neko_giver
{
    public class NekosFunApiProvider : ImageApiDescription
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

    internal class BindedImageResult
    {
        public string Image { get; set; }
    }

    public class NekosFunApi : IImageProviderApi
    {
        /* Well, the tags list is offline. */
        private static readonly IEnumerable<CategoryImage> LocalCategories = new[]
        {
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
            //Nothing really to prepare tbh...
        }

        public async void LoadCategoryImage(
            CategoryImage category, int amount,
            Action<string> onError, Action<ResultImage> onSuccess,
            Action<string> doProgress, Action onFinal)
        {
            for (int i = 0; i < amount; i++)
            {
                var cancellationToken = new CancellationTokenSource();
                doProgress(Resources.progress_connectapi);
                var message = await GeneralAccess.GetMessageAsync(cancellationToken, _localHttpClient,
                    $"http://api.nekos.fun:8080/api/{category.Name}");
                if (cancellationToken.IsCancellationRequested)
                {
                    onError(Resources.error_accessapi);
                    continue;
                }

                doProgress(Resources.progress_fetching);
                var listDescription = JsonConvert.DeserializeObject<BindedImageResult>(message);
                doProgress(string.Format(Resources.progress_downloadimage, i, amount));

                /* Description Name */
                var idx = listDescription.Image.LastIndexOf('/');
                var imageName = idx != -1
                    ? listDescription.Image.Substring(idx + 1).Split('.')[0]
                    : listDescription.Image;
                Image imageItself = null;

                var response = await _localHttpClient.GetAsync(listDescription.Image);
                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        imageItself = Image.FromStream(stream);
                        stream.Close();
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
                    SourceUrl = listDescription.Image,
                    NeedAnimation = listDescription.Image.EndsWith(".gif"),
                    FormattedDescription = listDescription.Image
                });
            }

            onFinal();
        }
    }
}
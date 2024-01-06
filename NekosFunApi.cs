using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using funny_neko_giver.Properties;
using Newtonsoft.Json;

namespace funny_neko_giver
{
    public class NekosFunApiProvider : ImageApiDescription
    {
        public NekosFunApiProvider()
        {
            Name = "NEKOS.FUN";
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
        private static readonly IEnumerable<CategoryImage> LocalCategories = new[]
        {
            new CategoryImage {Name = "kiss", Type = "SFW"},
            new CategoryImage {Name = "lick", Type = "SFW"},
            new CategoryImage {Name = "hug", Type = "SFW"},
            new CategoryImage {Name = "baka", Type = "SFW"},
            new CategoryImage {Name = "cry", Type = "SFW"},
            new CategoryImage {Name = "poke", Type = "SFW"},
            new CategoryImage {Name = "smug", Type = "SFW"},
            new CategoryImage {Name = "slap", Type = "SFW"},
            new CategoryImage {Name = "tickle", Type = "SFW"},
            new CategoryImage {Name = "pat", Type = "SFW"},
            new CategoryImage {Name = "laugh", Type = "SFW"},
            new CategoryImage {Name = "feed", Type = "SFW"},
            new CategoryImage {Name = "cuddle", Type = "SFW"},
            new CategoryImage {Name = "4k", Type = "NSFW"},
            new CategoryImage {Name = "ass", Type = "NSFW"},
            new CategoryImage {Name = "blowjob", Type = "NSFW"},
            new CategoryImage {Name = "boobs", Type = "NSFW"},
            new CategoryImage {Name = "cum", Type = "NSFW"},
            new CategoryImage {Name = "feet", Type = "NSFW"},
            new CategoryImage {Name = "hentai", Type = "NSFW"},
            new CategoryImage {Name = "wallpapers", Type = "NSFW"},
            new CategoryImage {Name = "spank", Type = "NSFW"},
            new CategoryImage {Name = "gasm", Type = "NSFW"},
            new CategoryImage {Name = "lesbian", Type = "NSFW"},
            new CategoryImage {Name = "lewd", Type = "NSFW"},
            new CategoryImage {Name = "pussy", Type = "NSFW"}
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
                var message = await GetMessageAsync(cancellationToken, $"http://api.nekos.fun:8080/api/{category.Name}");
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
                var imageName =idx != -1 ? listDescription.Image.Substring(idx + 1).Split('.')[0] : listDescription.Image;
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
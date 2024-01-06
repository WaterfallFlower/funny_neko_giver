using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace funny_neko_giver
{
    public class CategoryImage
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }
        public bool IsSafe { get; set; } = true;

        public override string ToString()
        {
            if (Name == "RANDOM")
            {
                return Name;
            }
            var builder = new StringBuilder();
            builder.Append(Name).Append(" ");
            if (Type != null)
            {
                builder.Append('(').Append(Type).Append(") ");
            }
            builder.Append(IsSafe ? "(SFW)" : "(NSFW)");
            return builder.ToString();
        }
    }

    public class ResultImage
    {
        public Image ImageItself { get; set; }
        public string ImageName { get; set; }
        public bool NeedAnimation { get; set; }
        public string SourceUrl { get; set; }
        public string FormattedDescription { get; set; }

        public override string ToString()
        {
            return ImageName;
        }
    }

    public abstract class ImageApiDescription
    {
        public string Name { get; set; }
        public string UrlSimple { get; set; }

        public abstract IImageProviderApi CreateInstance();

        public override string ToString()
        {
            return Name;
        }
    }

    public interface IImageProviderApi
    {
        IEnumerable<CategoryImage> GetCategories();

        void Init(HttpClient client, Action<string> onError, Action<IImageProviderApi> onSuccess);

        void LoadCategoryImage(
            CategoryImage category, int amount,
            Action<string> onError, Action<ResultImage> onSuccess,
            Action<string> doProgress, Action onFinal
        );
    }

    public class GeneralAccess
    {
        public static async Task<string> GetMessageAsync(
            CancellationTokenSource c, HttpClient _localHttpClient,string uri
            )
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
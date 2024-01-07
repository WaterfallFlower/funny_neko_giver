using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            if (Type == "rnd")
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

    public abstract class ApiDescription
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
            Action<string> onError, Action<ResultImage> pushReadyImage,
            Action<string> callProgressBar, Action onFinal
        );
    }

    public static class GeneralAccess
    {
        public static string GetNameFromImageUrl(string s)
        {
            var idx = s.LastIndexOf('/');
            return idx != -1? s.Substring(idx + 1).Split('.')[0]: s;
        }

        public static async Task<string> GetMessageAsync(CancellationTokenSource token, HttpClient client, string uri)
        {
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            token?.Cancel();
            return $"Error accessing: {response.ReasonPhrase}";
        }

        public static string GetAllPropertiesList<T>(T tObject)
        {
            var builder = new StringBuilder();
            foreach (var prop in tObject.GetType().GetProperties())
            {
                var value = prop.GetValue(tObject);
                if (value is IEnumerable<object> objects)
                {
                    value = $"[{string.Join(", ", objects.ToArray() )}]";
                }
                
                builder.Append(prop.Name.ToLower()).Append(": ").Append(value ?? "[N/A]").Append("\n\n");
            }
            return builder.ToString();
        }
    }
}
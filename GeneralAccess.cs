using System;
using System.Collections.Generic;
using System.Drawing;

namespace funny_neko_giver
{
    public class CategoryImage
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return Type != null ? $"{Name} ({Type})" : Name;
        }
    }

    public class ResultImage
    {
        public Image ImageItself { get; set; }
        public string ImageName { get; set; }
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
        
        void Init(Action<string> onError, Action<IImageProviderApi> onSuccess);

        void LoadCategoryImage(
            CategoryImage category, int amount,
            Action<string> onError, Action<ResultImage> onSuccess,
            Action<string> doProgress, Action onFinal
        );
    }
}
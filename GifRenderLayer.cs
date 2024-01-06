using System.Drawing;
using System.Drawing.Imaging;

namespace funny_neko_giver
{
    public class GifRenderLayer
    {
        private Image _gifImage;
        private FrameDimension _dimension;
        private int _frameCount;
        private int _currentFrame = -1;
        private int _step = 1;

        public GifRenderLayer(Image image)
        {
            _gifImage = image;
            _dimension = new FrameDimension(image.FrameDimensionsList[0]);
            _frameCount = image.GetFrameCount(_dimension);
        }

        public Image GetNextFrame()
        {
            _currentFrame += _step;
            if (_currentFrame < _frameCount && _currentFrame >= 0) return GetFrame(_currentFrame);
            _currentFrame = 0;
            return GetFrame(_currentFrame);
        }

        public Image GetFrame(int index)
        {
            _gifImage.SelectActiveFrame(_dimension, index);
            return (Image)_gifImage.Clone();
        }
    }
}
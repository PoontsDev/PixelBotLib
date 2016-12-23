using System.Collections.Generic;
using System.Drawing;
using PixelBotLib.Imaging;

namespace PixelBotLib.PixelMap
{
    public class PixelMap
    {
        public PixelMap(Size size)
        {
            _size = size;
        }

        public PixelMap(int width, int height)
        {
            _size = new Size(width, height);
        }

        public PixelMap(Size size, List<Pixel> pixels)
        {
            _size = size;
            _pixels = pixels;
        }

        private readonly Size _size;
        private List<Pixel> _pixels;

        public Size Size { get { return _size; } }
        public int Width { get { return _size.Width; } }
        public int Height { get { return _size.Height; } }

        public void AddArray(Pixel[] array)
        {
            foreach (var pixel in array)
            {
                _pixels.Add(pixel);
            }
        }

        public void AddList(List<Pixel> list)
        {
            _pixels.AddRange(list);
        }

        public void AddPixel(Pixel pixel)
        {
            _pixels.Add(pixel);
        }

        public Pixel[] PixelArray()
        {
            return _pixels.ToArray();
        }

        public List<Pixel> Pixels()
        {
            return _pixels;
        }

        public bool MatchImage(Bitmap image, Point startPoint)
        {
            var result = false;
            using (var window = ImageHelpers.CropImage(image, new Rectangle(startPoint, this.Size)))
            {
                var lockBitmap = new LockBitmap(window);
                lockBitmap.LockBits();
                foreach (var pixel in this.Pixels())
                {
                    result = HexConverter.ColorToHex(lockBitmap.GetPixel(pixel.Point.X, pixel.Point.Y)) == pixel.ToHex();
                    if (!result)
                    {
                        break;
                    }
                }
                lockBitmap.UnlockBits();
            }
            return result;
        }
    }
}

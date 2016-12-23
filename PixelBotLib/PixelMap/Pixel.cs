using System.Drawing;
using PixelBotLib.Imaging;

namespace PixelBotLib.PixelMap
{
    public class Pixel
    {
        public Pixel(int x, int y, Color color)
        {
            _point = new Point(x, y);
            _color = color;
        }

        public Pixel(Point point, Color color)
        {
            _point = point;
            _color = color;
        }

        private readonly Point _point;
        private readonly Color _color;

        public Point Point { get { return _point; } }
        public Color Color { get { return _color; } }

        public string ToHex()
        {
            return HexConverter.ColorToHex(_color);
        }
    }
}

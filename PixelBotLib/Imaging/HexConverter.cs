using System.Drawing;

namespace PixelBotLib.Imaging
{
    public class HexConverter
    {
        public static string ColorToHex(Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        public static Color HexToColor(string hex)
        {
            return ColorTranslator.FromHtml(hex);
        }
    }
}

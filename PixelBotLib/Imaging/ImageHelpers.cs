using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PixelBotLib.Native;

namespace PixelBotLib.Imaging
{
    public class ImageHelpers
    {
        public static Bitmap CropImage(Bitmap source, Rectangle section)
        {
            Bitmap bmp = new Bitmap(section.Width, section.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }

        public static Bitmap ImageToBitmap(Image image)
        {
            var bitmap = new Bitmap(image);
            return bitmap;
        }

        public static bool MatchColor(Color color, IEnumerable<Color> matchList)
        {
            return matchList.Any(c => c.Equals(color));
        }

        public static Color GetColor(IntPtr wHandle,int x, int y)
        {
            var hdc = User32.GetWindowDC(wHandle);
            var pixel = GDI32.GetPixel(hdc, x, y);
            User32.ReleaseDC(wHandle, hdc);
            var color = Color.FromArgb((int)(pixel & 0x000000FF),
                         (int)(pixel & 0x0000FF00) >> 8,
                         (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }

        public static List<Point> SearchColor(IntPtr wHandle, List<Color> colorList, Point startPoint, int width, int height, bool continuedSearch)
        {
            var returnList = new List<Point>();
            var y = 0;

            while (!(y >= height))
            {
                for (int x = 0; x < width; x++)
                {
                    if (MatchColor(GetColor(wHandle, startPoint.X + x, startPoint.Y + y), colorList))
                    {
                        returnList.Add(new Point(startPoint.X + x, startPoint.Y + y));
                        if (!continuedSearch)
                        {
                            return returnList;
                        }
                    }
                }
                y++;
            }

            return returnList;
        }
    }
}

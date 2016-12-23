using System;
using System.Collections.Generic;
using System.Drawing;

namespace PixelBotLib.Imaging
{
    public class ImageSearch
    {
        public List<Point> SearchPoints(IntPtr wHandle, Bitmap searchBitmap)
        {
            var screenCapture = new ScreenCapture();
            var pointsList = new List<Point>();
            var wndLocked = new LockBitmap(ImageHelpers.ImageToBitmap(screenCapture.CaptureWindow(wHandle)));
            var searchLocked = new LockBitmap(searchBitmap);
            wndLocked.LockBits();
            searchLocked.LockBits();
            int y = 1;
            while (y < wndLocked.Height)
            {
                for (int x = 1; x < wndLocked.Width; x++)
                {
                    if (wndLocked.GetPixel(x, y) == searchLocked.GetPixel(1, 1))
                    {
                        var correctPixels = true;
                        var yy = 1;
                        var xx = 1;
                        while (correctPixels && yy < searchLocked.Height)
                        {
                            while (correctPixels && xx < searchLocked.Width)
                            {
                                if (searchLocked.GetPixel(xx, yy) != wndLocked.GetPixel(x + xx - 1, y + yy - 1))
                                {
                                    correctPixels = false;
                                }
                                xx++;
                            }
                            yy++;
                        }
                        if (correctPixels)
                        {
                            pointsList.Add(new Point(x, y));
                        }
                    }
                }
                y++;
            }
            wndLocked.UnlockBits();
            searchLocked.UnlockBits();
            return pointsList;
        }

        public Point SearchPoint(IntPtr wHandle, Bitmap searchBitmap)
        {
            var screenCapture = new ScreenCapture();
            var wndLocked = new LockBitmap(ImageHelpers.ImageToBitmap(screenCapture.CaptureWindow(wHandle)));
            var searchLocked = new LockBitmap(searchBitmap);
            wndLocked.LockBits();
            searchLocked.LockBits();
            int y = 1;
            while (y < wndLocked.Height)
            {
                for (int x = 1; x < wndLocked.Width; x++)
                {
                    if (wndLocked.GetPixel(x, y) == searchLocked.GetPixel(1, 1))
                    {
                        var correctPixels = true;
                        var yy = 1;
                        var xx = 1;
                        while (correctPixels && yy < searchLocked.Height)
                        {
                            while (correctPixels && xx < searchLocked.Width)
                            {
                                if (searchLocked.GetPixel(xx, yy) != wndLocked.GetPixel(x + xx - 1, y + yy - 1))
                                {
                                    correctPixels = false;
                                }
                                xx++;
                            }
                            yy++;
                        }
                        if (correctPixels)
                        {
                            wndLocked.UnlockBits();
                            searchLocked.UnlockBits();
                            return new Point(x, y);
                        }
                    }
                }
                y++;
            }
            wndLocked.UnlockBits();
            searchLocked.UnlockBits();
            return new Point();
        }
    }
}

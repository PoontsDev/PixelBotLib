using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace PixelBotLib.Native
{
    public class NativeConstants
    {
        public static uint WM_KEYDOWN = 0x100;
        public static uint WM_KEYUP = 0x101;
        public static uint WM_SYSKEYDOWN = 0x104;
        public static uint WM_LBUTTONDOWN = 0x0201;
        public static uint WM_LBUTTONUP = 0x0202;
        public static uint WM_RBUTTONDOWN = 0x0204;
        public static uint WM_RBUTTONUP = 0x0205;
        public static uint wM_MOUSEMOVE = 0x0200;
        public static uint WM_PRINT = 0x0317;

        [Flags]
        public enum DrawingOptions
        {
            PRF_CHECKVISIBLE = 0x01,
            PRF_NONCLIENT = 0x02,
            PRF_CLIENT = 0x04,
            PRF_ERASEBKGND = 0x08,
            PRF_CHILDREN = 0x10,
            PRF_OWNED = 0x20
        }

        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;

        public const int SRCCOPY = 0x00CC0020;

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }
    }
}

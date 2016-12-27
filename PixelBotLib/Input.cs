using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using PixelBotLib.Native;

namespace PixelBotLib
{
    public class Input
    {
        public static void SendKey(IntPtr wHandle, uint keyCode, bool extended)
        {
            uint scanCode = User32.MapVirtualKey(keyCode, 0);
            uint lParam;

            //KEY DOWN
            lParam = (0x00000001 | (scanCode << 16));
            if (extended)
            {
                lParam = lParam | 0x01000000;
            }

            PostMessageSafe(wHandle, NativeConstants.WM_KEYDOWN, new IntPtr(keyCode), new IntPtr(lParam));

            Thread.Sleep(50);

            PostMessageSafe(wHandle, NativeConstants.WM_KEYUP, new IntPtr(keyCode), new IntPtr(lParam));
        }

        public static void LeftClick(IntPtr wHandle, Point point, int time)
        {
            PostMessageSafe(wHandle, NativeConstants.WM_LBUTTONDOWN, new IntPtr(0), new IntPtr(Makelparam((uint)point.X, (uint)point.Y)));
            Thread.Sleep(time);
            PostMessageSafe(wHandle, NativeConstants.WM_LBUTTONUP, new IntPtr(0), new IntPtr(Makelparam((uint)point.X, (uint)point.Y)));
        }

        public static void RightClick(IntPtr wHandle, Point point, int time)
        {
            PostMessageSafe(wHandle, NativeConstants.WM_RBUTTONDOWN, new IntPtr(0), new IntPtr(Makelparam((uint)point.X, (uint)point.Y)));
            Thread.Sleep(time);
            PostMessageSafe(wHandle, NativeConstants.WM_RBUTTONUP, new IntPtr(0), new IntPtr(Makelparam((uint)point.X, (uint)point.Y)));
        }

        private static uint Makelparam(uint l, uint h)
        {

            return ((h << 16) | (l & 0xFFFF));
        }

        private static void PostMessageSafe(IntPtr wHandle, uint msg, IntPtr wParam, IntPtr lParam)
        {
            bool returnValue = User32.PostMessage(wHandle, msg, wParam, lParam);
            if (!returnValue)
            {
                // An error occured
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }
}

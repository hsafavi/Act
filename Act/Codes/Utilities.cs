using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace dastyar.Codes
{
    class Utilities
    {
        public static BitmapSource CopyScreen()
        {

            var left = 0;
            var top = 0;
            var right = (int)SystemParameters.PrimaryScreenWidth;
            var bottom = (int)SystemParameters.PrimaryScreenHeight;
            var width = right - left;
            var height = bottom - top;

            using (var screenBmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (var bmpGraphics = Graphics.FromImage(screenBmp))
                {
                    bmpGraphics.CopyFromScreen(left, top, 0, 0, new System.Drawing.Size(width, height));
                    return Imaging.CreateBitmapSourceFromHBitmap(
                        screenBmp.GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
            }
        }
        public static Bitmap CopyScreenBitmap()
        {
            var left = 0;
            var top = 0;
            var right = (int)SystemParameters.PrimaryScreenWidth;
            var bottom = (int)SystemParameters.PrimaryScreenHeight;
            var width = right - left;
            var height = bottom - top;

            var screenBmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            {
                using (var bmpGraphics = Graphics.FromImage(screenBmp))
                {
                    bmpGraphics.CopyFromScreen(left, top, 0, 0, new System.Drawing.Size(width, height));
                    return screenBmp;
                }

            }
        }
    }
}

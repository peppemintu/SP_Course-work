using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Render
{
    public class FastBitmap : IDisposable
    {
        public Bitmap Bitmap { get; private set; }
        public Int32[] Bits { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        protected GCHandle BitsHandle { get; private set; }

        public static FastBitmap FromBitmap(Bitmap bitmap)
        {
            var fastBitmap = new FastBitmap(bitmap.Width , bitmap.Height);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int g = 0; g < bitmap.Height; g++)
                {
                    fastBitmap.Bits[g * bitmap.Width + i] = bitmap.GetPixel(i, g).ToArgb();
                }
            }
            return fastBitmap;
        }
        public FastBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new Int32[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        public void SetPixel(int index, Color colour)
        {
            int col = colour.ToArgb();
            Bits[index] = col;
        }

        public void Dispose()
        {
            BitsHandle.Free();
        }
    }
}

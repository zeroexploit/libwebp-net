using System;
using System.Drawing;
using System.Drawing.Imaging;
using LibWebP.Net.Native;

namespace LibWebP.Net
{
    public class WebPDecoder
    {
        public static string GetDecoderVersion()
        {
            var v = (uint)NativeMethods.WebPGetDecoderVersion();

            var revision = v % 256;
            var minor = (v >> 8) % 256;
            var major = (v >> 16) % 256;

            return $"{major}.{minor}.{revision}";
        }

        public unsafe Bitmap DecodeFromBytes(byte[] data, long length)
        {
            fixed (byte* dataptr = data)
            {
                return DecodeFromPointer((IntPtr)dataptr, length);
            }
        }

        public Bitmap DecodeFromPointer(IntPtr data, long length)
        {
            var width = 0;
            var height = 0;

            if (NativeMethods.WebPGetInfo(data, (UIntPtr)length, ref width, ref height) == 0)
                throw new InvalidWebPHeaderException();

            var success = false;
            Bitmap bitmap = null;
            BitmapData bitmapData = null;

            try
            {
                bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                var result = NativeMethods.WebPDecodeBGRAInto(data, (UIntPtr)length, bitmapData.Scan0, (UIntPtr)(bitmapData.Stride * bitmapData.Height), bitmapData.Stride);

                if (bitmapData.Scan0 != result)
                    throw new WebPDecodingException((long)result);

                success = true;
            }
            finally
            {
                if (bitmapData != null)
                    bitmap.UnlockBits(bitmapData);

                if (!success && bitmap != null)
                    bitmap.Dispose();
            }

            return bitmap;
        }
    }
}

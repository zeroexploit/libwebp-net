using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using LibWebP.Net.Native;

namespace LibWebP.Net
{
    public static class WebPEncoder
    {
        public static string GetEncoderVersion()
        {
            var v = (uint)NativeMethods.WebPGetEncoderVersion();

            var revision = v % 256;
            var minor = (v >> 8) % 256;
            var major = (v >> 16) % 256;

            return $"{major}.{minor}.{revision}";
        }

        /// <summary>
        /// Encodes the given RGB(A) bitmap to the given stream. Specify quality = -1 for lossless, otherwise specify a value between 0 and 100.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="quality"></param>
        public static unsafe void Encode(Bitmap from, Stream to, float quality)
        {
            var result = IntPtr.Zero;
            
            try
            {
                Encode(from, quality, out result, out var length);

                var pointer = (byte*)result.ToPointer();
                using var unmangedStream = new UnmanagedMemoryStream(pointer, length, length, FileAccess.Read);
                unmangedStream.CopyTo(to);
            }
            finally
            {
                NativeMethods.WebPFree(result);
            }
        }

        /// <summary>
        /// Encodes the given RGB(A) bitmap to an unmanged memory buffer (returned via result/length). Specify quality = -1 for lossless, otherwise specify a value between 0 and 100.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="quality"></param>
        /// <param name="result"></param>
        /// <param name="length"></param>
        public static void Encode(Bitmap bitmap, float quality, out IntPtr result, out long length)
        {
            if (quality < -1)
                quality = -1;

            if (quality > 100)
                quality = 100;

            var width = bitmap.Width;
            var height = bitmap.Height;
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            try
            {
                result = IntPtr.Zero;

                switch (bitmap.PixelFormat)
                {
                    case PixelFormat.Format32bppArgb when quality == -1:
                        length = (long)NativeMethods.WebPEncodeLosslessBGRA(bitmapData.Scan0, width, height, bitmapData.Stride, ref result);
                        break;
                    case PixelFormat.Format32bppArgb:
                        length = (long)NativeMethods.WebPEncodeBGRA(bitmapData.Scan0, width, height, bitmapData.Stride, quality, ref result);
                        break;
                    case PixelFormat.Format24bppRgb when quality == -1:
                        length = (long)NativeMethods.WebPEncodeLosslessBGR(bitmapData.Scan0, width, height, bitmapData.Stride, ref result);
                        break;
                    case PixelFormat.Format24bppRgb:
                        length = (long)NativeMethods.WebPEncodeBGR(bitmapData.Scan0, width, height, bitmapData.Stride, quality, ref result);
                        break;
                    default:
                        {
                            using var b2 = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format32bppArgb);
                            Encode(b2, quality, out result, out length);
                            break;
                        }
                }

                if (length == 0)
                    throw new WebPEncodingException();
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
    }
}

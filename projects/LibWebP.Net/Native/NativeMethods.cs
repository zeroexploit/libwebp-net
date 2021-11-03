using System;
using System.Runtime.InteropServices;

namespace LibWebP.Net.Native
{
    static class NativeMethods
    {
        [DllImport("libwebp", EntryPoint = "WebPGetDecoderVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebPGetDecoderVersion();

        [DllImport("libwebp", EntryPoint = "WebPGetInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebPGetInfo([In] IntPtr data, UIntPtr dataSize, ref int width, ref int height);

        [DllImport("libwebp", EntryPoint = "WebPDecodeBGRAInto", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPDecodeBGRAInto([In] IntPtr data, UIntPtr dataSize, IntPtr outputBuffer, UIntPtr outputBufferSize, int outputStride);

        [DllImport("libwebp", EntryPoint = "WebPGetEncoderVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebPGetEncoderVersion();

        [DllImport("libwebp", EntryPoint = "WebPEncodeBGR", CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr WebPEncodeBGR([In] IntPtr bgr, int width, int height, int stride, float qualityFactor, ref IntPtr output);

        [DllImport("libwebp", EntryPoint = "WebPEncodeBGRA", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPEncodeBGRA([In] IntPtr bgra, int width, int height, int stride, float qualityFactor, ref IntPtr output);

        [DllImport("libwebp", EntryPoint = "WebPEncodeLosslessBGR", CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr WebPEncodeLosslessBGR([In] IntPtr bgr, int width, int height, int stride, ref IntPtr output);

        [DllImport("libwebp", EntryPoint = "WebPEncodeLosslessBGRA", CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr WebPEncodeLosslessBGRA([In] IntPtr bgra, int width, int height, int stride, ref IntPtr output);

        [DllImport("libwebp", EntryPoint = "WebPFree", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WebPFree(IntPtr toDeallocate);
    }
}
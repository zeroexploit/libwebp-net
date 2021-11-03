using System;
using System.Runtime.InteropServices;

namespace LibWebP.Net.Extern
{
    public enum WebPImageHint
    {
        WEBP_HINT_DEFAULT = 0,
        WEBP_HINT_PICTURE,
        WEBP_HINT_PHOTO,
        WEBP_HINT_GRAPH,
        WEBP_HINT_LAST,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WebPConfig
    {
        /// Lossless encoding (0=lossy(default), 1=lossless).
        public int lossless;

        /// between 0 (smallest file) and 100 (biggest)
        public float quality;

        /// quality/speed trade-off (0=fast, 6=slower-better)
        public int method;


        public WebPImageHint image_hint;

        /// if non-zero, set the desired target size in bytes. Takes precedence over the 'compression' parameter.
        public int target_size;

        /// if non-zero, specifies the minimal distortion to try to achieve. Takes precedence over target_size.
        public float target_PSNR;

        /// maximum number of segments to use, in [1..4]
        public int segments;

        /// Spatial Noise Shaping. 0=off, 100=maximum.
        public int sns_strength;

        /// range: [0 = off .. 100 = strongest]
        public int filter_strength;

        /// range: [0 = off .. 7 = least sharp]
        public int filter_sharpness;

        /// filtering type: 0 = simple, 1 = strong (only used
        /// if filter_strength > 0 or autofilter > 0)
        public int filter_type;

        ///  Auto adjust filter's strength [0 = off, 1 = on]
        public int autofilter;

        /// Algorithm for encoding the alpha plane (0 = none,
        /// 1 = compressed with WebP lossless). Default is 1.
        public int alpha_compression;

        /// Predictive filtering method for alpha plane.
        ///  0: none, 1: fast, 2: best. Default if 1.
        public int alpha_filtering;

        /// Between 0 (smallest size) and 100 (lossless).
                           // Default is 100.
        public int alpha_quality;

        /// number of entropy-analysis passes (in [1..10]).
        public int pass;

        /// if true, export the compressed picture back.
        /// In-loop filtering is not applied.
        public int show_compressed;

        /// preprocessing filter:
        /// 0=none, 1=segment-smooth, 2=pseudo-random dithering
        public int preprocessing;

        /// log2(number of token partitions) in [0..3]. Default
        /// is set to 0 for easier progressive decoding.
        public int partitions;

        /// quality degradation allowed to fit the 512k limit
        /// on prediction modes coding (0: no degradation,
        /// 100: maximum possible degradation).
        public int partition_limit;

        /// <summary>
        /// If true, compression parameters will be remapped
        /// to better match the expected output size from
        /// JPEG compression. Generally, the output size will
        /// be similar but the degradation will be lower.
        /// </summary>
        public int emulate_jpeg_size;

        /// If non-zero, try and use multi-threaded encoding.
        public int thread_level;

        /// <summary>
        /// If set, reduce memory usage (but increase CPU use).
        /// </summary>
        public int low_memory;

        /// <summary>
        ///  Near lossless encoding [0 = max loss .. 100 = off (default)].
        /// </summary>
        public int near_lossless;

        /// if non-zero, preserve the exact RGB values under
        /// transparent area. Otherwise, discard this invisible
        /// RGB information for better compression. The default
        /// value is 0. 
        public int exact;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U4)]
        public uint[] pad;
    }

    public enum WebPPreset
    {
        // Default
        WEBP_PRESET_DEFAULT = 0,

        /// digital picture, like portrait, inner shot
        WEBP_PRESET_PICTURE,

        /// outdoor photograph, with natural lighting
        WEBP_PRESET_PHOTO,

        /// hand or line drawing, with high-contrast details
        WEBP_PRESET_DRAWING,

        /// small-sized colorful images
        WEBP_PRESET_ICON,

        /// text-like
        WEBP_PRESET_TEXT,
    }

    public delegate int WebPWriterFunction([In] IntPtr data, UIntPtr dataSize, ref WebPPicture picture);

    [StructLayout(LayoutKind.Sequential)]
    public struct WebPMemoryWriter
    {
        public IntPtr mem;
        public UIntPtr size;
        public UIntPtr max_size;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.U4)]
        public uint[] pad;
    }

    public delegate int WebPProgressHook(int percent, ref WebPPicture picture);

    public enum WebPEncCSP
    {
        /// 4:2:0 (half-res chroma x and y)
        WEBP_YUV420 = 0,

        /// bit-mask to get the UV sampling factors
        WEBP_CSP_UV_MASK = 3,

        /// 4:2:0 with alpha
        WEBP_YUV420A = 4,

        /// Bit mask to set alpha
        WEBP_CSP_ALPHA_BIT = 4,
    }

    public enum WebPEncodingError
    {
        VP8_ENC_OK = 0,
        VP8_ENC_ERROR_OUT_OF_MEMORY,
        VP8_ENC_ERROR_BITSTREAM_OUT_OF_MEMORY,
        VP8_ENC_ERROR_NULL_PARAMETER,
        VP8_ENC_ERROR_INVALID_CONFIGURATION,
        VP8_ENC_ERROR_BAD_DIMENSION,
        VP8_ENC_ERROR_PARTITION0_OVERFLOW,
        VP8_ENC_ERROR_PARTITION_OVERFLOW,
        VP8_ENC_ERROR_BAD_WRITE,
        VP8_ENC_ERROR_FILE_TOO_BIG,
        VP8_ENC_ERROR_USER_ABORT,
        VP8_ENC_ERROR_LAST,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WebPPicture
    {
        public int use_argb;
        public WebPEncCSP colorspace;
        public int width;
        public int height;
        public IntPtr y;
        public IntPtr u;
        public IntPtr v;
        public int y_stride;
        public int uv_stride;
        public IntPtr a;
        public int a_stride;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
        public uint[] pad1;


        public IntPtr argb;
        public int argb_stride;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U4)]
        public uint[] pad2;

        public WebPWriterFunction writer;
        public IntPtr custom_ptr;
        public int extra_info_type;
        public IntPtr extra_info;
        public IntPtr stats;
        public WebPEncodingError error_code;
        public WebPProgressHook progress_hook;
        public IntPtr user_data;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U4)]
        public uint[] pad3;

        public IntPtr pad4;
        public IntPtr pad5;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.U4)]
        public uint[] pad6;

        public IntPtr memory_;
        public IntPtr memory_argb_;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.SysUInt)]
        public IntPtr[] pad7;
    }

    public partial class NativeMethods
    {
        [DllImport("libwebp", EntryPoint = "WebPGetEncoderVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebPGetEncoderVersion();

        [DllImport("libwebp", EntryPoint = "WebPEncodeBGR", CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr WebPEncodeBGR([In()] IntPtr bgr, int width, int height, int stride, float qualityFactor, ref IntPtr output);

        [DllImport("libwebp", EntryPoint = "WebPEncodeBGRA", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPEncodeBGRA([In()] IntPtr bgra, int width, int height, int stride, float qualityFactor, ref IntPtr output);

        [DllImport("libwebp", EntryPoint = "WebPEncodeLosslessBGR", CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr WebPEncodeLosslessBGR([In()] IntPtr bgr, int width, int height, int stride, ref IntPtr output);

        [DllImport("libwebp", EntryPoint = "WebPEncodeLosslessBGRA", CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr WebPEncodeLosslessBGRA([In()] IntPtr bgra, int width, int height, int stride, ref IntPtr output);

        [DllImport("libwebp", EntryPoint = "WebPConfigInitInternal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebPConfigInitInternal(ref WebPConfig param0, WebPPreset param1, float param2, int param3);

        [DllImport("libwebp", EntryPoint = "WebPPictureInitInternal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebPPictureInitInternal(ref WebPPicture param0, int param1);
    }
}
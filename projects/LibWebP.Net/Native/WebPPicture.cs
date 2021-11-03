using System;
using System.Runtime.InteropServices;

namespace LibWebP.Net.Native
{
    [StructLayout(LayoutKind.Sequential)]
    struct WebPPicture
    {
        public int use_argb;
        public WebpEncCsp colorspace;
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

    delegate int WebPWriterFunction([In] IntPtr data, UIntPtr dataSize, ref WebPPicture picture);
    delegate int WebPProgressHook(int percent, ref WebPPicture picture);
}
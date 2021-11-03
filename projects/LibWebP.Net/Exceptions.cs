using System;

namespace LibWebP.Net
{
    public class InvalidWebPHeaderException : Exception
    {
        public InvalidWebPHeaderException() : base("Invalid WebP header detected") { }
    }

    public class WebPDecodingException : Exception
    {
        public WebPDecodingException(long errorCode) : base($"Failed to decode WebP image with error: {errorCode}") { }
    }

    public class WebPEncodingException : Exception
    {
        public WebPEncodingException() : base("WebP encode failed") { }
    }
}

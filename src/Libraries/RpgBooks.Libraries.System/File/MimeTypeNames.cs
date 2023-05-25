namespace System.File;

/// <summary>
/// Specifies the media types of files used in the application.
/// </summary>
public static class MimeTypeNames
{
    /// <summary>
    /// Media type used for audio files.
    /// </summary>
    public static class Audio
    {
        /// <summary>
        /// Gets AAC audio type name.
        /// </summary>
        public const string Aac = "audio/aac";

        /// <summary>
        /// Gets MP3 audio type name.
        /// </summary>
        public const string Mp3 = "audio/mpeg";

        /// <summary>
        /// Gets WEBM audio mime type name.
        /// </summary>
        public const string Weba = "audio/webm";

        /// <summary>
        /// Gets WAV audio type name.
        /// </summary>
        public const string Wav = "audio/wav";
    }

    /// <summary>
    /// Media types used or created by specific applications.
    /// </summary>
    public static class Application
    {
        /// <summary>
        /// Gets general media type name.
        /// </summary>
        public const string Octet = "application/octet-stream";

        /// <summary>
        /// Gets the PDF type name.
        /// </summary>
        public const string Pdf = "application/pdf";

        /// <summary>
        /// Old Microsoft Word mime type name.
        /// </summary>
        public const string Doc = "application/msword";

        /// <summary>
        /// New Microsoft Word mime type name.
        /// </summary>
        public const string Docx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        /// <summary>
        /// Old Microsoft PowerPoint mime type name.
        /// </summary>
        public const string Ppt = "application/vnd.ms-powerpoint";

        /// <summary>
        /// New Microsoft PowerPoint mime type name.
        /// </summary>
        public const string Pptx = "application/vnd.openxmlformats-officedocument.presentationml.presentation";

        /// <summary>
        /// Old Microsoft Excel mime type name.
        /// </summary>
        public const string Xls = "application/vnd.ms-excel";

        /// <summary>
        /// New Microsoft Excel mime type name.
        /// </summary>
        public const string Xlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        /// <summary>
        /// Media types used for archives.
        /// </summary>
        public static class Archive
        {
            /// <summary>
            /// Gets GZip compressed archive mime type name.
            /// </summary>
            public const string Gz = "application/gzip";

            /// <summary>
            /// Gets JAR mime type name.
            /// </summary>
            public const string Jar = "application/java-archive";

            /// <summary>
            /// Gets RAR mime type name.
            /// </summary>
            public const string Rar = "application/vnd.rar";

            /// <summary>
            /// Get the ZIP type name.
            /// </summary>
            public const string Zip = "application/zip";
        }
    }

    /// <summary>
    /// Mime types related to HTTP request content type.
    /// </summary>
    public static class Request
    {
        /// <summary>
        /// JSON file mime type name.
        /// </summary>
        public const string Json = "application/json";

        /// <summary>
        /// Request made with multiple content types in multipart form data.
        /// </summary>
        public const string MultipartForm = "multipart/form-data";

        /// <summary>
        /// Request with URL encoded form.
        /// </summary>
        public const string UrlForm = "application/x-www-form-urlencoded";
    }

    /// <summary>
    /// Media types for images.
    /// </summary>
    public static class Image
    {
        /// <summary>
        /// Gets GIF media type name.
        /// </summary>
        public const string Gif = "image/gif";

        /// <summary>
        /// Gets JPG media type name.
        /// </summary>
        public const string Jpg = "image/jpeg";

        /// <summary>
        /// Gets JPEG media type name.
        /// </summary>
        public const string Jpeg = "image/jpeg";

        /// <summary>
        /// Gets Tiff media type name.
        /// </summary>
        public const string Tiff = "image/tiff";

        /// <summary>
        /// Gets SVG media type name.
        /// </summary>
        public const string Svg = "image/svg+xml";

        /// <summary>
        /// Gets PNG media type name.
        /// </summary>
        public const string Png = "image/png";

        /// <summary>
        /// Gets WEBM image mime type name.
        /// </summary>
        public const string Webp = "image/webp";
    }

    /// <summary>
    /// Media types for text files.
    /// </summary>
    public static class Text
    {
        /// <summary>
        /// Gets HTML mime type.
        /// </summary>
        public const string Html = "text/html";

        /// <summary>
        /// Gets plain text mime type.
        /// </summary>
        public const string Txt = "text/plain";

        /// <summary>
        /// Gets XML mime type name.
        /// </summary>
        public const string Xml = "text/xml";
    }

    /// <summary>
    /// Media types used for video files.
    /// </summary>
    public static class Video
    {
        /// <summary>
        /// Gets MP4 media type name.
        /// </summary>
        public const string Mp4 = "video/mp4";

        /// <summary>
        /// Gets MPEG media type name.
        /// </summary>
        public const string Mpeg = "video/mpeg";

        /// <summary>
        /// Gets WEBM video mime type name.
        /// </summary>
        public const string Webm = "video/webm";
    }
}

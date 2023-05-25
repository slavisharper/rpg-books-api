namespace System.File;

/// <summary>
/// Easy access to mime types or extensions.
/// </summary>
public static class MimeTypeHelpers
{
    private static readonly IDictionary<string, string> Extensions = new Dictionary<string, string>
    {
        { MimeTypeNames.Application.Doc, nameof(MimeTypeNames.Application.Doc).ToLower() },
        { MimeTypeNames.Application.Docx, nameof(MimeTypeNames.Application.Docx).ToLower() },
        { MimeTypeNames.Application.Octet, string.Empty },
        { MimeTypeNames.Application.Pdf, nameof(MimeTypeNames.Application.Pdf).ToLower() },
        { MimeTypeNames.Application.Ppt, nameof(MimeTypeNames.Application.Ppt).ToLower() },
        { MimeTypeNames.Application.Pptx, nameof(MimeTypeNames.Application.Pptx).ToLower() },
        { MimeTypeNames.Application.Xls, nameof(MimeTypeNames.Application.Xls).ToLower() },
        { MimeTypeNames.Application.Xlsx, nameof(MimeTypeNames.Application.Xlsx).ToLower() },
        { MimeTypeNames.Audio.Mp3, nameof(MimeTypeNames.Audio.Mp3).ToLower() },
        { MimeTypeNames.Audio.Wav, nameof(MimeTypeNames.Audio.Wav).ToLower() },
        { MimeTypeNames.Audio.Weba, nameof(MimeTypeNames.Audio.Weba).ToLower() },
        { MimeTypeNames.Audio.Aac, nameof(MimeTypeNames.Audio.Aac).ToLower() },
        { MimeTypeNames.Image.Gif, nameof(MimeTypeNames.Image.Gif).ToLower() },
        { MimeTypeNames.Image.Jpg, nameof(MimeTypeNames.Image.Jpg).ToLower() },
        { MimeTypeNames.Image.Png, nameof(MimeTypeNames.Image.Png).ToLower() },
        { MimeTypeNames.Image.Svg, nameof(MimeTypeNames.Image.Svg).ToLower() },
        { MimeTypeNames.Image.Tiff, nameof(MimeTypeNames.Image.Tiff).ToLower() },
        { MimeTypeNames.Image.Webp, nameof(MimeTypeNames.Image.Webp).ToLower() },
        { MimeTypeNames.Text.Html, nameof(MimeTypeNames.Text.Html).ToLower() },
        { MimeTypeNames.Text.Txt, nameof(MimeTypeNames.Text.Txt).ToLower() },
        { MimeTypeNames.Text.Xml, nameof(MimeTypeNames.Text.Xml).ToLower() },
        { MimeTypeNames.Video.Mp4, nameof(MimeTypeNames.Video.Mp4).ToLower() },
        { MimeTypeNames.Video.Mpeg, nameof(MimeTypeNames.Video.Mpeg).ToLower() },
        { MimeTypeNames.Video.Webm, nameof(MimeTypeNames.Video.Webm).ToLower() },
    };

    private static readonly IDictionary<string, string> Types = new Dictionary<string, string>
    {
        { nameof(MimeTypeNames.Application.Doc).ToLower(), MimeTypeNames.Application.Doc },
        { nameof(MimeTypeNames.Application.Docx).ToLower(), MimeTypeNames.Application.Docx },
        { string.Empty, MimeTypeNames.Application.Octet },
        { nameof(MimeTypeNames.Application.Pdf).ToLower(), MimeTypeNames.Application.Pdf },
        { nameof(MimeTypeNames.Application.Ppt).ToLower(), MimeTypeNames.Application.Ppt },
        { nameof(MimeTypeNames.Application.Pptx).ToLower(), MimeTypeNames.Application.Pptx },
        { nameof(MimeTypeNames.Application.Xls).ToLower(), MimeTypeNames.Application.Xls },
        { nameof(MimeTypeNames.Application.Xlsx).ToLower(), MimeTypeNames.Application.Xlsx },
        { nameof(MimeTypeNames.Audio.Mp3).ToLower(), MimeTypeNames.Audio.Mp3 },
        { nameof(MimeTypeNames.Audio.Wav).ToLower(), MimeTypeNames.Audio.Wav },
        { nameof(MimeTypeNames.Audio.Weba).ToLower(), MimeTypeNames.Audio.Weba },
        { nameof(MimeTypeNames.Audio.Aac).ToLower(), MimeTypeNames.Audio.Aac },
        { nameof(MimeTypeNames.Image.Gif).ToLower(), MimeTypeNames.Image.Gif },
        { nameof(MimeTypeNames.Image.Jpeg).ToLower(), MimeTypeNames.Image.Jpeg },
        { nameof(MimeTypeNames.Image.Jpg).ToLower(), MimeTypeNames.Image.Jpg },
        { nameof(MimeTypeNames.Image.Png).ToLower(), MimeTypeNames.Image.Png },
        { nameof(MimeTypeNames.Image.Svg).ToLower(), MimeTypeNames.Image.Svg },
        { nameof(MimeTypeNames.Image.Tiff).ToLower(), MimeTypeNames.Image.Tiff },
        { nameof(MimeTypeNames.Image.Webp).ToLower(), MimeTypeNames.Image.Webp },
        { nameof(MimeTypeNames.Text.Html).ToLower(), MimeTypeNames.Text.Html },
        { nameof(MimeTypeNames.Text.Txt).ToLower(), MimeTypeNames.Text.Txt },
        { nameof(MimeTypeNames.Text.Xml).ToLower(), MimeTypeNames.Text.Xml },
        { nameof(MimeTypeNames.Video.Mp4).ToLower(), MimeTypeNames.Video.Mp4 },
        { nameof(MimeTypeNames.Video.Mpeg).ToLower(), MimeTypeNames.Video.Mpeg },
        { nameof(MimeTypeNames.Video.Webm).ToLower(), MimeTypeNames.Video.Webm },
    };

    /// <summary>
    /// Get the extension for given mime type name.
    /// </summary>
    /// <param name="mimeTypeName">Mime type name.</param>
    /// <returns>File extension without the dot.</returns>
    public static string GetExtension(string mimeTypeName)
    {
        Extensions.TryGetValue(mimeTypeName, out string? extension);
        return extension ?? string.Empty;
    }

    /// <summary>
    /// Get the mime type for given extension.
    /// </summary>
    /// <param name="extension">Extension name. Can be passed with or without the dot.</param>
    /// <returns>Mime type name.</returns>
    public static string GetMimeType(string extension)
    {
        Types.TryGetValue(extension.Replace(".", string.Empty), out string? mimeType);
        return mimeType ?? string.Empty;
    }
}

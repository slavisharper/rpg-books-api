namespace RpgBooks.Libraries.Module.Application.Services.Email;

using System;
using System.File;
using System.IO;
using System.Net.Mime;
using System.Text.Json.Serialization;

/// <summary>
/// Main email attachment implementation.
/// </summary>
public sealed class EmailAttachment
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAttachment"/> class.
    /// </summary>
    /// <param name="filePath">Full file location.</param>
    /// <param name="isInline">Determine the attachment disposition.</param>
    public EmailAttachment(string filePath, bool isInline = false)
    {
        var fileInfo = new FileInfo(filePath);
        using var fileStream = fileInfo.OpenRead();
        using var memoryStream = new MemoryStream();
        fileStream.CopyTo(memoryStream);

        this.Name = fileInfo.Name;
        this.Content = memoryStream.ToArray();
        this.ContentId = $"<{Path.GetFileNameWithoutExtension(fileInfo.Name)}>";
        this.Type = MimeTypeHelpers.GetMimeType(Path.GetExtension(fileInfo.Name));
        this.Disposition = isInline ? DispositionTypeNames.Inline : DispositionTypeNames.Attachment;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAttachment"/> class.
    /// </summary>
    /// <param name="name">Attachment name.</param>
    /// <param name="content">Attachment content.</param>
    public EmailAttachment(string name, byte[] content)
    {
        this.Name = name;
        this.Content = content;
        this.ContentId = $"<{Path.GetFileNameWithoutExtension(name)}>";

        string extension = Path.GetExtension(name);
        if (extension is not null)
        {
            this.Type = MimeTypeHelpers.GetMimeType(extension);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAttachment"/> class.
    /// </summary>
    /// <param name="name">Attachment name.</param>
    /// <param name="content">Attachment content.</param>
    /// <param name="type">Attachment MIME type.</param>
    public EmailAttachment(string name, byte[] content, string? type)
        : this(name, content)
    {
        this.Type = type;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAttachment"/> class.
    /// </summary>
    /// <param name="name">Attachment name.</param>
    /// <param name="content">Attachment content.</param>
    /// <param name="type">Attachment MIME type.</param>
    /// <param name="disposition">Attachment disposition type. Either "attachment" or "inline" ("attachment" by default).</param>
    /// <param name="contentId">Id used in the email body for displaying the attachment(cid).</param>
    [JsonConstructor]
    public EmailAttachment(string name, byte[] content, string? type, string? disposition, string? contentId)
        : this(name, content, type)
    {
        this.Disposition = disposition;
        this.ContentId = contentId;
    }

    /// <summary>
    /// Gets attachment name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets attachment type. For example application/pdf, image/jpeg, etc.
    /// </summary>
    public string? Type { get; private set; }

    /// <summary>
    /// Gets the disposition type. Either "attachment" or "inline".
    /// </summary>
    public string? Disposition { get; private set; }

    /// <summary>
    /// Gets content id for cid resources shown in the email body.
    /// </summary>
    public string? ContentId { get; private set; }

    /// <summary>
    /// Gets attachment content as byte array.
    /// </summary>
    public byte[] Content { get; private set; }

    /// <summary>
    /// Create in-line attachment.
    /// </summary>
    /// <param name="name">Attachment name.</param>
    /// <param name="content">Attachment content.</param>
    /// <param name="type">Attachment MYME type.</param>
    /// <param name="contentId">Attachment cid.</param>
    /// <returns>New email attachment.</returns>
    public static EmailAttachment CreateInlineAttachment(string name, byte[] content, string? type, string contentId)
        => new(name, content, type, EmailDispositionTypes.Inline, contentId);

    /// <summary>
    /// Get content stream.
    /// </summary>
    /// <returns>Byte content as stream.</returns>
    public Stream GetContentAsStream()
        => new MemoryStream(this.Content);

    /// <summary>
    /// Get content as Base64 string.
    /// </summary>
    /// <returns>Attachment content as Base64 string.</returns>
    public string GetContentAsBase64String()
        => Convert.ToBase64String(this.Content);
}

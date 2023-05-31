namespace RpgBooks.Libraries.Module.Application.Services.Email;

/// <summary>
/// Constant values for email attachment disposition types.
/// </summary>
public static class EmailDispositionTypes
{
    /// <summary>
    /// Default disposition type. Used for attachments that are not visible in the email body.
    /// For example PDF file.
    /// </summary>
    public const string Attachment = "attachment";

    /// <summary>
    /// In-line disposition type is used for attachments that are displayed in the email body.
    /// For example images.
    /// </summary>
    public const string Inline = "inline";
}

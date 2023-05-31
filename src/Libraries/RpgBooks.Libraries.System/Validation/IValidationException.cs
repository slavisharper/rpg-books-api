namespace System;

/// <summary>
/// Validation exception interface.
/// </summary>
public interface IValidationException
{
    /// <summary>
    /// Gets or sets the Exception's Message.
    /// </summary>
    string ValidationMessage { get; set; }
}

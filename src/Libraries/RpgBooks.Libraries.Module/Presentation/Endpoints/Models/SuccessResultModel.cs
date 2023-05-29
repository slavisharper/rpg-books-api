namespace RpgBooks.Libraries.Module.Presentation.Endpoints.Models;

/// <summary>
/// Success result model. This is the actual API response model translated from the application layer.
/// </summary>
public record SuccessResultModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SuccessResultModel"/> class.
    /// </summary>
    public SuccessResultModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SuccessResultModel"/> class.
    /// </summary>
    /// <param name="message">Success message.</param>
    public SuccessResultModel(string message)
    {
        this.Message = message;
    }

    /// <summary>
    /// Gets or sets result message.
    /// </summary>
    public string Message { get; set; } = default!;
}

/// <summary>
/// Success result model with data. This is the actual API response model translated from the application layer.
/// </summary>
/// <typeparam name="TData">Type of the response data.</typeparam>
public sealed record SuccessResultModel<TData>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SuccessResultModel{TData}"/> class.
    /// </summary>
    public SuccessResultModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SuccessResultModel{TData}"/> class.
    /// </summary>
    /// <param name="message">Success message.</param>
    /// <param name="data">Response data.</param>
    public SuccessResultModel(string message, TData? data)
    {
        this.Message = message;
        this.Data = data;
    }

    /// <summary>
    /// Gets or sets result message.
    /// </summary>
    public string Message { get; set; } = default!;

    /// <summary>
    /// Gets or sets result data.
    /// </summary>
    public TData? Data { get; set; }
}

namespace RpgBooks.Libraries.Module.Presentation.Endpoints.Extensions;

using Microsoft.AspNetCore.Http;

using RpgBooks.Libraries.Module.Application.Results;
using RpgBooks.Libraries.Module.Application.Results.Contracts;
using RpgBooks.Libraries.Module.Application.Services.FileStorage;
using RpgBooks.Libraries.Module.Presentation.Endpoints.Models;

/// <summary>
/// Extensions used for transforming application results to an API responses.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Converts application <see cref="IAppResult{TData}"/> to IResult API response.
    /// </summary>
    /// <typeparam name="TData">Type of response data.</typeparam>
    /// <param name="resultTask">Task returned from the command handler.</param>
    /// <returns>Task with Generated API response.</returns>
    public static async Task<IResult> ToIResult<TData>(this Task<IAppResult<TData>> resultTask)
        => (await resultTask).ToIResult();

    /// <summary>
    /// Converts application <see cref="IAppResult{TResponseData}"/> to IResult API response.
    /// </summary>
    /// <param name="result">Task returned from the command handler.</param>
    /// <returns>Task with Generated API response.</returns>
    public static IResult ToIResult<TData>(this IAppResult<TData> result)
    {
        if (result.IsSuccess)
        {
            if (result.Data is FileModel)
            {
                var file = result.Data as FileModel;
                return Results.File(file!.Data, file!.ContentType, file!.Name);
            }

            return Results.Ok(new SuccessResultModel<TData>(result.Message, result.Data));
        }

        if (result.FailureReason!.Equals(FailureReason.ValidationFailed))
        {
            return Results.BadRequest(new ErrorResultModel(result.Message, result.Errors));
        }

        if (result.FailureReason!.Equals(FailureReason.NotFound))
        {
            return Results.NotFound(new ErrorResultModel(result.Message, result.Errors));
        }

        if (result.FailureReason!.Equals(FailureReason.Unauthorized))
        {
            return Results.Unauthorized();
        }

        return Results.Problem();
    }

    /// <summary>
    /// Converts application <see cref="IAppResult"/> to IResult API response.
    /// </summary>
    /// <param name="resultTask">Task returned from the command handler.</param>
    /// <returns>Task with Generated API response.</returns>
    public static async Task<IResult> ToIResult(this Task<IAppResult> resultTask)
        => (await resultTask).ToIResult();

    /// <summary>
    /// Converts application <see cref="IAppResult"/> to IResult API response.
    /// </summary>
    /// <param name="result">Task returned from the command handler.</param>
    /// <returns>Task with Generated API response.</returns>
    public static IResult ToIResult(this IAppResult result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok(new SuccessResultModel(result.Message));
        }

        if (result.FailureReason!.Equals(FailureReason.ValidationFailed))
        {
            return Results.BadRequest(new ErrorResultModel(result.Message, result.Errors));
        }

        if (result.FailureReason!.Equals(FailureReason.NotFound))
        {
            return Results.NotFound(new ErrorResultModel(result.Message, result.Errors));
        }

        if (result.FailureReason!.Equals(FailureReason.Unauthorized))
        {
            return Results.Unauthorized();
        }

        return Results.Problem();
    }

    /// <summary>
    /// Converts application data response to IResult API response.
    /// </summary>
    /// <typeparam name="TData">Type of the returned data.</typeparam>
    /// <param name="resultTask">Task with the returned data.</param>
    /// <returns>Task with Generated API response.</returns>
    public static async Task<IResult> ToIResult<TData>(this Task<TData> resultTask)
        => Results.Ok(await resultTask);
}

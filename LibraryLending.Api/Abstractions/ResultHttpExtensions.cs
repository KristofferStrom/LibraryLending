using LibraryLending.SharedKernel.Results;
using Microsoft.AspNetCore.Mvc;

namespace LibraryLending.Api.Abstractions;


public static class ResultHttpExtensions
{
    public static IResult ToHttp<T>(this Result<T> result, Func<T, object?> mapValue)
    {
        if (result.IsSuccess)
            return Results.Ok(mapValue(result.Value));

        return result.Error.ToHttp();
    }

    public static IResult ToHttp(this Result result)
    {
        if (result.IsSuccess)
            return Results.NoContent();

        return result.Error.ToHttp();
    }

    public static IResult ToHttp(this Error error)
    {
        var status = MapStatusCode(error.Type);

        var problem = new ProblemDetails
        {
            Status = status,
            Title = error.Type.ToString(),
            Detail = error.Message,
            Type = $"https://httpstatuses.com/{status}"
        };

        return Results.Problem(problem);
    }

    private static int MapStatusCode(ErrorType type) =>
        type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
}

namespace LibraryLending.SharedKernel.Results;

public sealed record Error(ErrorType Type, string Message)
{
    public static readonly Error None = new(ErrorType.None, string.Empty);
}

public enum ErrorType
{
    None,
    Validation,
    NotFound,
    Conflict,
    InternalServerError
}

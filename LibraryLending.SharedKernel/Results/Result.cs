namespace LibraryLending.SharedKernel.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
            throw new InvalidOperationException("Ett lyckat resultat kan inte ha ett error");

        if (!isSuccess && error == Error.None)
            throw new InvalidOperationException("Ett misslyckat resultat måste ha ett error");

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) =>
        new(false, error);

    public static implicit operator Result(Error error) => Failure(error);

}

public sealed class Result<T> : Result
{
    public T Value { get; }

    private Result(bool isSuccess, T value, Error error) : base(isSuccess, error)
        => Value = value;

    public static Result<T> Success(T value) =>
        new(true, value, Error.None);

    public static new Result<T> Failure(Error error) =>
        new(false, default!, error);

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
}
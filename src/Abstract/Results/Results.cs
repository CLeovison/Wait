namespace Wait.Abstract.Results;

public sealed record Error(string Code, string? Description = null)
{
    public static readonly Error None = new(string.Empty);
    public static implicit operator Results(Error error) => Results.Failure(error);
}

public sealed class Results
{
    private Results(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid Error", nameof(error));
        }
        IsSuccess = isSuccess;
        Error = error;
    }


    public bool IsSuccess { get; }
    public bool IsError => !IsSuccess;
    public Error Error { get; }

    public static Results Success() => new(true, Error.None);
    public static Results Failure(Error error) => new(false, error);
}
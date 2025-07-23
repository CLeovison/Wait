namespace Wait.Abstract.Results;

public sealed class Results
{
    public bool IsSuccess { get; }
    public bool IsError => !IsSuccess;
}
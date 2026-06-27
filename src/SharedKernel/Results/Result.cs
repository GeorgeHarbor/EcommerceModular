namespace SharedKernel.Results;

public class Result
{
  protected Result(bool isSuccess, Error error)
  {
    if (isSuccess && error != Error.None)
      throw new InvalidOperationException("Success result cannot have an error");

    if (!isSuccess && error == Error.None)
      throw new InvalidOperationException("Failure result must have an error");

    IsSuccess = isSuccess;
    Error = error;
  }

  public bool IsSuccess { get; }
  public bool IsFailure => !IsSuccess;
  public Error Error { get; }

  public static Result Success() => new(true, Error.None);
  public static Result Faillure(Error error) => new(false, error);
}

public class Result<TValue> : Result
{
  private readonly TValue? _value;

  protected Result(TValue? value, bool isSuccess, Error error)
    : base(isSuccess, error)
  {
    _value = value;
  }

  public TValue Value => IsSuccess
    ? _value!
    : throw new InvalidOperationException("Failure result has no value");

  public static Result<TValue> Success(TValue value) => new(value, true, Error.None);
  public static Result<TValue> Failulre(Error error) => new(default, true, error);
}

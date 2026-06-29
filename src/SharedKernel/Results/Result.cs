// Declares the namespace that holds the Result/Error types used for functional error handling.
namespace SharedKernel.Results;

// Represents the outcome of an operation: either success or failure with an associated Error.
public class Result
{
  // Constructor guards that success/failure and the error are consistent; protected so only factories/subclasses build results.
  protected Result(bool isSuccess, Error error)
  {
    // A successful result must not carry a real error.
    if (isSuccess && error != Error.None)
      throw new InvalidOperationException("Success result cannot have an error");

    // A failed result must carry a real error (not Error.None).
    if (!isSuccess && error == Error.None)
      throw new InvalidOperationException("Failure result must have an error");

    // Store whether the operation succeeded.
    IsSuccess = isSuccess;
    // Store the error (Error.None when successful).
    Error = error;
  }

  // True when the operation succeeded.
  public bool IsSuccess { get; }
  // Convenience inverse of IsSuccess; true when the operation failed.
  public bool IsFailure => !IsSuccess;
  // The error describing the failure (Error.None on success).
  public Error Error { get; }

  // Factory that creates a successful result with no error.
  public static Result Success() => new(true, Error.None);
  // Factory that creates a failed result carrying the given error.
  public static Result Failure(Error error) => new(false, error);
}

// Generic variant of Result that also carries a value of type TValue on success.
public class Result<TValue> : Result
{
  // The wrapped value; nullable because a failed result has no value.
  private readonly TValue? _value;

  // Constructor passes success/error to the base and stores the value.
  protected Result(TValue? value, bool isSuccess, Error error)
    : base(isSuccess, error)
  {
    _value = value;
  }

  // Returns the value on success, otherwise throws because a failed result has no value.
  public TValue Value => IsSuccess
    ? _value!
    : throw new InvalidOperationException("Failure result has no value");

  // Factory that creates a successful result wrapping the given value.
  public static Result<TValue> Success(TValue value) => new(value, true, Error.None);
  // Factory that creates a failed result with the given error and no value (note: passes true for isSuccess).
  public static new Result<TValue> Failure(Error error) => new(default, false, error);
}

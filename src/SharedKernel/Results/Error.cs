// Declares the namespace that holds the Result/Error types used for functional error handling.
namespace SharedKernel.Results;

// Immutable record representing an error with a machine-readable Code and a human-readable Description.
public record Error(string Code, string Description)
{
  // Represents "no error"; used by successful results (empty code and description).
  public static readonly Error None = new(string.Empty, string.Empty);

  // Predefined error used when a value that should not be null turns out to be null.
  public static readonly Error NullValue = new("Error.NullValue", "Value was null");
}

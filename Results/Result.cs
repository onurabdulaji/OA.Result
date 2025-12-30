namespace OA.Result.Results;

/// <summary>
///     Represents the result of an operation.
/// </summary>
public sealed class Result
{
    private static readonly Error[] NoErrors = [];

    private Result(bool isSuccess, IReadOnlyList<Error> errors)
    {
        if (isSuccess && errors.Count != 0)
            throw new InvalidOperationException("A successful result cannot have errors.");

        if (!isSuccess && errors.Count == 0)
            throw new InvalidOperationException("A failed result must have at least one error.");

        IsSuccess = isSuccess;
        Errors = errors;
    }

    /// <summary>
    ///     Indicates whether the result is successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    ///     Indicates whether the result is a failure.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    ///     The errors associated with the result.
    /// </summary>
    public IReadOnlyList<Error> Errors { get; }

    /// <summary>
    ///     The primary error associated with the result.
    /// </summary>
    public Error? PrimaryError => Errors.Count > 0 ? Errors[0] : null;

    /// <summary>
    ///     Creates a successful result.
    /// </summary>
    public static Result Ok()
    {
        return new Result(true, NoErrors);
    }

    /// <summary>
    ///     Creates a failed result with a single error.
    /// </summary>
    public static Result Fail(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);
        return new Result(false, new[] { error });
    }

    /// <summary>
    ///     Creates a failed result with multiple errors.
    /// </summary>
    public static Result Fail(IEnumerable<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        var list = errors as Error[] ?? errors.ToArray();
        if (list.Length == 0) throw new ArgumentException("At least one error is required.", nameof(errors));

        return new Result(false, list);
    }

    /// <summary>
    ///     Combines multiple results into a single result.
    /// </summary>
    public static Result Combine(params Result[] results)
    {
        ArgumentNullException.ThrowIfNull(results);
        if (results.Length == 0) return Ok();

        var allErrors = results
            .Where(r => r.IsFailure)
            .SelectMany(r => r.Errors)
            .ToArray();

        return allErrors.Length == 0 ? Ok() : Fail(allErrors);
    }

    /// <summary>
    ///     Deconstructs the result into its success and error components.
    /// </summary>
    public void Deconstruct(out bool isSuccess, out IReadOnlyList<Error> errors)
    {
        isSuccess = IsSuccess;
        errors = Errors;
    }

    /// <summary>
    ///     Creates a successful result with a value.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return IsSuccess ? "Result: Success" : $"Result: Failure ({Errors.Count} error(s))";
    }
}
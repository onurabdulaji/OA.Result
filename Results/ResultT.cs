namespace OA.Result.Results;

/// <summary>
///     Represents the result of an operation with a value.
/// </summary>
public sealed class Result<T>
{
    private static readonly Error[] NoErrors = [];

    private readonly T? _value;

    private Result(bool isSuccess, T? value, IReadOnlyList<Error> errors)
    {
        if (isSuccess && errors.Count != 0)
            throw new InvalidOperationException("A successful result cannot have errors.");

        if (!isSuccess && errors.Count == 0)
            throw new InvalidOperationException("A failed result must have at least one error.");

        IsSuccess = isSuccess;
        _value = value;
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
    ///     The value associated with the result.
    /// </summary>
    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access Value for a failed result.");

    /// <summary>
    ///     Creates a successful result with a value.
    /// </summary>
    public static Result<T> Ok(T value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        return new Result<T>(true, value, NoErrors);
    }

    /// <summary>
    ///     Creates a failed result with a single error.
    /// </summary>
    public static Result<T> Fail(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);
        return new Result<T>(false, default, new[] { error });
    }

    /// <summary>
    ///     Creates a failed result with multiple errors.
    /// </summary>
    public static Result<T> Fail(IEnumerable<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        var list = errors as Error[] ?? errors.ToArray();
        if (list.Length == 0) throw new ArgumentException("At least one error is required.", nameof(errors));

        return new Result<T>(false, default, list);
    }

    /// <summary>
    ///     Attempts to get the value from the result.
    /// </summary>
    public bool TryGetValue(out T? value)
    {
        value = _value;
        return IsSuccess;
    }

    /// <summary>
    ///     Deconstructs the result into its success, value, and error components.
    /// </summary>
    public void Deconstruct(out bool isSuccess, out T? value, out IReadOnlyList<Error> errors)
    {
        isSuccess = IsSuccess;
        value = _value;
        errors = Errors;
    }

    /// <summary>
    ///     Creates a successful result with a value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static implicit operator Result<T>(T value)
    {
        return Ok(value);
    }

    /// <summary>
    ///     Creates a failed result with a single error.
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static implicit operator Result<T>(Error error)
    {
        return Fail(error);
    }

    /// <summary>
    ///     Creates a failed result with multiple errors.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return IsSuccess
            ? $"Result<{typeof(T).Name}>: Success"
            : $"Result<{typeof(T).Name}>: Failure ({Errors.Count} error(s))";
    }
}
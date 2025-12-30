using OA.Result.Results;

namespace OA.Result;

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

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public IReadOnlyList<Error> Errors { get; }
    public Error? PrimaryError => Errors.Count > 0 ? Errors[0] : null;

    public static Result Ok()
    {
        return new Result(true, NoErrors);
    }

    public static Result Fail(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);
        return new Result(false, new[] { error });
    }

    public static Result Fail(IEnumerable<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        var list = errors as Error[] ?? errors.ToArray();
        if (list.Length == 0) throw new ArgumentException("At least one error is required.", nameof(errors));

        return new Result(false, list);
    }

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

    public void Deconstruct(out bool isSuccess, out IReadOnlyList<Error> errors)
    {
        isSuccess = IsSuccess;
        errors = Errors;
    }

    public override string ToString()
    {
        return IsSuccess ? "Result: Success" : $"Result: Failure ({Errors.Count} error(s))";
    }
}

// Generic

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

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public IReadOnlyList<Error> Errors { get; }

    public Error? PrimaryError => Errors.Count > 0 ? Errors[0] : null;

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access Value for a failed result.");

    public static Result<T> Ok(T value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        return new Result<T>(true, value, NoErrors);
    }

    public static Result<T> Fail(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);
        return new Result<T>(false, default, new[] { error });
    }

    public static Result<T> Fail(IEnumerable<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        var list = errors as Error[] ?? errors.ToArray();
        if (list.Length == 0) throw new ArgumentException("At least one error is required.", nameof(errors));

        return new Result<T>(false, default, list);
    }

    public bool TryGetValue(out T? value)
    {
        value = _value;
        return IsSuccess;
    }

    public void Deconstruct(out bool isSuccess, out T? value, out IReadOnlyList<Error> errors)
    {
        isSuccess = IsSuccess;
        value = _value;
        errors = Errors;
    }

    public static implicit operator Result<T>(T value)
    {
        return Ok(value);
    }

    public static implicit operator Result<T>(Error error)
    {
        return Fail(error);
    }

    public override string ToString()
    {
        return IsSuccess
            ? $"Result<{typeof(T).Name}>: Success"
            : $"Result<{typeof(T).Name}>: Failure ({Errors.Count} error(s))";
    }
}
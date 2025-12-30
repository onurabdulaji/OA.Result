using OA.Result.Results;

namespace OA.Result;

public sealed record Error
{
    private static readonly IReadOnlyDictionary<string, object?> EmptyMetadata = new Dictionary<string, object?>(0);

    private Error(
        string code,
        string message,
        ErrorType type,
        IReadOnlyDictionary<string, object?>? metadata)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Error code is required.", nameof(code));
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Error message is required.", nameof(message));

        Code = code;
        Message = message;
        Type = type;
        Metadata = metadata ?? EmptyMetadata;
    }

    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }
    public IReadOnlyDictionary<string, object?> Metadata { get; }

    public static Error Create(
        string code,
        string message,
        ErrorType type = ErrorType.Failure,
        IReadOnlyDictionary<string, object?>? metadata = null)
    {
        return new Error(code, message, type, metadata);
    }

    public static Error Failure(string code, string message, IReadOnlyDictionary<string, object?>? metadata = null)
    {
        return new Error(code, message, ErrorType.Failure, metadata);
    }

    public static Error Validation(string code, string message, string? field = null)
    {
        return new Error(
            code,
            message,
            ErrorType.Validation,
            field is null ? null : new Dictionary<string, object?> { ["field"] = field });
    }

    public static Error NotFound(string code, string message, IReadOnlyDictionary<string, object?>? metadata = null)
    {
        return new Error(code, message, ErrorType.NotFound, metadata);
    }

    public static Error Conflict(string code, string message, IReadOnlyDictionary<string, object?>? metadata = null)
    {
        return new Error(code, message, ErrorType.Conflict, metadata);
    }

    public static Error Unauthorized(string code, string message = "Unauthorized.")
    {
        return new Error(code, message, ErrorType.Unauthorized, null);
    }

    public static Error Forbidden(string code, string message = "Forbidden.")
    {
        return new Error(code, message, ErrorType.Forbidden, null);
    }

    public static Error Unexpected(string code, string message, IReadOnlyDictionary<string, object?>? metadata = null)
    {
        return new Error(code, message, ErrorType.Unexpected, metadata);
    }

    public static Error Internal(string code, string message = "Internal error.")
    {
        return new Error(code, message, ErrorType.Internal, null);
    }

    public static Error Timeout(string code, string message = "Request timeout.")
    {
        return new Error(code, message, ErrorType.Timeout, null);
    }

    public static Error RateLimit(string code, string message = "Rate limit exceeded.")
    {
        return new Error(code, message, ErrorType.RateLimit, null);
    }

    public static Error Invalid(string code, string message = "Invalid request.")
    {
        return new Error(code, message, ErrorType.Invalid, null);
    }

    public static Error NotSupported(string code, string message = "Not supported.")
    {
        return new Error(code, message, ErrorType.NotSupported, null);
    }

    public static Error NotAllowed(string code, string message = "Not allowed.")
    {
        return new Error(code, message, ErrorType.NotAllowed, null);
    }
}
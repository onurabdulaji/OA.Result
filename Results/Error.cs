namespace OA.Result.Results;

/// <summary>
///     Represents an error.
/// </summary>
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

    /// <summary>
    ///     The error code.
    /// </summary>
    public string Code { get; }

    /// <summary>
    ///     The error message.
    /// </summary>
    public string Message { get; }

    /// <summary>
    ///     The error type.
    /// </summary>
    public ErrorType Type { get; }

    /// <summary>
    ///     The error metadata.
    /// </summary>
    public IReadOnlyDictionary<string, object?> Metadata { get; }

    /// <summary>
    ///     Creates a new error.
    /// </summary>
    public static Error Create(
        string code,
        string message,
        ErrorType type = ErrorType.Failure,
        IReadOnlyDictionary<string, object?>? metadata = null)
    {
        return new Error(code, message, type, metadata);
    }

    /// <summary>
    ///     Creates a new failure error.
    /// </summary>
    public static Error Failure(string code, string message, IReadOnlyDictionary<string, object?>? metadata = null)
    {
        return new Error(code, message, ErrorType.Failure, metadata);
    }

    /// <summary>
    ///     Creates a new validation error.
    /// </summary>
    public static Error Validation(string code, string message, string? field = null)
    {
        return new Error(
            code,
            message,
            ErrorType.Validation,
            field is null ? null : new Dictionary<string, object?> { ["field"] = field });
    }

    /// <summary>
    ///     Creates a new not found error.
    /// </summary>
    public static Error NotFound(string code, string message, IReadOnlyDictionary<string, object?>? metadata = null)
    {
        return new Error(code, message, ErrorType.NotFound, metadata);
    }

    /// <summary>
    ///     Creates a new conflict error.
    /// </summary>
    public static Error Conflict(string code, string message, IReadOnlyDictionary<string, object?>? metadata = null)
    {
        return new Error(code, message, ErrorType.Conflict, metadata);
    }

    /// <summary>
    ///     Creates a new unauthorized error.
    /// </summary>
    public static Error Unauthorized(string code, string message = "Unauthorized.")
    {
        return new Error(code, message, ErrorType.Unauthorized, null);
    }

    /// <summary>
    ///     Creates a new forbidden error.
    /// </summary>
    public static Error Forbidden(string code, string message = "Forbidden.")
    {
        return new Error(code, message, ErrorType.Forbidden, null);
    }

    /// <summary>
    ///     Creates a new unexpected error.
    /// </summary>
    public static Error Unexpected(string code, string message, IReadOnlyDictionary<string, object?>? metadata = null)
    {
        return new Error(code, message, ErrorType.Unexpected, metadata);
    }

    /// <summary>
    ///     Creates a new internal error.
    /// </summary>
    public static Error Internal(string code, string message = "Internal error.")
    {
        return new Error(code, message, ErrorType.Internal, null);
    }

    /// <summary>
    ///     Creates a new timeout error.
    /// </summary>
    public static Error Timeout(string code, string message = "Request timeout.")
    {
        return new Error(code, message, ErrorType.Timeout, null);
    }

    /// <summary>
    ///     Creates a new rate limit error.
    /// </summary>
    public static Error RateLimit(string code, string message = "Rate limit exceeded.")
    {
        return new Error(code, message, ErrorType.RateLimit, null);
    }

    /// <summary>
    ///     Creates a new invalid request error.
    /// </summary>
    public static Error Invalid(string code, string message = "Invalid request.")
    {
        return new Error(code, message, ErrorType.Invalid, null);
    }

    /// <summary>
    ///     Creates a new not supported error.
    /// </summary>
    public static Error NotSupported(string code, string message = "Not supported.")
    {
        return new Error(code, message, ErrorType.NotSupported, null);
    }

    /// <summary>
    ///     Creates a new not allowed error.
    /// </summary>
    public static Error NotAllowed(string code, string message = "Not allowed.")
    {
        return new Error(code, message, ErrorType.NotAllowed, null);
    }
}
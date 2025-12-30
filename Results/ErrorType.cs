namespace OA.Result.Results;

/// <summary>
///     Represents the type of an error.
/// </summary>
public enum ErrorType
{
    /// <summary>
    ///     A failure error.
    /// </summary>
    Failure = 0,

    /// <summary>
    ///     A validation error.
    /// </summary>
    Validation = 1,

    /// <summary>
    ///     A not found error.
    /// </summary>
    NotFound = 2,

    /// <summary>
    ///     A conflict error.
    /// </summary>
    Conflict = 3,

    /// <summary>
    ///     An unauthorized error.
    /// </summary>
    Unauthorized = 4,

    /// <summary>
    ///     A forbidden error.
    /// </summary>
    Forbidden = 5,

    /// <summary>
    ///     A not supported error.
    /// </summary>
    NotSupported = 11,

    /// <summary>
    ///     A not allowed error.
    /// </summary>
    NotAllowed = 12,

    /// <summary>
    ///     An unexpected error.
    /// </summary>
    Unexpected = 13,

    /// <summary>
    ///     An internal error.
    /// </summary>
    Internal = 14,

    /// <summary>
    ///     A timeout error.
    /// </summary>
    Timeout = 15,

    /// <summary>
    ///     A rate limit error.
    /// </summary>
    RateLimit = 16,

    /// <summary>
    ///     An invalid request error.
    /// </summary>
    Invalid = 17
}
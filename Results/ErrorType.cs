namespace OA.Result;

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Conflict = 3,
    Unauthorized = 4,
    Forbidden = 5,
    Unexpected = 6,
    Internal = 7,
    Timeout = 8,
    RateLimit = 9,
    Invalid = 10,
    NotSupported = 11,
    NotAllowed = 12,
}
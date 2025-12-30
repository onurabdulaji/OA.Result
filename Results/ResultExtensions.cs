using OA.Result.Results;

namespace OA.Result;

public static class ResultExtensions
{
    // Non-generic flow
    public static Results.Result Tap(this Results.Result result, Action action)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(action);

        if (result.IsSuccess) action();
        return result;
    }

    public static Results.Result TapError(this Results.Result result, Action<IReadOnlyList<Error>> action)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(action);

        if (result.IsFailure) action(result.Errors);
        return result;
    }

    public static Results.Result Ensure(this Results.Result result, Func<bool> predicate, Error errorIfFalse)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(errorIfFalse);

        if (result.IsFailure) return result;
        return predicate() ? result : Results.Result.Fail(errorIfFalse);
    }

    // Generic flow
    public static Result<TResult> Map<T, TResult>(this Result<T> result, Func<T, TResult> mapper)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(mapper);

        if (result.IsFailure) return Result<TResult>.Fail(result.Errors);
        return Result<TResult>.Ok(mapper(result.Value));
    }

    public static Result<TResult> Bind<T, TResult>(this Result<T> result, Func<T, Result<TResult>> binder)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(binder);

        if (result.IsFailure) return Result<TResult>.Fail(result.Errors);
        return binder(result.Value);
    }

    public static Result<T> Tap<T>(this Result<T> result, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(action);

        if (result.IsSuccess) action(result.Value);
        return result;
    }

    public static Result<T> TapError<T>(this Result<T> result, Action<IReadOnlyList<Error>> action)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(action);

        if (result.IsFailure) action(result.Errors);
        return result;
    }

    public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Error errorIfFalse)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(errorIfFalse);

        if (result.IsFailure) return result;
        return predicate(result.Value) ? result : Result<T>.Fail(errorIfFalse);
    }

    public static TResult Match<T, TResult>(
        this Result<T> result,
        Func<T, TResult> onSuccess,
        Func<IReadOnlyList<Error>, TResult> onFailure)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Errors);
    }

    public static TResult Match<TResult>(
        this Results.Result result,
        Func<TResult> onSuccess,
        Func<IReadOnlyList<Error>, TResult> onFailure)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        return result.IsSuccess ? onSuccess() : onFailure(result.Errors);
    }
}
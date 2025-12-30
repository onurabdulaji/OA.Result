# OA.Result

A production-ready implementation of the Result pattern (`Result` and `Result<T>`) for .NET 9, designed for
domain-driven design (DDD) and building blocks. It supports error aggregation and functional programming helpers to
handle success/failure scenarios elegantly.

## Features

- **Result Types**: `Result` for void operations and `Result<T>` for typed results.
- **Error Aggregation**: Collect multiple errors in a single result.
- **Functional Helpers**: Methods like `Map`, `Bind`, and `Match` for chaining operations.
- **Immutable and Thread-Safe**: Built for concurrent environments.
- **NuGet Package**: Easy to integrate into .NET 9 projects.

## Installation

Install via NuGet:

```bash
dotnet add package OA.Result --version 1.0.0


using OA.Result;

// Success result
var success = Result.Success();
if (success.IsSuccess)
{
    Console.WriteLine("Operation succeeded!");
}

// Failure result with error
var failure = Result.Failure("Something went wrong");
if (failure.IsFailure)
{
    Console.WriteLine($"Error: {failure.Error}");
}

// Typed result
var result = Result<int>.Success(42);
var doubled = result.Map(x => x * 2); // Result<int> with 84

// Error aggregation
var errors = new List<string> { "Error 1", "Error 2" };
var aggregated = Result.Failure(errors);



// Bind for chaining
var chained = Result<int>.Success(10)
    .Bind(x => x > 5 ? Result<int>.Success(x * 2) : Result<int>.Failure("Too small"));

// Match for pattern matching
var message = result.Match(
    success: value => $"Success: {value}",
    failure: error => $"Failure: {error}"
);

API Reference
Result.Success(): Creates a success result.
Result.Failure(string error): Creates a failure result with a single error.
Result.Failure(IEnumerable<string> errors): Creates a failure result with multiple errors.
Result<T>.Success(T value): Creates a success result with a value.
Result<T>.Failure(string error): Creates a failure result with an error.
Map(Func<T, TResult> mapper): Transforms the value if successful.
Bind(Func<T, Result<TResult>> binder): Chains operations.
Match(Func<T, TResult> success, Func<string, TResult> failure): Pattern matches on success/failure.
For full API docs, see the generated XML documentation file.
Contributing
Contributions are welcome! Please submit issues or pull requests on GitHub.
License
MIT License. See LICENSE for details.


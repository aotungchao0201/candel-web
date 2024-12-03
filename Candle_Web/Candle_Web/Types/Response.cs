namespace Candle_Web.Types;


public record Response(
    int error,
    string message,
    object? data
);
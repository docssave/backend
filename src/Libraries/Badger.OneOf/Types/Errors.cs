namespace Badger.OneOf.Types;

public readonly struct NotFound<T>(T value)
{
    public T Value { get; } = value;
}

public struct Conflict;

public readonly struct Conflict<T>(T value)
{
    public T Value { get; } = value;
}

public struct Unreachable;

public readonly struct Unreachable<T>(T value)
{
    public T Value { get; } = value;
}

public readonly struct Unknown<T>(T value)
{
    public T Value { get; } = value;
}
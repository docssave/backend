namespace Badger.OneOf.Types;

public readonly struct NotFound<T>(T value)
{
    public T Value { get; } = value;
}

public struct Conflct;

public readonly struct Conflct<T>(T value)
{
    public T Value { get; } = value;
}

public struct Unreachable;

public readonly struct Unreachable<T>(T value)
{
    public T Value { get; } = value;
}

public struct Unknown;

public readonly struct Unknown<T>(T value)
{
    public T Value { get; } = value;
}
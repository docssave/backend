namespace Badger.Clock;

public interface IClock
{
    DateTimeOffset Now { get; }
}
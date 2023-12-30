namespace Badger.Clock;

public class ServerClock : IClock
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}
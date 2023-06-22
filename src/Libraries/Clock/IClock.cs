namespace Clock;

public interface IClock
{
    DateTimeOffset Now { get; }
}
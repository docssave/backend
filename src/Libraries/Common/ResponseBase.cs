namespace Common;

public abstract record ResponseBase(Error? Error)
{
    public bool IsSuccess => Error == null;
}
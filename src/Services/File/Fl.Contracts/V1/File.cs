namespace Fl.Contracts.V1;

public sealed class File(FileId id, byte[] content, string mimeType)
{
    public FileId Id { get; } = id;

    public byte[] Content { get; } = content;

    public string MimeType { get; } = mimeType;
}
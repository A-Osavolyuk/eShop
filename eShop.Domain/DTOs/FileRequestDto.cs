namespace eShop.Domain.DTOs;

public record FileRequestDto(
    FileData Data,
    HttpMethods Method,
    string Url);

public class FileData
{
    public IBrowserFile File { get; init; }
    public IReadOnlyList<IBrowserFile> Files { get; init; }

    public FileData(IReadOnlyList<IBrowserFile> files)
    {
        Files = files;
    }

    public FileData(IBrowserFile file)
    {
        File = file;
    }
}
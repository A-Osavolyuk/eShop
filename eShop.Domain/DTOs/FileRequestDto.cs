using eShop.Domain.Enums;
using Microsoft.AspNetCore.Components.Forms;

namespace eShop.Domain.DTOs;

public record FileRequestDto(
    FileData Data,
    HttpMethods Method,
    string Url);

public class FileData
{
    public IBrowserFile File { get; init; }
    public IReadOnlyList<IBrowserFile> Files { get; init; }
    public string StringData { get; init; }
    public string DataName { get; }

    public FileData(IReadOnlyList<IBrowserFile> files, string stringData, string dataName)
    {
        Files = files;
        StringData = stringData;
        DataName = dataName;
    }

    public FileData(IBrowserFile file, string stringData, string dataName)
    {
        File = file;
        StringData = stringData;
        DataName = dataName;
    }
}
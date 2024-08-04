public interface IFileTypeCheckerService
{
    string GetFileType(byte[] fileBytes, out List<string> secondaryResults);
}
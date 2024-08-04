using HeyRed.Mime;

namespace FileUploadSecurity;

public class MimeDetectiveFileChecker : IFileChecker
{
    public string CheckFileType(byte[] fileBytes)
    {
        var fileType = MimeGuesser.GuessFileType(fileBytes);
        return fileType.Extension.ToUpper();
    }
}

using System.Text;
using FileUploadSecurity;

public class TextFileChecker : FileCheckerBase
{
    public override string CheckFileType(byte[] fileBytes)
    {
        string content = Encoding.UTF8.GetString(fileBytes).Trim();
        return content.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || char.IsPunctuation(c)) ? "TEXT" : "UNKNOWN";
    }
}
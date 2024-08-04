using System.Text;
using FileUploadSecurity;

public class HtmlFileChecker : FileCheckerBase
{
    public override string CheckFileType(byte[] fileBytes)
    {
        string content = Encoding.UTF8.GetString(fileBytes).TrimStart();

        // Check if the file starts with '<!DOCTYPE html>' or '<html>'
        if (content.StartsWith("<!DOCTYPE html>", StringComparison.OrdinalIgnoreCase) ||
            content.StartsWith("<html>", StringComparison.OrdinalIgnoreCase))
            return "HTML";

        return "UNKNOWN";
    }
}


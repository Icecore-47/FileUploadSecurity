using System.Text;
using FileUploadSecurity;

public class JsonFileChecker : FileCheckerBase
{
    public override string CheckFileType(byte[] fileBytes)
    {
        string content = Encoding.UTF8.GetString(fileBytes).TrimStart();

        // Check if the file starts with '{' or '[' indicating a JSON file
        if (content.StartsWith("{") || content.StartsWith("["))
            return "JSON";

        return "UNKNOWN";
    }
}
using System.Text;
using FileUploadSecurity;

public class YamlFileChecker : FileCheckerBase
{
    public override string CheckFileType(byte[] fileBytes)
    {
        string fileContent = Encoding.UTF8.GetString(fileBytes);

        if (fileContent.TrimStart().StartsWith("---"))
        {
            return "YAML";
        }

        return "UNKNOWN";
    }
}
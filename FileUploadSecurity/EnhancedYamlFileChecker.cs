using System.Text;
using FileUploadSecurity;

public class EnhancedYamlFileChecker : FileCheckerBase
{
    private static readonly byte[] YAML_SIGNATURE = Encoding.UTF8.GetBytes("---");

    public override string CheckFileType(byte[] fileBytes)
    {
        if (fileBytes.Length < YAML_SIGNATURE.Length)
            return "UNKNOWN";

        // Check if the file starts with "---"
        if (StartsWith(fileBytes, YAML_SIGNATURE))
            return "YAML";

        // Additional checks can be added here
        return "UNKNOWN";
    }
}
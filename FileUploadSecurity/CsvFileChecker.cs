using System.Text;

namespace FileUploadSecurity;

public class CsvFileChecker : FileCheckerBase
{
    public override string CheckFileType(byte[] fileBytes)
    {
        string fileContent = Encoding.UTF8.GetString(fileBytes);

        // Check for presence of typical CSV structure: header and data rows
        var lines = fileContent.Split('\n');
        if (lines.Length > 1 && lines[0].Contains(",") && lines[1].Contains(","))
        {
            // Ensure it is not binary
            if (fileContent.All(c => c >= 32 || c == 9 || c == 10 || c == 13))
            {
                return "CSV";
            }
        }

        return "UNKNOWN";
    }
}
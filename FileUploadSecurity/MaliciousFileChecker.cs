using System.Text;

namespace FileUploadSecurity;

public class MaliciousFileChecker : FileCheckerBase
{
    private static readonly string[] MaliciousPatterns = {
        "<?php", "<script", "eval(", "document.cookie", "String.fromCharCode", "unescape(", "exec(", "base64_decode(", "system(", "shell_exec("
    };

    public override string CheckFileType(byte[] fileBytes)
    {
        string fileContent = Encoding.UTF8.GetString(fileBytes);
        return MaliciousPatterns.Any(pattern => fileContent.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0) ? "MALICIOUS" : "UNKNOWN";
    }
}
namespace FileUploadSecurity;

public class DocxFileChecker : FileCheckerBase
{
    private static readonly byte[] DOCX_SIGNATURE = { 0x50, 0x4B, 0x03, 0x04 }; // Same as ZIP

    public override string CheckFileType(byte[] fileBytes)
    {
        if (StartsWith(fileBytes, DOCX_SIGNATURE))
        {
            // Additional checks can be implemented here
            return "DOCX";
        }

        return "UNKNOWN";
    }
}
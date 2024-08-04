namespace FileUploadSecurity;

public class ZipFileChecker : FileCheckerBase
{
    private static readonly byte[] ZIP_SIGNATURE = { 0x50, 0x4B, 0x03, 0x04 };

    public override string CheckFileType(byte[] fileBytes)
    {
        return StartsWith(fileBytes, ZIP_SIGNATURE) ? "ZIP" : "UNKNOWN";
    }
}
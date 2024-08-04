namespace FileUploadSecurity;

public class SevenZFileChecker : FileCheckerBase
{
    private static readonly byte[] SEVEN_Z_SIGNATURE = { 0x37, 0x7A, 0xBC, 0xAF, 0x27, 0x1C };

    public override string CheckFileType(byte[] fileBytes)
    {
        return StartsWith(fileBytes, SEVEN_Z_SIGNATURE) ? "7Z" : "UNKNOWN";
    }
}
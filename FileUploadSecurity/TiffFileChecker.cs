using FileUploadSecurity;

public class TiffFileChecker : FileCheckerBase
{
    private static readonly byte[] TIFF_SIGNATURE = { 0x49, 0x49, 0x2A, 0x00 }; // Little-endian TIFF

    public override string CheckFileType(byte[] fileBytes)
    {
        return StartsWith(fileBytes, TIFF_SIGNATURE) ? "TIFF" : "UNKNOWN";
    }
}
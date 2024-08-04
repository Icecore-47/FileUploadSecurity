using FileUploadSecurity;

public class BmpFileChecker : FileCheckerBase
{
    private static readonly byte[] BMP_SIGNATURE = { 0x42, 0x4D }; // BM

    public override string CheckFileType(byte[] fileBytes)
    {
        return StartsWith(fileBytes, BMP_SIGNATURE) ? "BMP" : "UNKNOWN";
    }
}
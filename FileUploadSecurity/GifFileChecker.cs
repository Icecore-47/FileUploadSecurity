using FileUploadSecurity;

public class GifFileChecker : FileCheckerBase
{
    private static readonly byte[] GIF_SIGNATURE = { 0x47, 0x49, 0x46 }; // GIF

    public override string CheckFileType(byte[] fileBytes)
    {
        return StartsWith(fileBytes, GIF_SIGNATURE) ? "GIF" : "UNKNOWN";
    }
}
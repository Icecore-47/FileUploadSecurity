using FileUploadSecurity;

public class PowerPointFileChecker : FileCheckerBase
{
    private static readonly byte[] PPTX_SIGNATURE = { 0x50, 0x4B, 0x03, 0x04 }; // ZIP signature, specific to Office Open XML

    public override string CheckFileType(byte[] fileBytes)
    {
        return StartsWith(fileBytes, PPTX_SIGNATURE) ? "PPTX" : "UNKNOWN";
    }
}

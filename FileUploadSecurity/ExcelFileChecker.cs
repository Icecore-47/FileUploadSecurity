using FileUploadSecurity;

public class ExcelFileChecker : FileCheckerBase
{
    private static readonly byte[] XLSX_SIGNATURE = { 0x50, 0x4B, 0x03, 0x04 }; // ZIP signature, specific to Office Open XML

    public override string CheckFileType(byte[] fileBytes)
    {
        return StartsWith(fileBytes, XLSX_SIGNATURE) ? "XLSX" : "UNKNOWN";
    }
}
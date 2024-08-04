using FileUploadSecurity;

public class Mp4FileChecker : FileCheckerBase
{
    private static readonly byte[] MP4_SIGNATURE = { 0x00, 0x00, 0x00, 0x18, 0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32 }; // 'ftypmp42'

    public override string CheckFileType(byte[] fileBytes)
    {
        return StartsWith(fileBytes, MP4_SIGNATURE) ? "MP4" : "UNKNOWN";
    }
}
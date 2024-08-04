using FileUploadSecurity;
internal class Mp3FileChecker : FileCheckerBase
{
    private static readonly byte[] MP3_SIGNATURE = { 0x49, 0x44, 0x33 }; // ID3 tag

    public override string CheckFileType(byte[] fileBytes)
    {
        return StartsWith(fileBytes, MP3_SIGNATURE) ? "MP3" : "UNKNOWN";
    }
}
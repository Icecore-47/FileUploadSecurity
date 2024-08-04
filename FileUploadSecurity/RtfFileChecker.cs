using System.Text;

namespace FileUploadSecurity.FileUploadSecurity;

public class RtfFileChecker : FileCheckerBase
{
    private static readonly byte[] RTF_SIGNATURE = Encoding.ASCII.GetBytes("{\\rtf");

    public override string CheckFileType(byte[] fileBytes)
    {
        return StartsWith(fileBytes, RTF_SIGNATURE) ? "RTF" : "UNKNOWN";
    }
}

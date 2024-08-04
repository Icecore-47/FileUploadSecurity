using System.Text;

namespace FileUploadSecurity;

public class DllFileChecker : FileCheckerBase
{
    private static readonly byte[] DLL_SIGNATURE = { 0x4D, 0x5A }; // MZ Header

    public override string CheckFileType(byte[] fileBytes)
    {
        return StartsWith(fileBytes, DLL_SIGNATURE) ? "DLL" : "UNKNOWN";
    }
}
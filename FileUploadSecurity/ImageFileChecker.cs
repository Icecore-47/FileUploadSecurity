using System.Text;

namespace FileUploadSecurity;

public class ImageFileChecker : FileCheckerBase
{
    private static readonly byte[] JPEG_SIGNATURE = { 0xFF, 0xD8 };
    private static readonly byte[] PNG_SIGNATURE = { 0x89, 0x50, 0x4E, 0x47 };
    private static readonly byte[] GIF87A_SIGNATURE = Encoding.ASCII.GetBytes("GIF87a");
    private static readonly byte[] GIF89A_SIGNATURE = Encoding.ASCII.GetBytes("GIF89a");
    private static readonly byte[] BMP_SIGNATURE = { 0x42, 0x4D };

    public override string CheckFileType(byte[] fileBytes)
    {
        if (StartsWith(fileBytes, JPEG_SIGNATURE) ||
            StartsWith(fileBytes, PNG_SIGNATURE) ||
            StartsWith(fileBytes, GIF87A_SIGNATURE) ||
            StartsWith(fileBytes, GIF89A_SIGNATURE) ||
            StartsWith(fileBytes, BMP_SIGNATURE))
        {
            return "IMAGE";
        }

        return "UNKNOWN";
    }
}
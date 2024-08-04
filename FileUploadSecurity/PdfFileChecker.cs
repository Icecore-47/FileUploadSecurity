using System.Text;

namespace FileUploadSecurity;

public class PdfFileChecker : FileCheckerBase
{
    private static readonly byte[] PDF_SIGNATURE = Encoding.ASCII.GetBytes("%PDF-");

    public override string CheckFileType(byte[] fileBytes)
    {
        return StartsWith(fileBytes, PDF_SIGNATURE) ? "PDF" : "UNKNOWN";
    }
}
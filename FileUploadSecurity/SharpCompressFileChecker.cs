using SharpCompress.Readers;

namespace FileUploadSecurity.FileUploadSecurity.FileUploadSecurity.FileUploadSecurity;

public class SharpCompressFileChecker : IFileChecker
{
    public string CheckFileType(byte[] fileBytes)
    {
        using (var stream = new MemoryStream(fileBytes))
        {
            try
            {
                if (ReaderFactory.Open(stream) != null)
                {
                    return "COMPRESSED";
                }
            }
            catch
            {
                // Handle exceptions for non-compressed files
            }
        }
        return "UNKNOWN";
    }
}


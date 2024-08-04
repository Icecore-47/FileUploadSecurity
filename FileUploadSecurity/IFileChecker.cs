namespace FileUploadSecurity;

public interface IFileChecker
{
    string CheckFileType(byte[] fileBytes);
}
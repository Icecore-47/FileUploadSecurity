namespace FileUploadSecurity;

public abstract class FileCheckerBase : IFileChecker
{
    public abstract string CheckFileType(byte[] fileBytes);

    protected bool StartsWith(byte[] fileBytes, byte[] signature)
    {
        if (fileBytes.Length < signature.Length)
            return false;

        return !signature.Where((t, i) => fileBytes[i] != t).Any();
    }
}
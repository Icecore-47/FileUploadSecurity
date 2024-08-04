using System.Text;

namespace FileUploadSecurity;

public class ProgrammingLanguageFileChecker : FileCheckerBase
{
    private static readonly Dictionary<string, string[]> LanguageSignatures = new Dictionary<string, string[]>
    {
        { "C#", new[] { "using ", "namespace ", "class ", "public ", "private ", "static ", "void ", "int ", "string " } },
        { "Python", new[] { "import ", "def ", "class ", "if __name__ == '__main__':", "# ", "print(" } },
        { "Java", new[] { "import ", "public class ", "public static void main", "package " } },
        { "PHP", new[] { "<?php", "function ", "$", "echo ", "namespace ", "use " } },
        { "JavaScript", new[] { "function ", "var ", "let ", "const ", "document.", "window." } },
        { "C++", new[] { "#include", "int main", "std::", "namespace ", "class " } }
    };

    public override string CheckFileType(byte[] fileBytes)
    {
        string fileContent = Encoding.UTF8.GetString(fileBytes);

        foreach (var language in LanguageSignatures)
        {
            var matchedSignatures = language.Value.Count(signature => fileContent.Contains(signature));
            if (matchedSignatures > 1)
            {
                return language.Key;
            }
        }

        return "UNKNOWN";
    }
}
using Serilog;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileUploadSecurity;
using FileUploadSecurity.FileUploadSecurity.FileUploadSecurity.FileUploadSecurity;
using FileUploadSecurity.FileUploadSecurity;

namespace FileUploadSecurity
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Application starting up");

                var manualCheckers = new List<IFileChecker>
                {
                    new MaliciousFileChecker(), 
                    new CsvFileChecker(),
                    new ImageFileChecker(),
                    new PdfFileChecker(),
                    new ProgrammingLanguageFileChecker(),
                    new DllFileChecker(),
                    new YamlFileChecker(),
                    new ZipFileChecker(),
                    new DocxFileChecker(),
                    new RtfFileChecker(),
                    new SevenZFileChecker(),
                    new EnhancedYamlFileChecker(),
                    new JsonFileChecker(),
                    new HtmlFileChecker(),
                    new Mp3FileChecker(),
                    new Mp4FileChecker(),
                    new ExcelFileChecker(),
                    new PowerPointFileChecker(),
                    new TextFileChecker(),
                    new GifFileChecker(),
                    new TiffFileChecker(),
                    new BmpFileChecker()
                };

                var secondaryCheckers = new List<IFileChecker>
                {
                    new MimeDetectiveFileChecker(),
                    new SharpCompressFileChecker(),
                };

                var fileTypeCheckerService = new FileTypeCheckerService(manualCheckers, secondaryCheckers, Log.Logger);

                string directoryPath = @"C:\Users\TB\Desktop\Work\TestProjects\FileUploadSecurity\Files";
                string outputDirectory = @"C:\Users\TB\Desktop\Work\TestProjects\FileUploadSecurity\Output";
                string outputFilePath = Path.Combine(outputDirectory, "results.txt");

                // Ensure the output directory exists
                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                if (Directory.Exists(directoryPath))
                {
                    Log.Information("Processing files in directory: {DirectoryPath}", directoryPath);
                    string[] files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);

                    var exceptions = new ConcurrentQueue<Exception>();

                    Parallel.ForEach(files, (filePath) =>
                    {
                        try
                        {
                            // Read the file content
                            byte[] fileContent = File.ReadAllBytes(filePath);

                            // Convert to Base64 string
                            string base64Encoded = Convert.ToBase64String(fileContent);

                            ProcessFile(Path.GetFileName(filePath), base64Encoded, fileTypeCheckerService, outputFilePath);
                        }
                        catch (Exception ex)
                        {
                            exceptions.Enqueue(new Exception($"Error processing file {filePath}: {ex.Message}", ex));
                        }
                    });

                    if (exceptions.Count > 0)
                    {
                        foreach (var ex in exceptions)
                        {
                            WriteToFile(outputFilePath, ex.Message);
                        }
                    }
                }
                else
                {
                    Log.Error("The directory {DirectoryPath} does not exist.", directoryPath);
                    WriteToFile(outputFilePath, $"The directory {directoryPath} does not exist.");
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
                string errorFilePath = Path.Combine(@"C:\Users\TB\Desktop\Work\TestProjects\FileUploadSecurity\Output", "errors.txt");
                WriteToFile(errorFilePath, $"Application start-up failed: {ex.Message}");
            }
        }

        static void ProcessFile(string fileName, string base64File, IFileTypeCheckerService fileTypeCheckerService, string outputFilePath)
        {
            byte[] fileBytes;

            // Extract the expected file format from the file extension
            string expectedFormat = Path.GetExtension(fileName).TrimStart('.').ToUpper();

            try
            {
                fileBytes = Convert.FromBase64String(base64File);
            }
            catch (FormatException)
            {
                Log.Error("Invalid base64 string: {Base64File}", base64File);
                WriteToFile(outputFilePath, $"Filename: {fileName}, Expected Result: {expectedFormat}, Actual Result: Invalid base64 string");
                return;
            }

            string fileType = fileTypeCheckerService.GetFileType(fileBytes, out List<string> secondaryResults);
            string actualResult = fileType == "UNKNOWN" && secondaryResults.Any() ? $"UNKNOWN (Potential: {string.Join(", ", secondaryResults)})" : fileType;

            if (fileType == "MALICIOUS")
            {
//                Log.Warning("The file is malicious. Filename: {FileName}", fileName);
                WriteToFile(outputFilePath, $"Filename: {fileName}, Expected Result: {expectedFormat}, Actual Result: The file is malicious, Secondary Results: [{string.Join("-", secondaryResults)}]");
            }
            else
            {
//                Log.Information("The file type is: {FileType}. Filename: {FileName}, Secondary Results: {SecondaryResults}", fileType, fileName, secondaryResults);
                WriteToFile(outputFilePath, $"Filename: {fileName}, Expected Result: {expectedFormat}, Actual Result: The file type is: {actualResult}, Secondary Results: [{string.Join("-", secondaryResults)}]");
            }
        }

        static void WriteToFile(string filePath, string content)
        {
            lock (typeof(Program))
            {
                File.AppendAllText(filePath, content + Environment.NewLine);
            }
        }
    }
}

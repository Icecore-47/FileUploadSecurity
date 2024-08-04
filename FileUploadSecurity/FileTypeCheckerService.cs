using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace FileUploadSecurity
{
    public class FileTypeCheckerService : IFileTypeCheckerService
    {
        private readonly IEnumerable<IFileChecker> _manualCheckers;
        private readonly IEnumerable<IFileChecker> _secondaryCheckers;
        private readonly ILogger _logger;

        public FileTypeCheckerService(IEnumerable<IFileChecker> manualCheckers, IEnumerable<IFileChecker> secondaryCheckers, ILogger logger)
        {
            _manualCheckers = manualCheckers;
            _secondaryCheckers = secondaryCheckers;
            _logger = logger;
        }

        public string GetFileType(byte[] fileBytes, out List<string> secondaryResults)
        {
            secondaryResults = new List<string>();

            // First run manual checks
            foreach (var checker in _manualCheckers)
            {
                var fileType = checker.CheckFileType(fileBytes);
                if (fileType != "UNKNOWN")
                {
                    // Run secondary checks to validate the result
                    if (ValidateWithSecondaryCheckers(fileBytes, fileType, out secondaryResults))
                    {
//                        _logger.Information("File type detected and validated: {FileType}", fileType);
                        return fileType;
                    }
                }
            }

            //_logger.Information("File type is UNKNOWN");
            return "UNKNOWN";
        }

        private bool ValidateWithSecondaryCheckers(byte[] fileBytes, string manualCheckType, out List<string> secondaryResults)
        {
            secondaryResults = _secondaryCheckers.Select(checker => checker.CheckFileType(fileBytes)).ToList();
            // Log all secondary results


            // Check if any secondary checker agrees with the manual check result
            return secondaryResults.Any(result => result == manualCheckType);
        }
    }
}
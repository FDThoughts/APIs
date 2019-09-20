using CSVMultiFormat.Interfaces;
using CSVMultiFormat.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CSVMultiFormat.Services
{
    /// <summary>
    /// Calls Validation, File and Parse services
    /// </summary>
    public class CsvFileHandler: ICsvFileHandler
    {
        /// <summary>
        /// Validation Service
        /// </summary>
        private readonly IValidationService _validationService;

        /// <summary>
        /// File Service
        /// </summary>
        private readonly IFileService _fileService;

        /// <summary>
        /// Parsing Service
        /// </summary>
        private readonly IParsingService _parsingService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="validationService">IValidationService</param>
        /// <param name="fileService">IFileService</param>
        /// <param name="parsingService">IParsingService</param>
        public CsvFileHandler(IValidationService validationService,
            IFileService fileService,
            IParsingService parsingService)
        {
            _validationService = validationService;
            _fileService = fileService;
            _parsingService = parsingService;
        }

        /// <summary>
        /// Validate, store and parse Csv File
        /// </summary>
        /// <param name="inputFile">IFormFile</param>
        /// <param name="isHeadedFile">bool</param>
        /// <returns>Successfully parsed data or error message - CsvHandleResult task</returns>
        public async Task<CsvHandleResult> ParseCsvFile(IFormFile inputFile,
            bool isHeadedFile)
        {
            var result = new CsvHandleResult();

            // Check file is valid
            if (!_validationService.IsCsvFile(inputFile.FileName))
            {
                result.ErrorMessage =
                    $"Selected file, {inputFile.FileName}, does not have supported format CSV";
                return result;
            }

            // Store the file
            var uploadedFilePath = await _fileService.StoreFile(inputFile);
            if (string.IsNullOrWhiteSpace(uploadedFilePath))
            {
                result.ErrorMessage = "File failed to save to server";
                return result;
            }

            // Parse the file
            var parsedFileContent = _parsingService.ParseCsv(
                uploadedFilePath, isHeadedFile);
            if (parsedFileContent == null)
            {
                result.ErrorMessage = "Failed to parse file content";
                return result;
            }

            result.Success = true;
            result.ParsedCsvContent = parsedFileContent;

            return result;
        }
    }
}

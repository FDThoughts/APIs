using CSVMultiFormat.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CSVMultiFormat.Interfaces
{
    /// <summary>
    /// CsvFileHandler interface
    /// </summary>
    public interface ICsvFileHandler
    {
        /// <summary>
        /// Parse Csv File
        /// </summary>
        /// <param name="inputFile">IFormFile</param>
        /// <param name="isHeadedFiles">bool</param>
        /// <returns>Task<CsvHandleResult></returns>
        Task<CsvHandleResult> ParseCsvFile(IFormFile inputFile,
            bool isHeadedFiles);
    }
}

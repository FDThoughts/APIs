using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CSVMultiFormat.Interfaces
{
    /// <summary>
    /// FileService interface
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Store the file
        /// </summary>
        /// <param name="inputStream">IFormFile</param>
        /// <returns>string task</returns>
        Task<string> StoreFile(IFormFile inputStream);
    }
}

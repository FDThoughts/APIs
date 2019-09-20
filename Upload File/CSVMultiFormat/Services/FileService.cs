using CSVMultiFormat.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace CSVMultiFormat.Services
{
    /// <summary>
    /// File Service
    /// </summary>
    public class FileService: IFileService
    {
        /// <summary>
        /// Path directory to store files 
        /// </summary>
        private string _uploadFilePath;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config">IConfiguration</param>
        public FileService(IConfiguration config)
        {
            // Get upload folder path from appsettings.json file
            _uploadFilePath = config
                .GetValue<string>("Files:filespath");
        }

        /// <summary>
        /// Store file
        /// </summary>
        /// <param name="file">IFormFile</param>
        /// <returns>Full file name - string task</returns>
        public async Task<string> StoreFile(IFormFile file)
        {
            // Get Current app root path
            var rootPath = AppDomain.CurrentDomain.BaseDirectory;

            // Combine with uploaded file path
            var folderPath = Path.Combine(rootPath, _uploadFilePath);

            // if directory doesn't exist create it
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Combine with file name
            var filename = Path.Combine(folderPath, file.FileName);

            // store the file
            using (var stream = new FileStream(filename,
                        FileMode.Create))
            {
                await file.CopyToAsync(stream);                
            }

            // return full file name
            return filename;
        }
    }
}

using CSVMultiFormat.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CSVMultiFormat.Services
{
    /// <summary>
    /// Validation Service
    /// </summary>
    public class ValidationService : IValidationService
    {
        /// <summary>
        /// Files valid type
        /// </summary>
        private const string _validType = ".csv";

        /// <summary>
        /// Check file type is valid
        /// </summary>
        /// <param name="filename">string</param>
        /// <returns>True if file extension is '.csv'</returns>
        public bool IsCsvFile(string filename)
        {
            return filename.ToLower().EndsWith(_validType);
        }

    }
}

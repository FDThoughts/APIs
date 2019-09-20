using CSVMultiFormat.Models;

namespace CSVMultiFormat.Interfaces
{
    /// <summary>
    /// ParsingService interface
    /// </summary>
    public interface IParsingService
    {
        /// <summary>
        /// Parse file
        /// </summary>
        /// <param name="fileContent">string</param>
        /// <param name="isHeadedFile">bool</param>
        /// <returns>DataModel</returns>
        DataModel ParseCsv(string fileContent,
            bool isHeadedFile);
    }
}

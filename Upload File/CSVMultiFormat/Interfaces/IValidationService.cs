
namespace CSVMultiFormat.Interfaces
{
    /// <summary>
    /// ValidationService interface
    /// </summary>
    public interface IValidationService
    {
        /// <summary>
        /// Check file is valid type
        /// </summary>
        /// <param name="filename">string</param>
        /// <returns></returns>
        bool IsCsvFile(string filename);
    }
}

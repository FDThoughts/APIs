
using System;

namespace CSVMultiFormat.Models
{
    /// <summary>
    /// Parsed data model
    /// </summary>
    public class CsvHandleResult : IEquatable<CsvHandleResult>
    {
        /// <summary>
        /// Validation, storing and parsing is successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Parsed data
        /// </summary>
        public DataModel ParsedCsvContent { get; set; }

        /// <summary>
        /// Override the 'Equals' operator
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>>bool - true if objects are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CsvHandleResult)obj);
        }

        /// <summary>
        /// Override GetHashCode
        /// </summary>
        /// <returns>int</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Override the 'Equals' operator
        /// </summary>
        /// <param name="chr">CsvHandleResult</param>
        /// <returns>bool - true if objects are equal, false otherwise</returns>
        public bool Equals(CsvHandleResult chr)
        {
            if (ReferenceEquals(null, chr)) return false;
            if (ReferenceEquals(this, chr)) return true;
            if (!this.Success.Equals(chr.Success)) return false;
            if (!(ReferenceEquals(null, chr.ErrorMessage) &&
                ReferenceEquals(this.ErrorMessage, chr.ErrorMessage)))
            {
                if (!this.ErrorMessage.Equals(chr.ErrorMessage)) return false;
            }
            if (ReferenceEquals(null, chr.ParsedCsvContent) &&
                ReferenceEquals(this.ParsedCsvContent, chr.ParsedCsvContent)) return true;
            if (!this.ParsedCsvContent.Equals(chr.ParsedCsvContent)) return false;

            return true;
        }
    }
}

using CSVMultiFormat.Interfaces;
using CSVMultiFormat.Models;
using Microsoft.VisualBasic.FileIO;

namespace CSVMultiFormat.Services
{
    /// <summary>
    /// Parsing Service
    /// </summary>
    public class ParsingService: IParsingService
    {
        /// <summary>
        /// Parse file
        /// </summary>
        /// <param name="filePath">string</param>
        /// <param name="isHeadedFile">bool</param>
        /// <returns>Parsed data - DataModel</returns>
        public DataModel ParseCsv(string filePath,
            bool isHeadedFile)
        {
            DataModel dataModel = new DataModel();

            // use TextFieldParser
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(new string[] { "," });
                int line = 0;                

                while (!parser.EndOfData)
                {
                    line++;
                    Row row = new Row();
                    var fields = parser.ReadFields();

                    // populate row columns
                    foreach (var field in fields)
                    {
                        Column column = new Column(field);
                        row.columns.Add(column);
                    }

                    // if the file is headed, create the header row
                    if (isHeadedFile && line == 1)
                    {
                        dataModel.header = row;
                    }
                    else
                    {
                        dataModel.rows.Add(row);
                    }
                }
            }

            return dataModel;
        }
    }
}

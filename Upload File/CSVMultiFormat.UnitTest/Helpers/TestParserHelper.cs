using CSVMultiFormat.Models;
using System.Collections.Generic;

namespace CSVMultiFormat.UnitTest
{
    internal class TestParserHelper
    {
        /// <summary>
        /// Headed expected result
        /// </summary>
        internal DataModel headedExpectedResult;

        /// <summary>
        /// expected result with no header
        /// </summary>
        internal DataModel expectedResultNoHeader;

        /// <summary>
        /// expected result with missing fields
        /// </summary>
        internal DataModel missingFieldsExpectedResult;

        /// <summary>
        /// File Content with header
        /// </summary>
        internal string headedContent;

        /// <summary>
        /// File Content without header
        /// </summary>
        internal string notHeadedContent;

        /// <summary>
        /// File Content with missing fields
        /// </summary>
        internal string missingFieldsContent;

        /// <summary>
        /// TestParserHelper constructor
        /// </summary>
        internal TestParserHelper()
        {
            headedContent = @"DealNumber,CustomerName,DealershipName,Vehicle,Price,Date
                5469,Milli Fulton, Sun of Saskatoon,2017 Ferrari 488 Spider,""429,987"",6/19/2018
                5132,Rahima Skinner, Seven Star Dealership,2009 Lamborghini Gallardo Carbon Fiber LP-560,""169,900"",1/14/2018";

            notHeadedContent = @"5469,Milli Fulton, Sun of Saskatoon,2017 Ferrari 488 Spider,""429,987"",6/19/2018
                5132,Rahima Skinner, Seven Star Dealership,2009 Lamborghini Gallardo Carbon Fiber LP-560,""169,900"",1/14/2018";

            missingFieldsContent = @"DealNumber,CustomerName,DealershipName,Vehicle,Price,Date
                5469,Milli Fulton, Sun of Saskatoon,2017 Ferrari 488 Spider,""429,987""
                5132,Rahima Skinner, Seven Star Dealership,2009 Lamborghini Gallardo Carbon Fiber LP-560,""169,900"",1/14/2018";

            List<Row> rows = new List<Row>
            {
                new Row
                {
                    columns = new List<Column>
                    {
                        new Column("5469"),
                        new Column("Milli Fulton"),
                        new Column("Sun of Saskatoon"),
                        new Column("2017 Ferrari 488 Spider"),
                        new Column("429,987"),
                        new Column("6/19/2018")
                    }
                },
                new Row
                {
                    columns = new List<Column>
                    {
                        new Column("5132"),
                        new Column("Rahima Skinner"),
                        new Column("Seven Star Dealership"),
                        new Column("2009 Lamborghini Gallardo Carbon Fiber LP-560"),
                        new Column("169,900"),
                        new Column("1/14/2018")
                    }
                }
            };

            List<Row> rowsMissingFields = new List<Row>
            {
                new Row
                {
                    columns = new List<Column>
                    {
                        new Column("5469"),
                        new Column("Milli Fulton"),
                        new Column("Sun of Saskatoon"),
                        new Column("2017 Ferrari 488 Spider"),
                        new Column("429,987")
                    }
                },
                new Row
                {
                    columns = new List<Column>
                    {
                        new Column("5132"),
                        new Column("Rahima Skinner"),
                        new Column("Seven Star Dealership"),
                        new Column("2009 Lamborghini Gallardo Carbon Fiber LP-560"),
                        new Column("169,900"),
                        new Column("1/14/2018")
                    }
                }
            };

            Row header = new Row
            {
                columns = new List<Column>
                   {
                       new Column("DealNumber"),
                       new Column("CustomerName"),
                       new Column("DealershipName"),
                       new Column("Vehicle"),
                       new Column("Price"),
                       new Column("Date")
                   }
            };

            headedExpectedResult = new DataModel
            {
                header = header,
                rows = rows
            };

            expectedResultNoHeader = new DataModel
            {
                rows = rows
            };

            missingFieldsExpectedResult = new DataModel
            {
                header = header,
                rows = rowsMissingFields
            };
        }
    }
}

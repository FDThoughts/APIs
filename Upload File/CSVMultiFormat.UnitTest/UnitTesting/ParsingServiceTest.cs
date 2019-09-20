using CSVMultiFormat.Models;
using CSVMultiFormat.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSVMultiFormat.UnitTest
{
    /// <summary>
    /// Test parsing service
    /// </summary>
    [TestClass]
    public class ParsingServiceTest
    {
        /// <summary>
        /// ParseCsv returns headed model when parsing headed file
        /// </summary>
        [TestMethod]
        public void ParseCsvTestReturnsHeadedModelForHeadedFile()
        {
            // Arrange
            ParsingService parsingService = new ParsingService();
            var filePath = string.Format(
                @"{0}\TestFiles\InputData.csv", 
                AppDomain.CurrentDomain.BaseDirectory);

            // Expected result
            TestParserHelper parserHelper = new TestParserHelper();
            DataModel expectedResult = parserHelper.headedExpectedResult;
            
            // Act
            DataModel actualResult = parsingService.ParseCsv(filePath, true);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// ParseCsv returns no headed model when parsing no headed file
        /// </summary>
        [TestMethod]
        public void ParseCsvTestReturnsNoHeadedModelForNoHeadedFile()
        {
            // Arrange
            ParsingService parsingService = new ParsingService();
            var filePath = string.Format(
                @"{0}\TestFiles\InputDataOnly.csv",
                AppDomain.CurrentDomain.BaseDirectory);

            // Expected result
            TestParserHelper parserHelper = new TestParserHelper();
            DataModel expectedResult = parserHelper.expectedResultNoHeader;

            // Act
            DataModel actualResult = parsingService.ParseCsv(filePath, false);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// ParseCsv returns model when parsing file with missing fields
        /// </summary>
        [TestMethod]
        public void ParseCsvTestReturnsModelForMissingFieldsFile()
        {
            // Arrange
            ParsingService parsingService = new ParsingService();
            var filePath = string.Format(
                @"{0}\TestFiles\InputDataMissingFields.csv",
                AppDomain.CurrentDomain.BaseDirectory);

            // Expected result
            TestParserHelper parserHelper = new TestParserHelper();
            DataModel expectedResult = parserHelper.missingFieldsExpectedResult;

            // Act
            DataModel actualResult = parsingService.ParseCsv(filePath, true);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// ParseCsv returns empty model when parsing empty file
        /// </summary>
        [TestMethod]
        public void ParseCsvTestReturnsEmptyModelForEmptyFile()
        {
            // Arrange
            ParsingService parsingService = new ParsingService();
            var filePath = string.Format(
                @"{0}\TestFiles\InputDataEmpty.csv",
                AppDomain.CurrentDomain.BaseDirectory);

            // Expected result
            DataModel expectedResult = new DataModel();

            // Act
            DataModel actualResult = parsingService.ParseCsv(filePath, true);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

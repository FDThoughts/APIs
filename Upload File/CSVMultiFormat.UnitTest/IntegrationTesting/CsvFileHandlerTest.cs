using CSVMultiFormat.Models;
using CSVMultiFormat.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSVMultiFormat.UnitTest
{
    /// <summary>
    /// Test CSV File Handler
    /// </summary>
    [TestClass]
    public class CsvFileHandlerTest
    {
        /// <summary>
        /// Test ParseCsvFile() returns error message if wrong extension
        /// </summary>
        [TestMethod]
        public void ParseCsvFileTestWrongExtensionReturnsErrorMessage()
        {
            //Arrange
            var txtFileName = "Data.txt";
            TestFileHelper tstHelper = new TestFileHelper(txtFileName, "");
            CsvFileHandler csvFileHandler = new CsvFileHandler(
                new ValidationService(), new FileService(tstHelper.config.Object),
                new ParsingService());

            // Expected result
            var expectedResult = new CsvHandleResult();
            expectedResult.ErrorMessage =
                $"Selected file, {txtFileName}, does not have supported format CSV";

            //Act
            var actualResult = csvFileHandler.ParseCsvFile(tstHelper.fileMock.Object, true);

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsNotNull(actualResult.Result);
            Assert.AreEqual(expectedResult, actualResult.Result);
        }

        /// <summary>
        /// ParseCsv returns headed model when parsing headed file
        /// </summary>
        [TestMethod]
        public void ParseCsvFileTestReturnsHeadedModelForHeadedFile()
        {
            // Arrange
            var filePath = "InputData.csv";
            TestParserHelper parserHelper = new TestParserHelper();
            TestFileHelper tstHelper = new TestFileHelper(filePath, parserHelper.headedContent);
            CsvFileHandler csvFileHandler = new CsvFileHandler(
                new ValidationService(), new FileService(tstHelper.config.Object),
                new ParsingService());

            // Expected result            
            var expectedResult = new CsvHandleResult();
            expectedResult.Success = true;
            expectedResult.ParsedCsvContent = parserHelper.headedExpectedResult;

            // Act
            var actualResult =
                csvFileHandler.ParseCsvFile(tstHelper.fileMock.Object, true);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.IsNotNull(actualResult.Result);
            Assert.AreEqual(expectedResult, actualResult.Result);
        }

        /// <summary>
        /// ParseCsv returns a not headed model when parsing a not headed file
        /// </summary>
        [TestMethod]
        public void ParseCsvFileTestReturnsNotHeadedModelForNotHeadedFile()
        {
            // Arrange
            var filePath = "InputData.csv";
            TestParserHelper parserHelper = new TestParserHelper();
            TestFileHelper tstHelper = new TestFileHelper(filePath, parserHelper.notHeadedContent);
            CsvFileHandler csvFileHandler = new CsvFileHandler(
                new ValidationService(), new FileService(tstHelper.config.Object),
                new ParsingService());

            // Expected result            
            var expectedResult = new CsvHandleResult();
            expectedResult.Success = true;
            expectedResult.ParsedCsvContent = parserHelper.expectedResultNoHeader;

            // Act
            var actualResult =
                csvFileHandler.ParseCsvFile(tstHelper.fileMock.Object, false);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.IsNotNull(actualResult.Result);
            Assert.AreEqual(expectedResult, actualResult.Result);
        }

        /// <summary>
        /// ParseCsv returns model when parsing file with missing fields
        /// </summary>
        [TestMethod]
        public void ParseCsvFileTestReturnsModelForMissingFieldsFile()
        {
            // Arrange
            var filePath = "InputData.csv";
            TestParserHelper parserHelper = new TestParserHelper();
            TestFileHelper tstHelper = new TestFileHelper(filePath, parserHelper.missingFieldsContent);
            CsvFileHandler csvFileHandler = new CsvFileHandler(
                new ValidationService(), new FileService(tstHelper.config.Object),
                new ParsingService());

            // Expected result            
            var expectedResult = new CsvHandleResult();
            expectedResult.Success = true;
            expectedResult.ParsedCsvContent = parserHelper.missingFieldsExpectedResult;

            // Act
            var actualResult =
                csvFileHandler.ParseCsvFile(tstHelper.fileMock.Object, true);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.IsNotNull(actualResult.Result);
            Assert.AreEqual(expectedResult, actualResult.Result);
        }

        /// <summary>
        /// ParseCsv returns empty model when parsing empty file
        /// </summary>
        [TestMethod]
        public void ParseCsvFileTestReturnsEmptyModelForEmptyFile()
        {
            // Arrange
            var filePath = "InputData.csv";
            TestFileHelper tstHelper = new TestFileHelper(filePath, "");
            CsvFileHandler csvFileHandler = new CsvFileHandler(
                new ValidationService(), new FileService(tstHelper.config.Object),
                new ParsingService());

            // Expected result
            var expectedResult = new CsvHandleResult();
            expectedResult.Success = true;
            expectedResult.ParsedCsvContent = new DataModel();

            // Act
            var actualResult =
                csvFileHandler.ParseCsvFile(tstHelper.fileMock.Object, true);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.IsNotNull(actualResult.Result);
            Assert.AreEqual(expectedResult, actualResult.Result);
        }
    }
}

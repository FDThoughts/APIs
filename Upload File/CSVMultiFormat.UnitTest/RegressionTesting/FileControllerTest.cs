using CSVMultiFormat.Controllers;
using CSVMultiFormat.Models;
using CSVMultiFormat.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CSVMultiFormat.UnitTest
{
    /// <summary>
    /// Test CSV File Handler
    /// </summary>
    [TestClass]
    public class FileControllerTest
    {
        /// <summary>
        /// Test Post() returns error message if wrong extension
        /// </summary>
        [TestMethod]
        public void PostTestWrongExtensionReturnsErrorMessage()
        {
            //Arrange
            var txtFileName = "Data.txt";
            TestFileHelper tstHelper = new TestFileHelper(txtFileName, "");
            FilesController fileController = new FilesController(tstHelper.config.Object,
                new CsvFileHandler(new ValidationService(),
                    new FileService(tstHelper.config.Object),
                    new ParsingService()));

            // Expected result
            var expectedResult = new NotFoundObjectResult(new
            {
                message = 
                    $"Selected file, {txtFileName}, does not have supported format CSV"
            });

            //Act
            var actualResult = fileController.Post(new List<IFormFile>() { tstHelper.fileMock.Object }, true);

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsNotNull(actualResult.Result);
            Assert.AreEqual(expectedResult.StatusCode, 
                ((NotFoundObjectResult)actualResult.Result).StatusCode);
            Assert.AreEqual(expectedResult.Value.ToString(), 
                ((NotFoundObjectResult)actualResult.Result).Value.ToString());
        }

        /// <summary>
        /// Post returns headed model when parsing headed file
        /// </summary>
        [TestMethod]
        public void PostTestReturnsHeadedModelForHeadedFile()
        {
            // Arrange
            var filePath = "InputData.csv";
            TestParserHelper parserHelper = new TestParserHelper();
            TestFileHelper tstHelper = new TestFileHelper(filePath, parserHelper.headedContent);
            FilesController fileController = new FilesController(tstHelper.config.Object,
                new CsvFileHandler(new ValidationService(),
                    new FileService(tstHelper.config.Object),
                    new ParsingService()));

            // Expected result            
            var expectedCsvResult = new CsvHandleResult();
            expectedCsvResult.Success = true;
            expectedCsvResult.ParsedCsvContent = parserHelper.headedExpectedResult;
            var expectedResult = new OkObjectResult(new
            {
                isHeadedFiles = true,
                resultList = new List<CsvHandleResult>() { expectedCsvResult }
            });

            // Act
            var actualResult =
                fileController.Post(new List<IFormFile>() { tstHelper.fileMock.Object }, true);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.IsNotNull(actualResult.Result);
            Assert.AreEqual(expectedResult.StatusCode, 
                ((OkObjectResult)actualResult.Result).StatusCode);
            Assert.AreEqual(expectedResult.Value.ToString(), 
                ((OkObjectResult)actualResult.Result).Value.ToString());
        }

        /// <summary>
        /// Post returns a not headed model when parsing a not headed file
        /// </summary>
        [TestMethod]
        public void PostTestReturnsNotHeadedModelForNotHeadedFile()
        {
            // Arrange
            var filePath = "InputData.csv";
            TestParserHelper parserHelper = new TestParserHelper();
            TestFileHelper tstHelper = new TestFileHelper(filePath, parserHelper.notHeadedContent);
            FilesController fileController = new FilesController(tstHelper.config.Object,
                new CsvFileHandler(new ValidationService(),
                    new FileService(tstHelper.config.Object),
                    new ParsingService()));

            // Expected result
            var expectedCsvResult = new CsvHandleResult();
            expectedCsvResult.Success = true;
            expectedCsvResult.ParsedCsvContent = parserHelper.expectedResultNoHeader;
            var expectedResult = new OkObjectResult(new
            {
                isHeadedFiles = false,
                resultList = new List<CsvHandleResult>() { expectedCsvResult }
            });

            // Act
            var actualResult =
                fileController.Post(new List<IFormFile>() { tstHelper.fileMock.Object }, false);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.IsNotNull(actualResult.Result);
            Assert.AreEqual(expectedResult.StatusCode,
                ((OkObjectResult)actualResult.Result).StatusCode);
            Assert.AreEqual(expectedResult.Value.ToString(),
                ((OkObjectResult)actualResult.Result).Value.ToString());
        }

        /// <summary>
        /// Post returns model when parsing file with missing fields
        /// </summary>
        [TestMethod]
        public void PostTestReturnsModelForMissingFieldsFile()
        {
            // Arrange
            var filePath = "InputData.csv";
            TestParserHelper parserHelper = new TestParserHelper();
            TestFileHelper tstHelper = new TestFileHelper(filePath, parserHelper.missingFieldsContent);
            FilesController fileController = new FilesController(tstHelper.config.Object,
                new CsvFileHandler(new ValidationService(),
                    new FileService(tstHelper.config.Object),
                    new ParsingService()));

            // Expected result
            var expectedCsvResult = new CsvHandleResult();
            expectedCsvResult.Success = true;
            expectedCsvResult.ParsedCsvContent = parserHelper.missingFieldsExpectedResult;
            var expectedResult = new OkObjectResult(new
            {
                isHeadedFiles = true,
                resultList = new List<CsvHandleResult>() { expectedCsvResult }
            });

            // Act
            var actualResult =
                fileController.Post(new List<IFormFile>() { tstHelper.fileMock.Object }, true);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.IsNotNull(actualResult.Result);
            Assert.AreEqual(expectedResult.StatusCode,
                ((OkObjectResult)actualResult.Result).StatusCode);
            Assert.AreEqual(expectedResult.Value.ToString(),
                ((OkObjectResult)actualResult.Result).Value.ToString());
        }

        /// <summary>
        /// Post returns empty model when parsing empty file
        /// </summary>
        [TestMethod]
        public void PostTestReturnsEmptyModelForEmptyFile()
        {
            // Arrange
            var filePath = "InputData.csv";
            TestFileHelper tstHelper = new TestFileHelper(filePath, "");
            FilesController fileController = new FilesController(tstHelper.config.Object,
                new CsvFileHandler(new ValidationService(),
                    new FileService(tstHelper.config.Object),
                    new ParsingService()));

            // Expected result
            var expectedCsvResult = new CsvHandleResult();
            expectedCsvResult.Success = true;
            expectedCsvResult.ParsedCsvContent = new DataModel();
            var expectedResult = new OkObjectResult(new
            {
                isHeadedFiles = true,
                resultList = new List<CsvHandleResult>() { expectedCsvResult }
            });

            // Act
            var actualResult =
                fileController.Post(new List<IFormFile>() { tstHelper.fileMock.Object }, true);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.IsNotNull(actualResult.Result);
            Assert.AreEqual(expectedResult.StatusCode,
                ((OkObjectResult)actualResult.Result).StatusCode);
            Assert.AreEqual(expectedResult.Value.ToString(),
                ((OkObjectResult)actualResult.Result).Value.ToString());
        }
    }
}

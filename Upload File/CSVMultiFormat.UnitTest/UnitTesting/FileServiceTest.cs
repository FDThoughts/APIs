using CSVMultiFormat.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSVMultiFormat.UnitTest
{
    /// <summary>
    /// Test file service
    /// </summary>
    [TestClass]
    public class FileServiceTest
    {
        /// <summary>
        /// Test StoreFile() to return the full file name
        /// </summary>
        [TestMethod]
        public void StoreFileTestReturnsFullFileName()
        {
            // Arrange
            // Mock the configuration file
            var fileName = "test.csv";
            TestFileHelper tstHelper = new TestFileHelper(fileName, "");

            // Create the file service instance
            FileService fileService = new FileService(
                tstHelper.config.Object);

            // Expected result
            var expectedFullFileName = string.Format(
                @"{0}\{1}",
                tstHelper.config.Object.GetValue<string>("Files:filespath"),
                fileName);

            // Act
            var actualResult = fileService.StoreFile(tstHelper.fileMock.Object);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.IsNotNull(actualResult.Result);
            Assert.AreEqual<string>(expectedFullFileName,
                actualResult.Result);
        }
    }
}

using CSVMultiFormat.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSVMultiFormat.UnitTest
{
    /// <summary>
    /// Test validation service
    /// </summary>
    [TestClass]
    public class ValidationServiceTest
    {
        /// <summary>
        /// Test IsCsvFile() returns false if wrong extension
        /// </summary>
        [TestMethod]
        public void IsCsvFileTestWrongExtensionReturnsFalse()
        {
            //Arrange
            ValidationService validationService = new ValidationService();
            var txtFileName = "Data.txt";

            //Act
            var result = validationService.IsCsvFile(txtFileName);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Test IsCsvFile() returns true is valid extension
        /// </summary>
        [TestMethod]
        public void IsCsvFileTestValidExtensionReturnsTrue()
        {
            //Arrange
            ValidationService validationService = new ValidationService();
            var csvFileName = "test.csv";

            //Act
            var result = validationService.IsCsvFile(csvFileName);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
    }
}

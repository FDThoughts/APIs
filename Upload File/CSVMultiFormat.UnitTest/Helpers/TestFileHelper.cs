using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IO;
using System.Threading;

namespace CSVMultiFormat.UnitTest
{
    internal class TestFileHelper
    {
        /// <summary>
        /// Mock the configuration file
        /// </summary>
        internal Mock<IConfiguration> config;

        /// <summary>
        /// Mock the input file
        /// </summary>
        internal Mock<IFormFile> fileMock;        

        /// <summary>
        /// TestFileHelper constructor
        /// </summary>
        /// <param name="fileName">string</param>
        /// <param name="content">string</param>
        internal TestFileHelper(string fileName, string content)
        {
            // Mock Configuration
            config = new Mock<IConfiguration>();
            var configSection = new Mock<IConfigurationSection>();
            configSection.Setup(a => a.Value)
                .Returns(@"C:\temp\fileUpload\test");

            config.Setup(a => a.GetSection("Files:filespath"))
                .Returns(configSection.Object);

            // Mock file
            fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream            
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(f => f.FileName).Returns(fileName).Verifiable();
            fileMock.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream))
                .Verifiable();
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
        }
    }
}

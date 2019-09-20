using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSVMultiFormat.Interfaces;
using CSVMultiFormat.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CSVMultiFormat.Controllers
{
    /// <summary>
    /// Upload file, validate it, store it and parse it
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [DisableCors]
    public class FilesController : ControllerBase
    {
        /// <summary>
        /// CsvFileHandler interface: Validate a csv file, store it and parse it
        /// </summary>
        private readonly ICsvFileHandler _csvFileHandler;

        /// <summary>
        /// Constructor
        /// </summary>
        public FilesController(IConfiguration config,
            ICsvFileHandler csvFileHandler)
        {
            this._csvFileHandler = csvFileHandler;
        }

        /// <summary>
        /// Default GET request
        /// </summary>
        /// <returns>A welcome message</returns>
        // GET api/files
        [HttpGet]
        public ActionResult<string> Index()
        {
            return "Welcome to FilesController!";
        }

        /// <summary>
        /// POST request - upload one or multiple files
        /// </summary>
        /// <param name="files">List of files</param>
        /// <param name="isHeadedFiles">true if files have header line</param>
        /// <returns>IActionResult task</returns>
        // POST api/files
        [HttpPost("UploadFile")]        
        public async Task<IActionResult> Post(List<IFormFile> files,
            bool isHeadedFiles = false)
        {
            try
            {
                // Check there is at least 1 file
                if (files != null && files.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "0 files received"
                    });
                }

                // Data list of parsed files
                List<CsvHandleResult> resultList = 
                    new List<CsvHandleResult>();
                
                foreach (var formFile in files)
                {
                    // Parse each file
                    var result = await _csvFileHandler.ParseCsvFile(
                        formFile, isHeadedFiles);

                    // Check if parsed successfully
                    if (!result.Success)
                    {
                        return NotFound(new
                        {
                            message = result.ErrorMessage
                        });
                    }

                    resultList.Add(result);
                }

                return Ok(new
                {
                    isHeadedFiles,
                    resultList
                });
            }
            catch (Exception e)
            {
                return NotFound(new
                {
                    message = $"Something went wrong: {e.Message}"
                });
            }
        }
    }
}

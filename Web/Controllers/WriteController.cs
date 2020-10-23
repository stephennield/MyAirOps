using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyAirOpsLogger.Models;
using MyAirOpsLogger.Logging;
using System;
using System.Net;

namespace MyAirOpsLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriteController : ControllerBase
    {
        #region Dependencies

        private readonly ILogger<FileSystemLoggingCategory> _fileLogger;
        private readonly ILogger<WriteController> _exceptionLogger;

        #endregion


        #region Construct

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteController" /> class.
        /// </summary>
        /// <param name="fileLogger">The file logger.</param>
        /// <param name="exceptionLogger">The exception logger.</param>
        /// <exception cref="System.ArgumentNullException">
        /// logSystem
        /// or
        /// exceptionLogger
        /// </exception>
        public WriteController(
            ILogger<FileSystemLoggingCategory> fileLogger,
            ILogger<WriteController> exceptionLogger)
        {
            _fileLogger = fileLogger ?? throw new ArgumentNullException(nameof(fileLogger));
            _exceptionLogger = exceptionLogger ?? throw new ArgumentNullException(nameof(exceptionLogger));
        }

        #endregion


        #region API

        public IActionResult Get()
        {
            return new JsonResult(new { action = "Use only POST method" });
        }

        /// <summary>
        /// Accepts a POST of the specified <see cref="Message" />.
        /// Here is an example body:
        /// {
        ///     "id": 1, 
        ///     "date": "2020-10-21T00:00:00.000Z", 
        ///     "content": "this is a test"
        /// }
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Message value)
        {
            try
            {
                _fileLogger
                    .LogInformation(
                        new Domain.LogMessage(
                            value.Id,
                            value.Date,
                            value.Content).ToString());

                // maybe if the requirements also included the need to view the created 
                // log entry you could return a CreatedResult() to be more RESTful
                return Ok();
            }
            catch (Exception exception)
            {
                _exceptionLogger
                    .LogError(
                        exception,
                        "Exception occurred in {Controller} {Action}", 
                        nameof(WriteController),
                        nameof(Post));

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        #endregion
    }
}

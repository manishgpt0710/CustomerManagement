using System;
using System.Net;
using CustomerManagement.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace CustomerManagement.WebApi.Controllers
{
    [Route("api/error/{statusCode}")]
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        private ILogger<ErrorController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CustomerManagement.WebApi.Controllers.ErrorController"/> class.
        /// </summary>
        /// <param name="logger"></param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Renders the error. <br/>
        /// Strictly speaking, a statuscode of less than 400 should not appear as a normal situation of calling error api,
        /// but in fact this situation will not occur, so the above correctness judgment is not made for statusCode
        /// </summary>
        /// <param name="statusCode"></param>
        [HttpGet, HttpPut, HttpPost, HttpDelete, HttpPatch]
        public IActionResult RenderError(int statusCode)
        {
            _logger.LogDebug($"Call api/error/{statusCode}");

            var statusType = ((HttpStatusCode)statusCode).ToString();
            var statusPhrase = ReasonPhrases.GetReasonPhrase(statusCode);

            return StatusCode(statusCode,
                new ApiResponse(null, new[] { new ApiError(statusType, statusPhrase, statusCode.ToString()) }));
        }
    }
}


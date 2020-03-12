using System;
using System.Collections.Generic;
using System.Linq;
using Common.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.WebApiCore.Controllers
{
    [Route("TimeZone")]
    public class TimeZoneController : BaseApiController
    {
        /// <summary>
        /// Returns a collection of all TimeZoneInfo
        /// </summary>
        /// <returns>Collection of TimeZoneInfo DTO</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TimeZoneInfoDTO>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(TimeZoneInfo.GetSystemTimeZones().Select(x => new TimeZoneInfoDTO(x)));
        }
    }
}
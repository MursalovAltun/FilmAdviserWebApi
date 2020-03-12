using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Exceptions;
using Common.Services.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;

namespace Common.WebApiCore.Controllers
{
    [Route("Movie")]
    public class MovieController : BaseApiController
    {
        private readonly IMovieService _movieService;
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger _logger;

        public MovieController(IMovieService movieService,
                               IDistributedCache distributedCache,
                               ILogger logger)
        {
            this._movieService = movieService;
            this._distributedCache = distributedCache;
            this._logger = logger;
        }

        /// <summary>
        /// Gets trending movies by time window.
        /// </summary>
        /// <param name="timeWindow">Time window could only be day or week</param>
        /// <returns>A collection of movies</returns>
        [HttpGet("Trending")]
        [ProducesResponseType(typeof(IEnumerable<MovieDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDTO), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTrending(string timeWindow)
        {
            var isValidTimeWindow = timeWindow switch
            {
                "day" => true,
                "week" => true,
                _ => false
            };

            if (!isValidTimeWindow)
            {
                throw new BadRequestException("Invalid timewindow value it must be day or week");
            }

            var movies = await this._movieService.GetTrending("all", timeWindow);
            return Ok(movies);
        }

        /// <summary>
        /// Gets movies by provided title
        /// </summary>
        /// <param name="title">Movie's title</param>
        /// <returns>A collection of matched title movies</returns>
        [HttpGet("ByTitle")]
        [ProducesResponseType(typeof(IEnumerable<MovieDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDTO), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new BadRequestException("Movie's title cannot be empty");
            }

            var movies = await this._movieService.GetByTitle(title);
            return Ok(movies);
        }
    }
}
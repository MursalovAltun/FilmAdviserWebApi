using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.TmdbDTO;
using Common.Exceptions;
using Common.Services.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.WebApiCore.Controllers
{
    /// <summary>
    /// Controller with endpoints that allows to manage movies
    /// </summary>
    [Route("Movie")]
    public class MovieController : BaseApiController
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            this._movieService = movieService;
        }

        /// <summary>
        /// Gets trending movies by time window.
        /// </summary>
        /// <param name="timeWindow">Time window could only be day or week</param>
        /// <returns>A collection of movies</returns>
        [HttpGet("Trending")]
        [ProducesResponseType(typeof(IEnumerable<MovieDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDTO), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
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

            var movies = await this._movieService.GetTrending("movie", timeWindow);
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
        [AllowAnonymous]
        public async Task<IActionResult> GetByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new BadRequestException("Movie's title cannot be empty");
            }

            var movies = await this._movieService.GetByTitle(title);
            return Ok(movies);
        }

        /// <summary>
        /// Gets movie details by movie id
        /// </summary>
        /// <param name="id">Movie id</param>
        /// <param name="append">
        /// Additional optional parameter that appends additional data such as: videos, images.
        /// For example: append=videos,images
        /// </param>
        /// <returns>Movie DTO</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDTO), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, string append = "")
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid movie id");
            }

            var movie = await this._movieService.GetMovieDetails(id, append);
            return Ok(movie);
        }
    }
}
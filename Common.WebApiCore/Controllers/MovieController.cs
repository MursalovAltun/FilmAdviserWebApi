using System.Threading.Tasks;
using Common.Services.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Common.WebApiCore.Controllers
{
    [Route("Movie")]
    public class MovieController : BaseApiController
    {
        private readonly IMovieService _movieService;
        private readonly ILogger _logger;

        public MovieController(IMovieService movieService,
                               ILogger logger)
        {
            this._movieService = movieService;
            this._logger = logger;
        }

        [HttpGet("Trending")]
        public async Task<IActionResult> GetTrending()
        {
            var movies = await this._movieService.GetTrending("all", "day");
            return Ok(movies);
        }

        [HttpGet("ByTitle")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            this._logger.Warning(title);
            var movies = await this._movieService.GetByTitle(title);
            return Ok(movies);
        }
    }
}
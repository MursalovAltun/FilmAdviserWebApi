using System.Threading.Tasks;
using Common.Services.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Common.WebApiCore.Controllers
{
    [Route("Movie")]
    public class MovieController : BaseApiController
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            this._movieService = movieService;
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
            var movies = await this._movieService.GetByTitle(title);
            return Ok(movies);
        }
    }
}
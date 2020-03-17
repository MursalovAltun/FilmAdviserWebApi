using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO.TmdbDTO;

namespace Common.Services.Infrastructure.Services
{
    public interface IMovieService
    {
        /// <summary>
        /// Gets movies
        /// </summary>
        /// <param name="title">The movie's title</param>
        /// <returns>A collection of Movie DTO</returns>
        Task<IEnumerable<MovieDTO>> GetByTitle(string title);

        /// <summary>
        /// Gets trending movies, series etc. per time
        /// </summary>
        /// <param name="mediaType">
        /// Available types: all, movie, tv, person
        /// </param>
        /// <param name="timeWindow">
        /// Available type windows: day, week
        /// </param>
        /// <returns>A collection of Movie DTO</returns>
        Task<IEnumerable<MovieDTO>> GetTrending(string mediaType, string timeWindow);

        Task<MovieDTO> GetMovieDetails(int id);
    }
}
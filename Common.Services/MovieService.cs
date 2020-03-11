using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Common.DTO.TmdbDTO;
using Common.Exceptions;
using Common.Services.Infrastructure.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _client;

        public MovieService(HttpClient client)
        {
            this._client = client;
        }

        public async Task<IEnumerable<MovieDTO>> GetByTitle(string title)
        {
            var response = await this._client.GetAsync($"3/search/movie?query={title}");
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<TmdbResponseDTO<MovieDTO>>(responseBody);
                return result.Results;
            }

            throw new BadRequestException(response.ReasonPhrase);
        }

        public async Task<IEnumerable<MovieDTO>> GetTrending(string mediaType, string timeWindow)
        {
            var response = await this._client.GetAsync($"3/trending/{mediaType}/{timeWindow}");
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<TmdbResponseDTO<MovieDTO>>(responseBody);
                return result.Results;
            }

            throw new BadRequestException(response.ReasonPhrase);
        }

        private async Task<IEnumerable<GenreDTO>> GetAllGenres(string type)
        {
            var response = await this._client.GetAsync($"3/genre/{type}/list");
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var genresJsonJObject= JObject.Parse(responseBody);

                var genresJson = genresJsonJObject["genres"].Children().ToList();

                return genresJson.Select(genre => genre.ToObject<GenreDTO>()).ToList();
            }

            throw new BadRequestException(response.ReasonPhrase);
        }
    }
}
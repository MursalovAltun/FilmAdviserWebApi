using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Constants;
using Common.DTO;
using Common.DTO.TmdbDTO;
using Common.Exceptions;
using Common.Services.Infrastructure.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Common.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _client;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;

        public MovieService(HttpClient client,
                            IDistributedCache distributedCache,
                            IMapper mapper)
        {
            this._client = client;
            this._distributedCache = distributedCache;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<MovieDTO>> GetByTitle(string title)
        {
            var response = await this._client.GetAsync($"3/search/movie?query={title}");
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<TmdbResponseDTO<MovieResponseDTO>>(responseBody);
                return this._mapper.Map<IEnumerable<MovieDTO>>(result.Results);
            }

            throw new BadRequestException(response.ReasonPhrase);
        }

        public async Task<IEnumerable<MovieDTO>> GetTrending(string mediaType, string timeWindow)
        {
            var cacheOptions = timeWindow switch
            {
                "day" => new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30))
                    .SetAbsoluteExpiration(DateTimeOffset.Now.AddHours(24)),
                "week" => new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30))
                    .SetAbsoluteExpiration(DateTimeOffset.Now.AddDays(7)),
                _ => throw new BadRequestException("Invalid timewindow value it must be day or week")
            };

            var cacheScheme = timeWindow switch
            {
                "day" => CacheSchemes.DailyTrending,
                "week" => CacheSchemes.WeeklyTrending,
                _ => throw new BadRequestException("Invalid timewindow value it must be day or week")
            };

            var cachedTrending = await this._distributedCache.GetAsync(cacheScheme);

            if (cachedTrending != null)
            {
                var cachedTrendingJson = Encoding.UTF8.GetString(cachedTrending);
                var movies = JsonConvert.DeserializeObject<IEnumerable<MovieResponseDTO>>(cachedTrendingJson);
                return this._mapper.Map<IEnumerable<MovieDTO>>(movies);
            }

            var response = await this._client.GetAsync($"3/trending/{mediaType}/{timeWindow}");
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<TmdbResponseDTO<MovieResponseDTO>>(responseBody);
                var cachedJson = JsonConvert.SerializeObject(result.Results);
                await this._distributedCache.SetAsync(cacheScheme, Encoding.UTF8.GetBytes(cachedJson), cacheOptions);
                return this._mapper.Map<IEnumerable<MovieDTO>>(result.Results);
            }

            throw new BadRequestException(response.ReasonPhrase);
        }

        public async Task<MovieDTO> GetMovieDetails(int id, string append = "")
        {
            var url = $"3/movie/{id}";
            if (!string.IsNullOrWhiteSpace(append))
            {
                url += $"?append_to_response={append}";
            }
            var response = await this._client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<MovieResponseDTO>(responseBody);
                return this._mapper.Map<MovieDTO>(result);
            }

            throw new BadRequestException(response.ReasonPhrase);
        }

        private async Task<IEnumerable<GenreDTO>> GetAllGenres(string type)
        {
            var cachedGenres = await this._distributedCache.GetAsync(CacheSchemes.Genres);
            if (cachedGenres != null)
            {
                var cachedGenresJson = Encoding.UTF8.GetString(cachedGenres);
                return JsonConvert.DeserializeObject<IEnumerable<GenreDTO>>(cachedGenresJson);
            }

            var response = await this._client.GetAsync($"3/genre/{type}/list");
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var genresJsonJObject = JObject.Parse(responseBody);

                var genresJson = genresJsonJObject["genres"].Children().ToList();

                var genres = genresJson.Select(genre => genre.ToObject<GenreDTO>());
                var cacheOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(DateTimeOffset.Now.AddHours(6));
                await this._distributedCache.SetAsync(CacheSchemes.Genres, Encoding.UTF8.GetBytes(genresJsonJObject["genres"].ToString(Formatting.None)), cacheOptions);
                return genres;
            }

            throw new BadRequestException(response.ReasonPhrase);
        }
    }
}
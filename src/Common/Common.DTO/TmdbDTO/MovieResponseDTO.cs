using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.DTO.TmdbDTO
{
    public class MovieResponseDTO
    {
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        [JsonProperty("budget")]
        public long Budget { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        /// <summary>
        /// Movie duration in minutes
        /// </summary>
        [JsonProperty("runtime")]
        public int Runtime { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        [JsonProperty("revenue")]
        public long Revenue { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        [JsonProperty("spoken_languages")]
        public IEnumerable<LanguageDTO> SpokenLanguages { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        [JsonProperty("production_countries")]
        public IEnumerable<CountryDTO> ProductionCountries { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        [JsonProperty("production_companies")]
        public IEnumerable<ProductionCompanyDTO> ProductionCompanies { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        [JsonProperty("genres")]
        public IEnumerable<GenreDTO> Genres { get; set; }

        [JsonProperty("videos")]
        public TmdbResponseDTO<VideoDTO> Videos { get; set; }
    }
}
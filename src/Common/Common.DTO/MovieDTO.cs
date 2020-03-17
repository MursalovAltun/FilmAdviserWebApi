using System.Collections.Generic;
using Common.DTO.TmdbDTO;

namespace Common.DTO
{
    public class MovieDTO
    {
        public string PosterPath { get; set; }

        public bool Adult { get; set; }

        public string Overview { get; set; }

        public string ReleaseDate { get; set; }

        public int Id { get; set; }

        public string OriginalTitle { get; set; }

        public string Title { get; set; }

        public string BackdropPath { get; set; }

        public double Popularity { get; set; }

        public int VoteCount { get; set; }

        public bool Video { get; set; }

        public double VoteAverage { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        public long Budget { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        public string Tagline { get; set; }

        /// <summary>
        /// Movie duration in minutes
        /// </summary>
        public int Runtime { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        public long Revenue { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        public IEnumerable<string> Languages { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        public IEnumerable<string> ProductionCountries { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        public IEnumerable<ProductionCompanyDTO> ProductionCompanies { get; set; }

        /// <summary>
        /// Will be returned only in movie details request
        /// </summary>
        public IEnumerable<string> Genres { get; set; }

        public IEnumerable<VideoDTO> Videos { get; set; }
    }
}
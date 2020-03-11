using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.DTO.TmdbDTO
{
    public class TmdbResponseDTO<T> where T : class
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("total_results")]
        public long TotalResults { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("results")]
        public IEnumerable<T> Results { get; set; }
    }
}
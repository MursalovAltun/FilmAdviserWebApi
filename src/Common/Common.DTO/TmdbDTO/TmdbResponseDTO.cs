using System.Collections.Generic;

namespace Common.DTO.TmdbDTO
{
    public class TmdbResponseDTO<T> where T : class
    {
        public int Page { get; set; }

        public long TotalResults { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<T> Results { get; set; }
    }
}
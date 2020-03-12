using System.Collections.Generic;
using AutoMapper;
using Common.DTO.TmdbDTO;
using MovieDTO = Common.DTO.MovieDTO;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<DTO.TmdbDTO.MovieDTO, MovieDTO>()
                .ReverseMap()
                .AfterMap((source, destination) =>
                {
                    source.Genres = new List<GenreDTO>();

                });
        }
    }
}
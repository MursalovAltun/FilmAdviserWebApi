using System.Linq;
using AutoMapper;
using Common.DTO;
using Common.DTO.TmdbDTO;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieDTO, MovieResponseDTO>()
                .ReverseMap()
                .ForMember(x => x.Videos,
                    opt =>
                        opt.MapFrom(src => src.Videos.Results))
                .ForMember(x => x.Genres,
                        opt =>
                            opt.MapFrom(src => src.Genres.Select(x => x.Name)))
                .ForMember(x => x.Languages,
                    opt =>
                            opt.MapFrom(src => src.SpokenLanguages.Select(x => x.Name)));
        }
    }
}
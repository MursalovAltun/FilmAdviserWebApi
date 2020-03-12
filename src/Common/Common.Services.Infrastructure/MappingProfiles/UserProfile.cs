using System.Linq;
using AutoMapper;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(d => d.Address,
                    opt => opt.MapFrom(src => new AddressDTO(src.AddressCity, src.AddressStreet, src.AddressZipCode,
                        src.AddressLat, src.AddressLng)))
                .ForMember(d => d.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(r => r.Role.Name)))
                .ForMember(d => d.Login, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap()
                .ForMember(d => d.AddressStreet, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(d => d.AddressCity, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(d => d.AddressZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
                .ForMember(d => d.AddressLat, opt => opt.MapFrom(src => src.Address.Lat))
                .ForMember(d => d.AddressLng, opt => opt.MapFrom(src => src.Address.Lng))
                .ForMember(d => d.UserRoles, opt => opt.MapFrom(src => Enumerable.Empty<Role>()))
                .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.Login));
        }
    }
}
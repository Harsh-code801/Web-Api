using AutoMapper;
using NzWalks.Api.Dtos;
using NzWalks.Api.Models.Domain;

namespace NzWalks.Api.AutoMapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile() 
        {
            CreateMap<Walks, WalksDto>().ReverseMap();
            //CreateMap<List<Walks>, List<WalksDto>>(); Not working
            CreateMap<WalksDto, WalksInput>().ReverseMap();
            CreateMap<Walks, WalksInput>().ReverseMap();
        }
    }
}

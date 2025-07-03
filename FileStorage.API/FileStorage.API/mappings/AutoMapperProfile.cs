using AutoMapper;
using FileStorage.Application.DTOs;
using FileStorage.Domain.Entities;

namespace FileStorage.API.mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MetirialEntity, GetAllMetirealResponseDTO>().ReverseMap();
            CreateMap<MetirialEntity, CreateMetirealRequestDTO>().ReverseMap();
        }
    }
}

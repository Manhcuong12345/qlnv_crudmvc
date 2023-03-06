using AutoMapper;
using QuanLyNV.Dto;
using QuanLyNV.Models;

namespace QuanLyNV.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser,UserDto>().ReverseMap();
        }
    }
}

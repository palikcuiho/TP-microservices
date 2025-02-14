using AutoMapper;
using BorrowingService.DTO;
using BorrowingService.Models;

namespace BorrowingService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Borrowing, BorrowingDTO>().ReverseMap();
        }
    }
}

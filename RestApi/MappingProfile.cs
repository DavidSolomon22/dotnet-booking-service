using AutoMapper;
using Entities.DataTransferObjects.BookingDtos;
using Entities.DataTransferObjects.RoomDtos;
using Entities.Models;

namespace RestApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookingForCreationDto, Booking>().ReverseMap();
            CreateMap<Booking, BookingDto>();
            CreateMap<RoomForCreationDto, Room>();
            CreateMap<Room, RoomDto>();
        }
    }
}

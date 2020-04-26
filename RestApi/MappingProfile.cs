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
            CreateMap<RoomForCreationDto, Room>();
            CreateMap<RoomForUpdateDto, Room>();
            CreateMap<Room, RoomDto>();

            CreateMap<BookingForCreationDto, Booking>();
            CreateMap<Booking, BookingDto>();
        }
    }
}

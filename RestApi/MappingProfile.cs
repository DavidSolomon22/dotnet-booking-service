using AutoMapper;
using Entities.DataTransferObjects.Booking;
using Entities.DataTransferObjects.Room;
using Entities.Models;

namespace RestApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookingForCreationDto, Booking>();

            CreateMap<RoomForCreationDto, Room>();
            CreateMap<Room, RoomDto>();
        }
    }
}

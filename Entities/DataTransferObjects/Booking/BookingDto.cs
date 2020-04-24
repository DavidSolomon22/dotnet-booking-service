using System;

namespace Entities.DataTransferObjects.Booking
{
    public class BookingDto 
    {
        public int Id {get;set;}
        public int RoomId { get; set; } 
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}
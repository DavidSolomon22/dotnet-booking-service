using System;

namespace Entities.DataTransferObjects.BookingDtos
{
    public class BookingForUpdateDto
    {
        public int RoomId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
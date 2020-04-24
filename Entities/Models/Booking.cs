using System;

namespace Entities.Models
{
    public class Booking 
    {
        public int Id { get; set; }
        public int RoomId { get; set; } 
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Room Room { get; set; }
    }
}
using System.Collections.Generic;

namespace Entities.DataTransferObjects.Room
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public bool HasWhiteboard { get; set; }
        public bool HasProjector { get; set; }

        public ICollection<Entities.Models.Booking> Bookings { get; set; }
    }
}
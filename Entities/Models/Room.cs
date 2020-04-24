using System.Collections.Generic;

namespace Entities.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public bool HasWhiteboard { get; set; }
        public bool HasProjector { get; set; }

        public IEnumerable<Booking> Bookings { get; set; }
    }
}
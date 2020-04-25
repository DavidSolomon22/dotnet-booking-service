using System.Collections.Generic;
using Entities.Models;

namespace Entities.DataTransferObjects.RoomDtos
{
    public class RoomForCreationDto
    {
        public string Number { get; set; }
        public bool HasWhiteboard { get; set; }
        public bool HasProjector { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}


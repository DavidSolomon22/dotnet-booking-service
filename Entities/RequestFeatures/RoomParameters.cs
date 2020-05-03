using System;

namespace Entities.RequestFeatures
{
    public class RoomParameters : RequestParameters
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public bool ValidDateRange => End > Start;
    }
}
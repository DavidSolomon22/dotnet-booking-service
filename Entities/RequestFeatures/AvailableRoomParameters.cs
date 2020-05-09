using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Entities.RequestFeatures
{
    public class AvailableRoomParameters : RequestParameters
    {
        [BindRequired]
        public DateTime Start { get; set; }
        [BindRequired]
        public DateTime End { get; set; }

        public bool ValidDateRange => End > Start;
    }
}
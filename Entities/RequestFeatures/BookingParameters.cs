using System;

namespace Entities.RequestFeatures
{
  public class BookingParameters : RequestParameters
  {
    public DateTime Start { get; set; } = DateTime.MinValue;
    public DateTime End { get; set; } = DateTime.MaxValue;
  }
}
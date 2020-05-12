using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IBookingRepository
    {
        void CreateBooking(Booking booking);
        Task<IEnumerable<Booking>> GetAllBookingsAsync(BookingParameters bookingParameters, bool trackChanges);
        Task<Booking> GetBookingAsync(int bookingId, bool trackChanges);
        void DeleteBooking(Booking booking);
    }
}
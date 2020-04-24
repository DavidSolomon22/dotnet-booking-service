using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IBookingRepository
    {
        void CreateBooking(Booking booking);
        Task<IEnumerable<Booking>> GetAllBookingsAsync(bool trackChanges);
        Task<Booking> GetBookingAsync(int bookingId, bool trackChanges);
        void DeleteBooking(Booking booking);
    }
}
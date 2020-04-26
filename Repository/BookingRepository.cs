using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        public BookingRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateBooking(Booking booking) => Create(booking);

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(r => r.Start)
            .ToListAsync();

        public async Task<Booking> GetBookingAsync(int bookingId, bool trackChanges) =>
            await FindByCondition(r => r.Id.Equals(bookingId), trackChanges)
            .SingleOrDefaultAsync();

        public void DeleteBooking(Booking booking) => Delete(booking);
    }
}

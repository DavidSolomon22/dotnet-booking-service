using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
  public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
  {
    public BookingRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateBooking(Booking booking) => Create(booking);

    public async Task<IEnumerable<Booking>> GetAllBookingsAsync(BookingParameters bookingParameters, bool trackChanges) =>
        await FindByCondition(b => b.Start >= bookingParameters.Start && b.End <= bookingParameters.End, trackChanges)
        .OrderBy(r => r.Start)
        .Skip((bookingParameters.PageNumber - 1) * bookingParameters.PageSize)
        .Take(bookingParameters.PageSize)
        .ToListAsync();

    public async Task<Booking> GetBookingAsync(int bookingId, bool trackChanges) =>
        await FindByCondition(b => b.Id.Equals(bookingId), trackChanges)
        .SingleOrDefaultAsync();

    public void DeleteBooking(Booking booking) => Delete(booking);
  }
}

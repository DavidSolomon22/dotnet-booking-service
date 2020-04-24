using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IBookingRepository _bookingRepository;
        private IRoomRepository _roomRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IBookingRepository Booking
        {
            get
            {
                if (_bookingRepository == null)
                    _bookingRepository = new BookingRepository(_repositoryContext);

                return _bookingRepository;
            }
        }

        public IRoomRepository Room
        {
            get
            {
                if (_roomRepository == null)
                    _roomRepository = new RoomRepository(_repositoryContext);

                return _roomRepository;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}

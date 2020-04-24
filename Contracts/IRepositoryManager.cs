using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IBookingRepository Booking { get; }
        IRoomRepository Room { get; }
        Task SaveAsync();
    }
}

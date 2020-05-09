using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IRoomRepository
    {
        void CreateRoom(Room room);
        Task<IEnumerable<Room>> GetAllRoomsAsync(RoomParameters roomParameters, bool trackChanges);
        Task<IEnumerable<Room>> GetAllAvailableRoomsAsync(AvailableRoomParameters roomParameters, bool trackChanges);
        Task<Room> GetRoomAsync(int roomId, bool trackChanges);
        Task<IEnumerable<Room>> GetRoomsByIdsAsync(IEnumerable<int> ids, bool trackChanges);
        void DeleteRoom(Room room);
    }
}

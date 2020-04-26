using System;
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
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateRoom(Room room) => Create(room);

        public async Task<IEnumerable<Room>> GetAllRoomsAsync(RoomParameters roomParameters, bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(r => r.Number)
            .Skip((roomParameters.PageNumber - 1) * roomParameters.PageSize)
            .Take(roomParameters.PageSize) 
            .ToListAsync();

        public async Task<Room> GetRoomAsync(int roomId, bool trackChanges) =>
            await FindByCondition(r => r.Id.Equals(roomId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Room>> GetRoomsByIdsAsync(IEnumerable<int> ids, bool trackChanges) =>
            await FindByCondition(r => ids.Contains(r.Id), trackChanges)
            .ToListAsync();

        public void DeleteRoom(Room room) => Delete(room);
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.RoomDtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Entities.RequestFeatures;

namespace RestApi.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public RoomsController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody]RoomForCreationDto room)
        {
            if (room == null)
            {
                return BadRequest("RoomForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var roomEntity = _mapper.Map<Room>(room);

            _repository.Room.CreateRoom(roomEntity);
            await _repository.SaveAsync();

            var roomToReturn = _mapper.Map<RoomDto>(roomEntity);

            return CreatedAtRoute("RoomById", new { id = roomToReturn.Id }, roomToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms([FromQuery] RoomParameters roomParameters)
        {
            var rooms = await _repository.Room.GetAllRoomsAsync(roomParameters, trackChanges: false);

            var roomsDto = _mapper.Map<IEnumerable<RoomDto>>(rooms);

            return Ok(roomsDto);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRooms([FromQuery] AvailableRoomParameters roomParameters)
        {
            if (!roomParameters.ValidDateRange)
            {
                return BadRequest(new { message = "Start date can't be earlier than end date." });
            }

            var rooms = await _repository.Room.GetAllAvailableRoomsAsync(roomParameters, trackChanges: false);

            if (rooms.Count() == 0) 
            {
                return NoContent();
            }

            var roomsDto = _mapper.Map<IEnumerable<RoomDto>>(rooms);

            return Ok(roomsDto);
        }

        [HttpGet("{id}", Name = "RoomById")]
        public async Task<IActionResult> GetRoom(int id)
        {
            var room = await _repository.Room.GetRoomAsync(id, trackChanges: false);
            if (room == null)
            {
                return NotFound();
            }
            else
            {
                var roomDto = _mapper.Map<RoomDto>(room);
                return Ok(roomDto);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomForUpdateDto room)
        {
            if(room == null)
            {
                return BadRequest("RoomForUpdateDto object is null");
            }

            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var roomEntity = await _repository.Room.GetRoomAsync(id, trackChanges: true);
            if(roomEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(room, roomEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _repository.Room.GetRoomAsync(id, trackChanges: false);
            if(room == null)
            {
                return NotFound();
            }

            _repository.Room.DeleteRoom(room);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.RoomDtos;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi.ModelBinders;

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
        public async Task<IActionResult> CreateRoom(RoomForCreationDto room)
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
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.BookingDtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public BookingsController(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _repository.Booking.GetAllBookingsAsync(trackChanges: false);

            var bookingDtos = _mapper.Map<IEnumerable<BookingDto>>(bookings);

            return Ok(bookingDtos);
        }

        [HttpGet("{id}", Name = "BookingById")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _repository.Booking.GetBookingAsync(id, trackChanges: false);
            if (booking == null)
            {
                return NotFound();
            }

            var bookingDto = _mapper.Map<BookingDto>(booking);
            return Ok(bookingDto);

        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingForCreationDto booking)
        {
            if (booking == null)
            {
                return BadRequest("BookingForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var room = await _repository.Room.GetRoomAsync(booking.RoomId, true);

            if (room == null)
            {
                return BadRequest();
            }

            var reserved = CheckIfRoomIsReserved(room, booking.Start, booking.End);

            if (reserved)
            {
                return Conflict("Room is already reserved for this time.");
            }

            var bookingEntity = _mapper.Map<Booking>(booking);

            _repository.Booking.CreateBooking(bookingEntity);

            await _repository.SaveAsync();

            var bookingToReturn = _mapper.Map<BookingDto>(bookingEntity);

            return CreatedAtRoute("BookingById", new { id = bookingToReturn.Id }, bookingToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _repository.Booking.GetBookingAsync(id, trackChanges: false);
            if (booking == null)
            {
                return NotFound();
            }

            _repository.Booking.DeleteBooking(booking);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingForUpdateDto booking)
        {
            if (booking == null)
            {
                return BadRequest("BookingForUpdateDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var bookingEntity = await _repository.Booking.GetBookingAsync(id, trackChanges: true);

            if (bookingEntity == null)
            {
                return NotFound();
            }

            var room = await _repository.Room.GetRoomAsync(bookingEntity.RoomId, trackChanges: false);

            if (room == null)
            {
                return BadRequest();
            }

            var reserved = CheckIfRoomIsReserved(room, booking.Start, booking.End);

            if (reserved)
            {
                return Conflict("Room is already reserved for this time.");
            }

            _mapper.Map(booking, bookingEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        private bool CheckIfRoomIsReserved(Room room, DateTime start, DateTime end)
        {
            if (room.Bookings.Any(x => (start.Ticks >= x.Start.Ticks && start.Ticks <= x.End.Ticks) || (end.Ticks >= x.Start.Ticks && end.Ticks <= x.End.Ticks)))
            {
                return true;
            }
            return false;
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.BookingDtos;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi.ModelBinders;

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
        public async Task<IActionResult> CreateRecipe([FromBody] BookingForCreationDto booking)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var room = await _repository.Room.GetRoomAsync(booking.RoomId, false);

            if (room == null)
            {
                return BadRequest();
            }

            if (room.Bookings.Any(x =>
             ((booking.Start >= x.Start && booking.Start <= x.End) || (booking.End >= x.Start && booking.End <= x.End))
                && x.RoomId == booking.RoomId))
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

    }
}

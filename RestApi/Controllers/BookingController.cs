using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Booking;
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
             ((booking.Start >= x.Start && booking.Start <= x.End) || (booking.End >= x.Start && booking.End <= x.End)) && x.RoomId == booking.RoomId))
            {
                return Conflict("Room is already reserver for this time");
            }

            var bookingEntity = _mapper.Map<Booking>(booking);

            _repository.Booking.CreateBooking(bookingEntity);

            await _repository.SaveAsync();

            var bookingToReturn = _mapper.Map<BookingDto>(bookingEntity);

            return CreatedAtRoute("BookingById", new { id = bookingToReturn.Id }, bookingToReturn);
        }

        [HttpGet("{id}", Name = "BookingById")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            var recipe = await _repository.Booking.GetBookingAsync(id, trackChanges: false);
            if (recipe == null)
            {
                return NotFound();
            }
            else
            {
                var recipeDto = _mapper.Map<RecipeDto>(recipe);
                return Ok(recipeDto);
            }
        }


        //     var recipeEntities = _mapper.Map<IEnumerable<Recipe>>(recipeCollection);
        //     foreach (var recipe in recipeEntities)
        //     {
        //         _repository.Recipe.CreateRecipe(recipe);
        //     }
        //     await _repository.SaveAsync();

        //     var recipeCollectionToReturn = _mapper.Map<IEnumerable<RecipeDto>>(recipeEntities);
        //     var ids = string.Join(",", recipeCollectionToReturn.Select(c => c.Id));

        //     return CreatedAtRoute("RecipeCollection", new { ids }, recipeCollectionToReturn);
        // }

        // [HttpGet, Authorize]
        // public async Task<IActionResult> GetRecipes()
        // {
        //     var recipes = await _repository.Recipe.GetAllRecipesAsync(trackChanges: false);

        //     var recipesDto = _mapper.Map<IEnumerable<RecipeDto>>(recipes);

        //     return Ok(recipesDto);
        // }



        // [HttpGet("collection/({ids})", Name = "RecipeCollection"), Authorize]
        // public async Task<IActionResult> GetRecipeCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        // {
        //     if (ids == null)
        //     {
        //         return BadRequest("Parameter ids is null");
        //     }

        //     var recipeEntities = await _repository.Recipe.GetRecipesByIdsAsync(ids, trackChanges: false);

        //     if (ids.Count() != recipeEntities.Count())
        //     {
        //         return NotFound();
        //     }

        //     var recipesToReturn = _mapper.Map<IEnumerable<RecipeDto>>(recipeEntities);
        //     return Ok(recipesToReturn);
        // }

        // [HttpPut, DisableRequestSizeLimit, Authorize]
        // public async Task<IActionResult> UpdateRecipe([FromForm]Guid id, [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "recipe")][FromForm] RecipeForCreationDto recipe, [FromForm] IFormFile file)
        // {
        //     if (recipe == null)
        //     {
        //         return BadRequest("RecipeForUpdateDto object is null");
        //     }

        //     if (!ModelState.IsValid)
        //     {
        //         return UnprocessableEntity(ModelState);
        //     }

        //     var recipeEntity = await _repository.Recipe.GetRecipeAsync(id, trackChanges: true);

        //     if (recipeEntity == null)
        //     {
        //         return NotFound();
        //     }

        //     var oldPhotoPath = recipeEntity.PhotoPath;

        //     _photoService.DeletePhoto(oldPhotoPath);

        //     var photoPath = _photoService.UploadFile(file);

        //     _mapper.Map(recipe, recipeEntity);

        //     recipeEntity.PhotoPath = photoPath;

        //     await _repository.SaveAsync();

        //     return NoContent();
        // }

        // [HttpDelete("{id}"), Authorize]
        // public async Task<IActionResult> DeleteRecipe(Guid id)
        // {
        //     var recipe = await _repository.Recipe.GetRecipeAsync(id, trackChanges: false);
        //     if (recipe == null)
        //     {
        //         return NotFound();
        //     }

        //     var photoPath = recipe.PhotoPath;

        //     _photoService.DeletePhoto(photoPath);

        //     _repository.Recipe.DeleteRecipe(recipe);
        //     await _repository.SaveAsync();

        //     return NoContent();
        // }

        // [HttpGet("photo/{id}")]
        // public async Task<IActionResult> GetRecipePhoto(Guid id)
        // {
        //     var recipe = await _repository.Recipe.GetRecipeAsync(id, trackChanges: false);

        //     if (recipe == null)
        //     {
        //         return NotFound();
        //     }
        //     else if(recipe.PhotoPath == null)
        //     {
        //         return NotFound();
        //     }

        //     var recipePhotoPath = recipe.PhotoPath;

        //     var recipePhoto = await _photoService.GetPhoto(recipePhotoPath);

        //     return File(recipePhoto, _photoService.GetContentType(recipePhotoPath), Path.GetFileName(recipePhotoPath));
        // }
    }
}

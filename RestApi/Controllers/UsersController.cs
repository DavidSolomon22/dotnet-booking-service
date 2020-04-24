using Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using Entities.DataTransferObjects;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using RestApi.ModelBinders;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using System.IO;
using System;
using Microsoft.AspNetCore.Http;

namespace RestApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        // private readonly IRepositoryManager _repository;
        // private readonly IMapper _mapper;
        // private readonly IPhotoService _photoService;

        // public UsersController(IRepositoryManager repository, IMapper mapper, IPhotoService photoService)
        // {
        //     _repository = repository;
        //     _mapper = mapper;
        //     _photoService = photoService;
        // }

        // [HttpGet]
        // public async Task<IActionResult> GetUsers()
        // {
        //     var users = await _repository.User.GetAllUsersAsync(trackChanges: false);

        //     var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

        //     return Ok(usersDto);
        // }

        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetUser(string id)
        // {
        //     var user = await _repository.User.GetUserAsync(id, trackChanges: false);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //     else
        //     {
        //         var userDto = _mapper.Map<UserDto>(user);

        //         return Ok(userDto);
        //     }
        // }

        // [HttpGet("{id}/photo")]
        // public async Task<IActionResult> GetUserPhoto(string id)
        // {
        //     var user = await _repository.User.GetUserAsync(id,trackChanges: false);

        //     if(user == null)
        //     {
        //         return NotFound();
        //     }
        //     else if( user.PhotoPath == null)
        //     {
        //         return NotFound();
        //     }

        //     var photoPath = user.PhotoPath;

        //     var userPhoto = await _photoService.GetPhoto(photoPath);

        //     return File(userPhoto, _photoService.GetContentType(photoPath), Path.GetFileName(photoPath));
        // }

        // [HttpPost("{id}/photo"), DisableRequestSizeLimit]
        // public async Task<IActionResult> PostUserPhoto(string id, [FromForm] IFormFile file)
        // {
        //     var user = await _repository.User.GetUserAsync(id,trackChanges: true);

        //     if(user == null)
        //     {
        //         return BadRequest("User doesn't exist");
        //     }

        //     if (!ModelState.IsValid)
        //     {
        //         return UnprocessableEntity(ModelState);
        //     }

        //     if(file == null)
        //     {
        //         return BadRequest("File is not provided");
        //     }

        //     var photoPath = _photoService.UploadFile(file);

        //     user.PhotoPath = photoPath;

        //     await _repository.SaveAsync();

        //     return NoContent();

        // }

        // [HttpGet("collection/({ids})")]
        // public async Task<IActionResult> GetUserCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<string> ids)
        // {
        //     if(ids == null)
        //     {
        //         return BadRequest("Parameter ids is null");
        //     }

        //     var userEntities = await _repository.User.GetUsersByIdsAsync(ids, trackChanges: false);

        //     if(ids.Count() != userEntities.Count())
        //     {
        //         return NotFound();
        //     }

        //     var usersToReturn = _mapper.Map<IEnumerable<UserDto>>(userEntities);

        //     return Ok(usersToReturn);
        // }

        // [HttpPatch("{id}")]
        // public async Task<IActionResult> PartiallyUpdateUser(string id, [FromBody] JsonPatchDocument<UserForUpdateDto> patchDoc)
        // {
        //     if(patchDoc == null)
        //     {
        //         return BadRequest("patchDoc object is null");
        //     }

        //     var userEntity = await _repository.User.GetUserAsync(id, trackChanges: true);
        //     if(userEntity == null)
        //     {
        //         NotFound();
        //     }

        //     var userToPatch = _mapper.Map<UserForUpdateDto>(userEntity);

        //     patchDoc.ApplyTo(userToPatch);
        //     _mapper.Map(userToPatch, userEntity);

        //     await _repository.SaveAsync();

        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteUser(string id)
        // {
        //     var user = await _repository.User.GetUserAsync(id, trackChanges: false);
        //     if(user == null)
        //     {
        //         return NotFound();
        //     }

        //     _repository.User.DeleteUser(user);
        //     await _repository.SaveAsync();

        //     return NoContent();
        // }
    }
}

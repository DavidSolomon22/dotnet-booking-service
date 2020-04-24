using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace RestApi.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        // private readonly IMapper _mapper;
        // private readonly UserManager<User> _userManager;
        // private readonly IAuthenticationManager _authManager;
        // private readonly IEmailSender _emailSender;
        // private readonly IConfiguration _configuration;
        // private readonly ILoggerManager _logger;


        // public AuthenticationController(UserManager<User> userManager, IMapper mapper,
        //                                 IAuthenticationManager authManager, IEmailSender emailSender,
        //                                 IConfiguration configuration, ILoggerManager logger)
        // {
        //     _mapper = mapper;
        //     _userManager = userManager;
        //     _authManager = authManager;
        //     _emailSender = emailSender;
        //     _configuration = configuration;
        //     _logger = logger;
        // }

        // [HttpPost("register")]
        // public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        // {
        //     var user = _mapper.Map<User>(userForRegistration);

        //     var result = await _userManager.CreateAsync(user, userForRegistration.Password);

        //     if (!result.Succeeded)
        //     {
        //         foreach (var error in result.Errors)
        //         {
        //             ModelState.TryAddModelError(error.Code, error.Description);
        //         }
        //         return BadRequest(ModelState);
        //     }

        //     var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        //     var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { confirmationToken, email = user.Email }, Request.Scheme);

        //     var message = new Message(new string[] { user.Email }, "Account confirmation email link", confirmationLink);

        //     await _emailSender.SendEmailAsync(message);

        //     await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

        //     return StatusCode(201);
        // }

        // [HttpGet]
        // public async Task<IActionResult> ConfirmEmail(string confirmationToken, string email)
        // {
        //     var user = await _userManager.FindByEmailAsync(email);

        //     if (user == null)
        //     {
        //         return BadRequest("User with such e-mail doesn't exist.");
        //     }

        //     var result = await _userManager.ConfirmEmailAsync(user, confirmationToken);
        //     var url = _configuration.GetSection("Frontend:Url").Value + "/home";

        //     return Redirect(url);
        // }

        // [HttpPost("login")]
        // public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        // {
        //     _logger.LogError("Jestem w Authenticate");
        //     if (!await _authManager.ValidateUser(user))
        //     {
        //         return Unauthorized(new { error = "Wrong email or password" });
        //     }

        //     var token = await _authManager.CreateToken();

        //     var userId = await _authManager.GetUserId(user.Email);

        //     var expirationDate = _authManager.GetExpirationDate(token);
            
        //     if(!await _authManager.IsEmailConfirmed(user.Email))
        //     {
        //         return Unauthorized(new { error = "Email not confirmed." });
        //     }

        //     return Ok(new { token, userId, expirationDate });
        // }
    }
}

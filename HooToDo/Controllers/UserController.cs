using AutoMapper;
using HooToDo.Domain.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HooToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpPost("authorize")]
        public async Task<IActionResult> Authorize([FromBody] AuthorizeRequest request)
        {
            if (!ModelState.IsValid)
                return Unauthorized();
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
                return NotFound();
            var checkPassResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!checkPassResult.Succeeded)
                return Unauthorized("Wrong Password");
            await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);
            var isSignedIn = _signInManager.IsSignedIn(User);
            if (isSignedIn)
                return NoContent();
            return Unauthorized("Not Signed in");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = _mapper.Map<UserModel>(request);
            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (createResult.Succeeded)
                return CreatedAtAction(nameof(Authorize), new { user.UserName });
            return BadRequest();
        }
        [Authorize]
        [HttpGet("signout")]
        public async Task<IActionResult> Singout() 
        {
            await _signInManager.SignOutAsync();
            return SignOut();
        }
        [HttpGet("unauthorized")]
        public IActionResult UnauthorizedAction()
        {
            return StatusCode(StatusCodes.Status401Unauthorized);
        }


    }
}

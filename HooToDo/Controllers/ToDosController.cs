using AutoMapper;
using HooToDo.Domain.Models.ToDo;
using HooToDo.Domain.Models.User;
using HooToDo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HooToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly ToDoContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<UserModel> _userManager;

        public ToDosController(ToDoContext context, IMapper mapper, UserManager<UserModel> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        [Authorize]
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var user = await _userManager.GetUserAsync(User);
            var todos = await _context.ToDos
                .Include(t => t.User)
                .Where(t => t.UserId == user.Id)
                .ToListAsync();
            var response = _mapper.Map<IEnumerable<ToDoResponse>>(todos);
            return Ok(response);
        }
        [Authorize]
        [HttpPost("set")]
        public async Task<IActionResult> Set(SetToDoRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            var model = _mapper.Map<ToDoModel>(request);
            model.UserId = user.Id;
            await _context.ToDos.AddAsync(model);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

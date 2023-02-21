using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OOTF.Shopping.Interfaces;
using OOTF.Shopping.Models;
using System.Data;

namespace OOTF.Shopping.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            try
            {
                var addedUser = await _userService.AddUserAsync(user);
                return CreatedAtAction(nameof(GetUserById), new { id = addedUser.Id }, addedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedUser = await _userService.UpdateUserAsync(user);
                if (updatedUser == null)
                {
                    return NotFound();
                }
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            try
            {
                var deletedUser = await _userService.DeleteUserAsync(id);
                if (deletedUser == null)
                {
                    return NotFound();
                }
                return Ok(deletedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

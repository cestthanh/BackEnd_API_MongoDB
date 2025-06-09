using Microsoft.AspNetCore.Mvc;
using UserApiDemo.Models;
using UserApiDemo.Services;

namespace UserApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            return _userService.GetAll();
        }

        // GET: api/users/1
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult AddUser([FromBody] User user)
        {
            _userService.Add(user);
            return Ok("User added");
        }

        // DELETE: api/users/1
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var existingUser = _userService.GetById(id);
            if (existingUser == null)
                return NotFound("User not found");

            _userService.Delete(id);
            return Ok("User deleted");
        }

        // POST: api/users/login
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequest request)
        {
            var allUsers = _userService.GetAll();
            var user = allUsers.FirstOrDefault(u =>
                u.Email == request.Email && u.Phone == request.Phone);

            if (user == null)
                return Unauthorized("Wrong email or phone number.");

            return Ok($"Login successfully. Hello {user.Name}!");
        }
    }
}

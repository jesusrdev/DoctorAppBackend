using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace API.Controllers
{
    [Route("api/[controller]")] //? api/user HttpGet, HttpPost
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet] // api/user
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _db.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]   //  api/user/[id]
        public ActionResult<User> GetUser(int id)
        {
            var user = _db.Users.Find(id);
            return Ok(user);
        }
    }
}

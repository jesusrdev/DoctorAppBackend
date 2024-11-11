using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet] // api/user
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _db.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]   //  api/user/[id]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _db.Users.FindAsync(id);
            return Ok(user);
        }
    }
}

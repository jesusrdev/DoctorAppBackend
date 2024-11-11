using System.Security.Cryptography;
using System.Text;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
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

        [HttpPost("sign-up")] // POST: api/user/registro
        public async Task<ActionResult<User>> SignUp(SignUpDto signUpDto)
        {
            if (await UserExist(signUpDto.Username)) return BadRequest("Username already exist");

            using var hmac = new HMACSHA512();
            var user = new User
            {
                Username = signUpDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signUpDto.Password)),
                PasswordSalt = hmac.Key
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return user;
        }

        private async Task<bool> UserExist(string username)
        {
            return await _db.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }
    }
}

using System.Security.Cryptography;
using System.Text;
using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Models.Entities;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        
        private readonly ApplicationDbContext _db;
        private readonly ITokenService _tokenService;

        public UserController(ApplicationDbContext db, ITokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
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
        public async Task<ActionResult<UserDto>> SignUp(SignUpDto signUpDto)
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

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user),
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Username == loginDto.Username);

            if (user == null) return Unauthorized("Invalid user");
            
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Incorrect Password");
            }
            // if (computedHash.Where((t, i) => t != user.PasswordHash[i]).Any())
            // {
            //     return Unauthorized("Incorrect Password");
            // }

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user),
            };
        }
        
        

        private async Task<bool> UserExist(string username)
        {
            return await _db.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }
    }
}

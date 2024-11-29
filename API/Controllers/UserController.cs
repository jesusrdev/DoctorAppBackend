using System.Security.Cryptography;
using System.Text;
using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Models.Entities;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        
        private readonly UserManager<UserApplication> _userManager;
        private readonly ITokenService _tokenService;

        public UserController(UserManager<UserApplication> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        // [Authorize]
        // [HttpGet] // api/user
        // public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        // {
        //     var users = await _db.Users.ToListAsync();
        //     return Ok(users);
        // }

        // [Authorize]
        // [HttpGet("{id}")]   //  api/user/[id]
        // public async Task<ActionResult<User>> GetUser(int id)
        // {
        //     var user = await _db.Users.FindAsync(id);
        //     return Ok(user);
        // }

        [HttpPost("sign-up")] // POST: api/user/registro
        public async Task<ActionResult<UserDto>> SignUp(SignUpDto signUpDto)
        {
            if (await UserExist(signUpDto.Username)) return BadRequest("Username already exist");

            var user = new UserApplication()
            {
                UserName = signUpDto.Username.ToLower(),
                Email = signUpDto.Email,
                Lastname = signUpDto.Lastname,
                Firstname = signUpDto.Firstname,
            };

            var result = await _userManager.CreateAsync(user, signUpDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, signUpDto.Role);
            if (!roleResult.Succeeded) return BadRequest("Error at adding Role to User");
            
            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Invalid user");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result) return Unauthorized("Password incorrect!");

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };
        }
        
        

        private async Task<bool> UserExist(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}

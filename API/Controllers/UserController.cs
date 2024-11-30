using System.Net;
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
        private ApiResponse _response;
        private readonly RoleManager<RoleApplication> _roleManager;

        public UserController(UserManager<UserApplication> userManager, ITokenService tokenService,
            RoleManager<RoleApplication> roleManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _response = new();
            _roleManager = roleManager;
        }

        [Authorize(Policy = "AdminRole")]
        [HttpGet] // api/user
        public async Task<ActionResult> GetUsers()
        {
            var users = await _userManager.Users
                .Select(u => new ListUserDto()
                {
                    Username = u.UserName,
                    Lastname = u.Lastname,
                    Firstname = u.Firstname,
                    Email = u.Email,
                    Role = string.Join(",", u.UserRoles.Select(ur => ur))
                })
                .ToListAsync();

            foreach (var user in users)
            {
                var appUser = await _userManager.FindByNameAsync(user.Username);
                user.Role = string.Join(",", await _userManager.GetRolesAsync(appUser));
            }

            _response.Result = users;
            _response.isSuccessfull = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // [Authorize]
        // [HttpGet("{id}")]   //  api/user/[id]
        // public async Task<ActionResult<User>> GetUser(int id)
        // {
        //     var user = await _db.Users.FindAsync(id);
        //     return Ok(user);
        // }

        [Authorize(Policy = "AdminRole")]
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

        [Authorize(Policy = "AdminRole")]
        [HttpGet("list-roles")]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.Select(r => new { RoleName = r.Name }).ToList();
            _response.Result = roles;
            _response.isSuccessfull = true;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }


        private async Task<bool> UserExist(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}
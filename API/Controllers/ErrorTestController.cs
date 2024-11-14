using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace API.Controllers;

public class ErrorTestController : BaseApiController
{
    private readonly ApplicationDbContext _db;

    public ErrorTestController(ApplicationDbContext db)
    {
        _db = db;
    }

    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetNotAuhorized()
    {
        return "Not authorized";
        return Unauthorized();
    }

    [HttpGet("not-found")]
    public ActionResult<User> GetNotFound()
    {
        return NotFound();
    }

    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {
        var obj = _db.Users.Find(-1);
        var objectString = obj.ToString();

        return objectString;
        
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest();
    }
}
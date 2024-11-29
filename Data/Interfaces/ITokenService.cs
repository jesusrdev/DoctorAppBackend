using Models.Entities;

namespace Data.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(UserApplication user);
}
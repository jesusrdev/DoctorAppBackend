using Models.Entities;

namespace Data.Interfaces;

public interface ITokenService
{
    string CreateToken(UserApplication user);
}
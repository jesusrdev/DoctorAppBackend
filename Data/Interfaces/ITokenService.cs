using Models.Entities;

namespace Data.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}
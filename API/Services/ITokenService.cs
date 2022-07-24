using Domain.DTOs.Users;
using Domain.Users;

namespace API.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}

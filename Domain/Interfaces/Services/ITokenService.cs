using Domain.DTOs.Users;

namespace API.Services
{
    public interface ITokenService
    {
        string CreateToken(UserDetailResponse user);
    }
}
using Domain.DTOs.Users;
using Domain.DTOs.Users.UpdateUser;
using Domain.Users;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDetailResponse> GetUserByUserName(string userName);

        Task<UserDetailResponse> GetUser(int userId);

        Task<UserDetailResponse> CreateUser(RegisterRequest registerRequest);

        Task<User> Login(LoginRequest loginRequest);

        Task<UpdateUserResponse> UpdateUser(UpdateUserRequest userInput);

        Task<UpdateUserResponse> ChangeEmail(string email);
    }
}
using AutoMapper;
using Domain.DTOs.Projects.GetProject;
using Domain.DTOs.Users;
using Domain.DTOs.Users.UpdateUser;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Domain.Projects;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectMemberRepository _projectMemberRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository,
            IProjectMemberRepository projectMemberRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(httpContextAccessor)
        {
            _userRepository = userRepository;
            _projectMemberRepository = projectMemberRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserDetailResponse> CreateUser(RegisterRequest registerRequest)
        {
            try
            {
                if (await _userRepository.GetAsync(s => s.UserName == registerRequest.UserName) != null)
                {
                    return null;
                }
                if (await _userRepository.GetAsync(s => s.Email == registerRequest.Email) != null)
                {
                    return null;
                }
                using var hmac = new HMACSHA512();
                var user = new User()
                {
                    UserName = registerRequest.UserName.ToLower(),
                    Email = registerRequest.Email,
                    Name = registerRequest.Name,
                    YearOfBirth = registerRequest.YearOfBirth,
                    Address = registerRequest.Address,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerRequest.Password)),
                    PasswordSalt = hmac.Key
                };
                await _userRepository.AddAsync(user);
                user.CreateDefaultProject();

                await _unitOfWork.SaveChangesAsync();
                
                return await GetUser(user.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<User> Login(LoginRequest loginRequest)
        {
            try
            {
                var user = await _userRepository.GetAsync(s => s.UserName == loginRequest.UserName.ToLower());
                if (user == null) return null;

                using var hmac = new HMACSHA512(user.PasswordSalt);

                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginRequest.Password));

                for (var i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != user.PasswordHash[i]) return null;
                }
                return user;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<UserDetailResponse> GetUserByUserName(string username)
        {
            try
            {
                var user = await _userRepository.GetAsync(s => s.UserName == username);
                if (user == null) return null;
                var projects = await _projectMemberRepository.GetAllProject(user.Id);
                var userMapper = _mapper.Map<UserDetailResponse>(user);
                var projectMappers = _mapper.Map<List<ProjectResponse>>(projects);
                userMapper.ListProject = projectMappers;
                return userMapper;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<UserDetailResponse> GetUser(int userId)
        {
            try
            {
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (user == null) return null;
                var projects = await _projectMemberRepository.GetAllProject(user.Id);
                var userMapper = _mapper.Map<UserDetailResponse>(user);
                var projectMappers = _mapper.Map<List<ProjectResponse>>(projects);
                userMapper.ListProject = projectMappers;
                return userMapper;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest userInput)
        {
            try
            {
                var username =  _httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Name).Value;
                var user = await _userRepository.GetAsync(s => s.UserName == username);
                if (user == null) return null;

                user.Update(userInput.Name,
                    userInput.YearOfBirth,
                    userInput.Address);

                return _mapper.Map<UpdateUserResponse>(user);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<UpdateUserResponse> ChangeEmail(string email)
        {
            try
            {
                var username =  _httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Name).Value;
                var user = await _userRepository.GetAsync(s => s.UserName == username);
                if (user == null) return null;

                if ((await _userRepository.GetAsync(s => s.Email == email)) != null) return null;
                user.ChangeEmail(email);

                return _mapper.Map<UpdateUserResponse>(user);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

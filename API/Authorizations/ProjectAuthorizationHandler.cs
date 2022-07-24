using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Projects;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace API.Authorizations
{
    public class ProjectAuthorizationHandler : AuthorizationHandler<SameAuthorRequirement, Project>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProjectMemberRepository _projectMemberRepository;
        private readonly IUserRepository _userRepository;
        public ProjectAuthorizationHandler(IProjectMemberRepository projectMemberRepository,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _projectMemberRepository = projectMemberRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            SameAuthorRequirement requirement,
            Project resource)
        {
            var username =  _httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
            var user = await _userRepository.GetAsync(s => s.UserName == username);
            if ((await _projectMemberRepository.GetAllMember(resource.Id)).Contains(user))
            {
                context.Succeed(requirement);
            }
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace API.Services
{
    public class BaseService
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public BaseService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
using API.Authorizations;
using API.DomainHandlers;
using API.Exceptions.Unautorizations;
using API.Services;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Domain.ListTasks;
using Domain.Projects;
using Domain.Tasks;
using Domain.Users;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.ApplicationTier.API.Profiles;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IAsyncRepository<>), typeof(Repository<>))
                .AddScoped<IMemberRepository, MemberRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ITaskItemRepository, TaskItemRepository>()
                .AddScoped<IProjectMemberRepository, ProjectMemberRepository>()
                .AddScoped<IListTaskRepository, ListTaskRepository>()
                .AddScoped<IAssignmentRepository, AssignmentRepository>()
                .AddScoped<ITagRepository, TagRepository>()
                .AddScoped<ITagMappingRepository, TagMappingRepository>()
                .AddScoped<IListTodoRepository, ListTodoRepository>()
                .AddScoped<ITodoItemRepository, TodoItemRepository>()
                .AddScoped<IAttachmentRepository, AttachmentRepository>()
                .AddScoped<IProjectRepository, ProjectRepository>();
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services
            , IConfiguration configuration)
        {
            return services.AddDbContext<EFContext>(options =>
                     options.UseSqlServer("server=ADMIN\\MINHTRI;database=TaskManagement;user id=sa;password=123456;"));
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services
           )
        {
            services.AddMediatR(typeof(DeleteListTaskDomainEventHandler).Assembly);
            services.AddScoped<IAuthorizationHandler, ProjectAuthorizationHandler>();
            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskItemService, TaskItemService>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }

        public static IServiceCollection AddExceptionServices(this IServiceCollection services
          )
        {
            services.AddControllers(options =>
                options.Filters.Add(new NotFoundExceptionFilter()));
            services.AddControllers(options =>
                options.Filters.Add(new UnauthorizationExceptionFilter()));
            return services;
        }
    }
}
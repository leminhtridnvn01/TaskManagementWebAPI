using API.Exceptions.Unautorizations;
using API.Extensions;
using AutoMapper;
using Domain.DTOs.ListTasks.AddListTask;
using Domain.DTOs.ListTasks.GetListTask;
using Domain.DTOs.Projects.AddProject;
using Domain.DTOs.Projects.GetProject;
using Domain.DTOs.Projects.UpdateProject;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Domain.ListTasks;
using Domain.Projects;
using Domain.Projects.Events;
using Domain.Users;
using Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Services
{
    public class ProjectService : BaseService, IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectMemberRepository _projectMemberRepository;
        private readonly IUserRepository _userRepository;
        private readonly IListTaskRepository _listTaskRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IHttpContextAccessor httpContextAccessor,
            IProjectRepository projectRepository,
            IProjectMemberRepository projectMemberRepository,
            IUserRepository userRepository,
            IListTaskRepository listTaskRepository,
            IMediator mediator,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(httpContextAccessor)
        {
            _userRepository = userRepository;
            _listTaskRepository = listTaskRepository;
            _projectRepository = projectRepository;
            _projectMemberRepository = projectMemberRepository;
            _mediator = mediator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        #region Get
        public async Task<ProjectDetailResponse> GetProject(int projectId)
        {
            try
            {
                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(projectId)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                var project = await _projectRepository.GetAsync(s => s.Id == projectId);
                if (project == null) throw new NotFoundException("Project is not found!"); 

                var projectMapper = _mapper.Map<ProjectDetailResponse>(project);
                var members = await _projectMemberRepository.GetAllMember(projectId);
                projectMapper.ListMember = _mapper.Map<List<UserResponse>>(members);

                return projectMapper;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Post
        public async Task<ProjectDetailResponse> CreateProject(AddProjectRequest projectInput)
        {
            try
            {
                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);

                var newProject = new Project(projectInput.Name, projectInput.Description);
                newProject.AddMember(user);
                await _projectRepository.AddAsync(newProject);

                await _unitOfWork.SaveChangesAsync();

                return await GetProject(newProject.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ProjectDetailResponse> AddMember(int memberId, int projectId)
        {
            try
            {
                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(projectId)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                var member = await _userRepository.GetAsync(s => s.Id == memberId);
                var project = await _projectRepository.GetAsync(s => s.Id == projectId);
                if (member == null) throw new NotFoundException("Member is not found!");
                if (project == null) throw new NotFoundException("Project is not found!");

                if (await _projectMemberRepository.GetAsync(s => s.Project.Id == project.Id && s.Member.Id == member.Id) != null)
                    throw new NotFoundException("Member is existed!");
                project.AddMember(member);

                await _unitOfWork.SaveChangesAsync();

                return await GetProject(projectId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ProjectDetailResponse> CreateListTask(int projectId, AddListTaskRequest listTaskRequest)
        {
            try
            {
                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(projectId)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                var project = await _projectRepository.GetAsync(s => s.Id == projectId);
                if (project == null) throw new NotFoundException("Project is not found!");

                project.CreateListTask(listTaskRequest.Name);

                await _unitOfWork.SaveChangesAsync();

                return await GetProject(projectId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Put
        public async Task<ProjectDetailResponse> UpdateProject(int projectId, UpdateProjectRequest projectInput)
        {
            try
            {
                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(projectId)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                var project = await _projectRepository.GetAsync(s => s.Id == projectId);
                if (project == null) throw new NotFoundException("Project is not found!");
                
                project.Update(projectInput.Name, projectInput.Description);

                await _unitOfWork.SaveChangesAsync();

                return await GetProject(projectId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion

        #region Delete
        public async Task<bool> DeleteProject(int projectId)
        {
            try
            {
                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(projectId)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                var project = await _projectRepository.GetAsync(s => s.Id == projectId);
                if (project == null) throw new NotFoundException("Project is not found!");

                foreach (var listTask in project.ListTasks)
                {
                    await DeleteListTask(projectId, listTask.Id);
                }
                await _projectRepository.SoftDeleteAsync(project);

                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ProjectDetailResponse> DeleteListTask(int projectId, int listTaskId)
        {
            try
            {
                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(projectId)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                var listTask = await _listTaskRepository.GetAsync(s => s.Id == listTaskId);
                if (listTask == null) throw new NotFoundException("List Task is not found!");
                var project = await _projectRepository.GetAsync(s => s.Id == projectId);
                if (project == null) throw new NotFoundException("Project is not found!");

                project.DeleteListTask(listTask.Id);

                await _unitOfWork.SaveChangesAsync();

                return await GetProject(projectId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ProjectDetailResponse> RemoveMember(int memberId, int projectId)
        {
            try
            {
                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(projectId)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                var project = await _projectRepository.GetAsync(s => s.Id == projectId);
                if (project == null) throw new NotFoundException("Project is not found!");
                var member = await _userRepository.GetAsync(s => s.Id == memberId);
                if (member == null) throw new NotFoundException("Member is not found!");

                var projectMember = await _projectMemberRepository.GetAsync(s => s.Member.Id == memberId && s.Project.Id == projectId);
                if ( projectMember == null) throw new NotFoundException("User is not a member in this project!");

                await _projectMemberRepository.SoftDeleteAsync(projectMember);

                await _unitOfWork.SaveChangesAsync();

                return await GetProject(projectId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}

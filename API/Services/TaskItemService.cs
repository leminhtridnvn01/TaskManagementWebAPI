using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using API.Exceptions.Unautorizations;
using API.Extensions;
using AutoMapper;
using Domain._Histories;
using Domain.DTOs.Attachments.AddAttachment;
using Domain.DTOs.Attachments.GetAttachment;
using Domain.DTOs.ListTasks.GetListTask;
using Domain.DTOs.ListTodos.AddListTodo;
using Domain.DTOs.Tags.DeleteTag;
using Domain.DTOs.Tags.GetTag;
using Domain.DTOs.TaskItems.AddTaskItem;
using Domain.DTOs.TaskItems.GetTaskItem;
using Domain.DTOs.TaskItems.UpdateTaskItem;
using Domain.DTOs.TodoItems.AddTodoItem;
using Domain.Entities.Tasks;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Domain.ListTasks;
using Domain.Projects;
using Domain.Tasks;
using Domain.Users;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace API.Services
{
    public class TaskItemService : BaseService, ITaskItemService
    {
        private readonly ITaskItemRepository _taskkItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IListTaskRepository _listTaskRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IListTodoRepository _listTodoRepository;
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly ITagMappingRepository _tagMappingRepository;
        private readonly IProjectMemberRepository _projectMemberRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public TaskItemService(ITaskItemRepository taskItemRepository,
            IUserRepository userRepository,
            IListTaskRepository listTaskRepository, 
            ITagRepository tagRepository,
            IListTodoRepository listTodoRepository,
            ITodoItemRepository todoItemRepository,
            ITagMappingRepository tagMappingRepository,
            IProjectMemberRepository projectMemberRepository,
            IAssignmentRepository assignmentRepository,
            IAttachmentRepository attachmentRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(httpContextAccessor)
        {
            _taskkItemRepository = taskItemRepository;
            _listTaskRepository = listTaskRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
            _listTodoRepository = listTodoRepository;
            _todoItemRepository = todoItemRepository;
            _tagMappingRepository = tagMappingRepository;
            _projectMemberRepository = projectMemberRepository;
            _assignmentRepository = assignmentRepository;
            _attachmentRepository = attachmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        #region Get
        public async Task<TaskItemDetailResponse> GetTaskItem( int taskItemId)
        {
            try
            {
                var taskItem = await _taskkItemRepository.GetAsync(s => s.Id == taskItemId);
                if (taskItem == null) throw new NotFoundException("Task is not found!");

                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(taskItem.ListTask.Project.Id)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                foreach (var listtodo in taskItem.ListTodoes)
                {
                    foreach (var todoItem in listtodo.TodoItems)
                    {
                        if (todoItem.TodoParrent != null) listtodo.TodoItems.Remove(todoItem);
                    }
                }
                
                var taskItemMapper = _mapper.Map<TaskItemDetailResponse>(taskItem);
                var assignees = await _assignmentRepository.GetAllAssignees(taskItemId);
                taskItemMapper.ListAssignee = _mapper.Map<List<UserResponse>>(assignees);
                var tag = await _tagMappingRepository.GetAllTags(taskItemId);
                taskItemMapper.ListTag = _mapper.Map<List<TagResponse>>(tag);

                return taskItemMapper;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Post
        public async  Task<ListTaskDetailResponse> CreateTaskItem( int listTaskId, AddTaskItemRequest taskItemInput)
        {
            try
            {
                var listTask = await _listTaskRepository.GetAsync(s => s.Id == listTaskId);
                if (listTask == null) throw new NotFoundException("List Task is not found!");

                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(listTask.Project.Id)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                var newTaskItem = new TaskItem(taskItemInput.Name, taskItemInput.Deadline, taskItemInput.Prioritized, taskItemInput.Description);
                newTaskItem.AddListTask(listTask);
                await _taskkItemRepository.AddAsync(newTaskItem);

                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<ListTaskDetailResponse>(listTask);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<TaskItemDetailResponse> CreateAttachment( int taskItemId, AddAttachmentRequest attachmentInput)
        {
            try
            {
                var taskItem = await _taskkItemRepository.GetAsync(s => s.Id == taskItemId);
                if (taskItem == null) throw new NotFoundException("Task is not found!");

                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(taskItem.ListTask.Project.Id)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                taskItem.CreateAttachment(attachmentInput.Name, attachmentInput.FileType, attachmentInput.URL);

                await _unitOfWork.SaveChangesAsync();

                return await GetTaskItem(taskItemId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<TaskItemDetailResponse> CreateListTodo( int taskItemId, AddListTodoRequest listTodoInput)
        {
            try
            {
                var taskItem = await _taskkItemRepository.GetAsync(s => s.Id == taskItemId);
                if (taskItem == null) throw new NotFoundException("Task is not found!");

                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(taskItem.ListTask.Project.Id)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                taskItem.CreateListTodo(listTodoInput.Name);

                await _unitOfWork.SaveChangesAsync();

                return await GetTaskItem(taskItemId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<TaskItemDetailResponse> CreateTodoItem(int listTodoId, int todoItemParrentId, AddTodoItemRequest todoItemInput)
        {
            try
            {
                var listTodo = await _listTodoRepository.GetAsync(s => s.Id == listTodoId);
                var todoItemParrent = await _todoItemRepository.GetAsync(s => s.Id == todoItemParrentId);
                if (listTodo == null) throw new NotFoundException("List Todo is not found");

                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(listTodo.TaskItem.ListTask.Project.Id)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                if (todoItemParrent == null)
                    listTodo.TaskItem.CreateTodoItem(listTodoId, todoItemInput.Name);
                else
                    listTodo.TaskItem.CreateSubTodoItem(listTodoId, todoItemParrentId, todoItemInput.Name);

                await _unitOfWork.SaveChangesAsync();

                return await GetTaskItem(listTodo.TaskItem.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<TaskItemDetailResponse> AddAssignee( int taskItemId, string assigneeUsername)
        {
            try
            {
                var taskItem = await _taskkItemRepository.GetAsync(s => s.Id == taskItemId);
                var newAssignee = await _userRepository.GetAsync(s => s.UserName == assigneeUsername);
                if (taskItem == null) throw new NotFoundException("Task is not found!");
                if (newAssignee == null) throw new NotFoundException("User is not found!");

                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(taskItem.ListTask.Project.Id)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                if ((await _assignmentRepository.GetAllAssignees(taskItem.Id)).Contains(newAssignee)) 
                    throw new NotFoundException("Member is existed in this task!");
                taskItem.AddAssignment(newAssignee);

                await _unitOfWork.SaveChangesAsync();

                return await GetTaskItem(taskItem.Id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<TaskItemDetailResponse> AddTag( int taskItemId, int tagId)
        {
            try
            {
                var taskItem = await _taskkItemRepository.GetAsync(s => s.Id == taskItemId);
                var tag = await _tagRepository.GetAsync(s => s.Id == tagId);
                if (taskItem == null) throw new NotFoundException("Task is not found!");
                if (tag == null) throw new NotFoundException("Tag is not found!");

                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(taskItem.ListTask.Project.Id)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                taskItem.AddTag(tag);

                await _unitOfWork.SaveChangesAsync();

                return await GetTaskItem(taskItem.Id);
            }
            catch (Exception e)
            {

                throw new Exception(e + "");
            }
        }

        #endregion

        #region Put
        public async Task<TaskItemDetailResponse> UpdateTaskItem(int taskItemId,  UpdateTaskItemsRequest taskItemInput)
        {
            try
            {
                var taskItem = await _taskkItemRepository.GetAsync(s => s.Id == taskItemId);
                if (taskItem == null) throw new NotFoundException("Task is not found!");

                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(taskItem.ListTask.Project.Id)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                taskItem.Update(taskItemInput.Name,
                    taskItemInput.Prioritized,
                    taskItemInput.Description);

                await _unitOfWork.SaveChangesAsync();

                return await GetTaskItem(taskItem.Id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        #endregion

        #region Patch
        public async Task<TaskItemDetailResponse> UpdateDeadlineInTaskItem( int taskId, DateTime newDeadline)
        {
            try
            {
                var taskItem = await _taskkItemRepository.GetAsync(s => s.Id == taskId);
                if ( taskItem == null) throw new NotFoundException("Task is not found!");

                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(taskItem.ListTask.Project.Id)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                taskItem.ChangeDeadline(newDeadline);

                await _unitOfWork.SaveChangesAsync();

                return await GetTaskItem(taskItem.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<TaskItemDetailResponse> UpdateAssigneeInProgressInTaskItem( int taskId, string assigneeUsername)
        {
            try
            {
                var taskItem = await _taskkItemRepository.GetAsync(s => s.Id == taskId);
                var assignee = await _userRepository.GetAsync(s => s.UserName == assigneeUsername);
                if (taskItem == null ) throw new NotFoundException("Task is not found!");
                if (assignee == null) throw new NotFoundException("User is not found!");

                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(taskItem.ListTask.Project.Id)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                taskItem.ChangeAssigneeInProgress(assignee);

                await _unitOfWork.SaveChangesAsync();

                return await GetTaskItem(taskItem.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteTodoItem( int todoItemId)
        {
            try
            {
                var todoItem = await _todoItemRepository.GetAsync(s => s.Id == todoItemId);
                if (todoItem == null) throw new NotFoundException("Todo Item is not found!");

                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(todoItem.ListTodo.TaskItem.ListTask.Project.Id)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                foreach (var subTodoItem in todoItem.SubTodoItems)
                {
                    await _todoItemRepository.SoftDeleteAsync(subTodoItem);
                }
                await _todoItemRepository.SoftDeleteAsync(todoItem);    

                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteListTodo( int listTodoId)
        {
            try
            {
                var listTodo = await _listTodoRepository.GetAsync(s => s.Id == listTodoId);
                if (listTodo == null) throw new NotFoundException("List Todo is not found!");

                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(listTodo.TaskItem.ListTask.Project.Id)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                foreach (var todoItem in listTodo.TodoItems)
                {
                    await DeleteTodoItem(todoItem.Id);
                }
                await _listTodoRepository.SoftDeleteAsync(listTodo);    

                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteAttachment( int attachmentId)
        {
            try
            {
                var attachment = await _attachmentRepository.GetAsync(s => s.Id == attachmentId);

                if (attachment == null) throw new NotFoundException("Attachment is not found!");

                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(attachment.TaskItem.ListTask.Project.Id)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                await _attachmentRepository.SoftDeleteAsync(attachment);    

                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteTag( DeleteTagRequest tagMappingInput)
        {
            try
            {
                var taskItem = await _taskkItemRepository.GetAsync(s => s.Id == tagMappingInput.TaskId);
                var tagMapping = await _tagMappingRepository.GetAsync(s => s.Tag.Id == tagMappingInput.TagId && s.Task.Id == tagMappingInput.TaskId);
                if ( taskItem == null) throw new NotFoundException("Task is not found!");
                if (tagMapping == null) throw new NotFoundException("Tag is not existed in this task!");

                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(taskItem.ListTask.Project.Id)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                await _tagMappingRepository.SoftDeleteAsync(tagMapping);    

                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<bool> DeleteTaskItem(int taskId)
        {
            try
            {
                var taskItem = await _taskkItemRepository.GetAsync(s => s.Id == taskId);
                if (taskItem == null) throw new NotFoundException("Task is not found!");

                var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var user = await _userRepository.GetAsync(s => s.Id == userId);
                if (!(await _projectMemberRepository.GetAllMember(taskItem.ListTask.Project.Id)).Contains(user))
                    throw new NotFoundException("You are not a member in this project!");

                foreach (var attachment in taskItem.Attachments) 
                {
                    if (!(await DeleteAttachment(attachment.Id))) return false;
                }
                foreach (var tag in taskItem.Tags)
                {
                    if (!(await DeleteTag(new DeleteTagRequest{ TagId = tag.Tag.Id, TaskId = tag.Task.Id}))) return false;
                }
                foreach (var listTodo in taskItem.ListTodoes)
                {
                    if (!(await DeleteListTodo(listTodo.Id))) return false;
                }
                await _taskkItemRepository.SoftDeleteAsync(taskItem);
                
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
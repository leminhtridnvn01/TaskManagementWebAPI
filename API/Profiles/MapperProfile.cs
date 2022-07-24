using System.Linq;
using AutoMapper;
using Domain.DTOs.Attachments.AddAttachment;
using Domain.DTOs.Attachments.GetAttachment;
using Domain.DTOs.ListTasks.AddListTask;
using Domain.DTOs.ListTasks.GetListTask;
using Domain.DTOs.ListTodos.AddListTodo;
using Domain.DTOs.ListTodos.GetListTodo;
using Domain.DTOs.Projects.AddProject;
using Domain.DTOs.Projects.GetProject;
using Domain.DTOs.Tags.GetTag;
using Domain.DTOs.TaskItems.AddTaskItem;
using Domain.DTOs.TaskItems.GetTaskItem;
using Domain.DTOs.TaskItems.UpdateTaskItem;
using Domain.DTOs.TodoItems.AddTodoItem;
using Domain.DTOs.TodoItems.GetTodoItem;
using Domain.DTOs.Users;
using Domain.DTOs.Users.UpdateUser;
using Domain.Entities.Tasks;
using Domain.ListTasks;
using Domain.Projects;
using Domain.Users;

namespace TaskManagement.ApplicationTier.API.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<AddProjectRequest, Project>();

            CreateMap<AddListTaskRequest, ListTask>();
            
            CreateMap<ListTask, ListTaskResponse>();
            CreateMap<ListTask, ListTaskDetailResponse >();
            CreateMap<AddListTaskRequest, ListTask>();

            CreateMap<User, UserDetailResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<User, UpdateUserResponse>();

            CreateMap<Project, ProjectResponse>();
            CreateMap<Project, ProjectDetailResponse>();
            CreateMap<ProjectDetailResponse, Project>();
            
            CreateMap<AddTaskItemRequest, TaskItem>();
            CreateMap<TaskItem, TaskItemResponse>()
                .ForMember(
                    dest => dest.AssigneeInProgress, 
                    opt => opt.MapFrom(src => src.AssigneeInProgress.UserName)
                );
            CreateMap<TaskItem, TaskItemDetailResponse>();
            CreateMap<UpdateTaskItemsRequest, TaskItem>();

            CreateMap<Attachment, AttachmentResponse>();
            CreateMap<AddAttachmentRequest, Attachment>();
            CreateMap<Attachment, AddAttachmentResponse>()
                .ForMember(
                    dest => dest.TaskItemId,
                    opt => opt.MapFrom(src => src.TaskItem.Id)
                );

            CreateMap<ListTodo, ListTodoResponse>()
                .AfterMap(
                    (src, dest) => 
                    {
                        if (dest.IsDone == true) dest.Done = 100;
                        else dest.Done = src.CalculateDone();
                        
                    }
                );;
            CreateMap<AddListTodoRequest, ListTodo>();

            CreateMap<TodoItem, TodoItemResponse>()
                .AfterMap(
                    (src, dest) => 
                    {
                        if (dest.IsDone == true) dest.Done = 100;
                        else dest.Done = src.CalculateDone();
                    }
                );
            CreateMap<AddTodoItemRequest, TodoItem>();

            CreateMap<Tag, TagResponse>();

            
            
        }
    }
}
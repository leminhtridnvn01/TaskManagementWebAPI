using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.DTOs.Attachments.GetAttachment;
using Domain.DTOs.ListTodos.AddListTodo;
using Domain.DTOs.Tags.AddTag;
using Domain.DTOs.Tags.DeleteTag;
using Domain.DTOs.TaskItems.GetTaskItem;
using Domain.DTOs.TaskItems.UpdateTaskItem;
using Domain.DTOs.TaskItems.AddTaskItem;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.ApplicationTier.API.Controllers;
using Domain.DTOs.TodoItems.AddTodoItem;

namespace API.Controllers
{
    public class TaskItemController : BaseApiController
    {
        private readonly ITaskItemService _taskItemService;
        public TaskItemController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        #region Get
        [Authorize]
        [HttpGet]
        [Route("{taskItemId}")]
        public async Task<ActionResult<TaskItemDetailResponse>> GetTaskItem([FromRoute] int taskItemId)
        {
            var taskItem = await _taskItemService.GetTaskItem(taskItemId);
            return Ok(taskItem);
        }
        #endregion

        #region Post
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TaskItemDetailResponse>> AddTaskItem([FromHeader] int listTaskId, [FromBody] AddTaskItemRequest taskInput)
        {
            var newListTask = await _taskItemService.CreateTaskItem(listTaskId, taskInput);
            return Ok(newListTask);
        }

        [Authorize]
        [HttpPost]
        [Route("ListTodos")]
        public async Task<ActionResult<TaskItemDetailResponse>> AddListTodo([FromHeader] int taskItemId, [FromBody] AddListTodoRequest listTodoInput)
        {
            var newTaskItem = await _taskItemService.CreateListTodo(taskItemId, listTodoInput);
            return Ok(newTaskItem);
        }

        [Authorize]
        [HttpPost]
        [Route("TodoItems")]
        public async Task<ActionResult<TaskItemDetailResponse>> AddTodoItem([FromHeader] int listTodoId, [FromHeader] int todoItemParrentId, [FromBody] AddTodoItemRequest todoItemInput)
        {
            var newTaskItem = await _taskItemService.CreateTodoItem(listTodoId, todoItemParrentId, todoItemInput);
            return Ok(newTaskItem);
        }

        [Authorize]
        [HttpPost]
        [Route("Attachments")]
        public async Task<ActionResult<TaskItemDetailResponse>> AddAttachment([FromHeader] int taskItemId, [FromBody] AddAttachmentRequest attachmentInput)
        {
            var newTaskItem = await _taskItemService.CreateAttachment(taskItemId, attachmentInput);
            return Ok(newTaskItem);
        }

        [Authorize]
        [HttpPost]
        [Route("Assignees")]
        public async Task<ActionResult<TaskItemDetailResponse>> AddAssignee([FromHeader] int taskItemId, [FromBody] string assigneeUusername)
        {
            var newTaskItem = await _taskItemService.AddAssignee(taskItemId, assigneeUusername);
            return Ok(newTaskItem);
        }

        [Authorize]
        [HttpPost]
        [Route("Tags")]
        public async Task<ActionResult<TaskItemDetailResponse>> AddTag([FromHeader] int taskItemId, [FromBody] int tagId)
        {
            var newTaskItem = await _taskItemService.AddTag(taskItemId, tagId);
            return Ok(newTaskItem);
        }
        #endregion

        #region Put
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<TaskItemDetailResponse>> UpdateTask([FromHeader] int taskItemId, [FromBody] UpdateTaskItemsRequest taskItemInput)
        {
            var newTaskItem = await _taskItemService.UpdateTaskItem(taskItemId, taskItemInput);
            return Ok(newTaskItem);
        }
        #endregion

        #region Patch
        [Authorize]
        [HttpPatch]
        [Route("Deadlines")]
        public async Task<ActionResult<TaskItemDetailResponse>> UpdateDeadline([FromHeader] int taskId, [FromBody] DateTime newDeadline)
        {
            var newTaskItem = await _taskItemService.UpdateDeadlineInTaskItem(taskId, newDeadline);
            return Ok(newTaskItem);
        }

        [Authorize]
        [HttpPatch]
        [Route("AssigneeInProgresses")]
        public async Task<ActionResult<TaskItemDetailResponse>> UpdateAssigneeInProgress([FromHeader] int taskId, [FromBody] string assigneeUsername)
        {
            var newTaskItem = await _taskItemService.UpdateAssigneeInProgressInTaskItem(taskId, assigneeUsername);
            return Ok(newTaskItem);
        }
        #endregion

        #region Delete
        [Authorize]
        [HttpDelete]
        [Route("TodoItems")]
        public async Task<ActionResult<bool>> DeleteTodoItem([FromBody] int todoItemId)
        {
            
            if(!(await _taskItemService.DeleteTodoItem(todoItemId)))
            {
                return BadRequest("Can not delete this Todo Item!");
            }
            return Ok("Delete this Todo Item successfully!");
        }

        [Authorize]
        [HttpDelete]
        [Route("ListTodos")]
        public async Task<ActionResult<bool>> DeleteListTodo([FromBody] int listTodoId)
        {
            
            if(!(await _taskItemService.DeleteListTodo(listTodoId)))
            {
                return BadRequest("Can not delete this List Todo!");
            }
            return Ok("Delete this List Todo successfully!");
        }

        [Authorize]
        [HttpDelete]
        [Route("Attachments")]
        public async Task<ActionResult<bool>> DeleteAttachment([FromBody] int attachmentId)
        {
            
            if(!(await _taskItemService.DeleteAttachment(attachmentId)))
            {
                return BadRequest("Can not delete this Attachment!");
            }
            return Ok("Delete this Attachment successfully!");
        }

        [Authorize]
        [HttpDelete]
        [Route("Tags")]
        public async Task<ActionResult<bool>> DeleteTag([FromBody] DeleteTagRequest tagMappingInput)
        {
            
            if(!(await _taskItemService.DeleteTag(tagMappingInput)))
            {
                return BadRequest("Can not delete this Tag!");
            }
            return Ok("Delete this Tag successfully!");
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteTaskItem([FromBody] int taskId)
        {
            
            if(!(await _taskItemService.DeleteTaskItem(taskId)))
            {
                return BadRequest("Can not delete this Tag!");
            }
            return Ok("Delete this Tag successfully!");
        }
        #endregion
    }
}
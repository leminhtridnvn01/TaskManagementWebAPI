using Domain.DTOs.Projects.AddProject;
using Domain.Projects;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.ApplicationTier.API.Controllers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Domain.DTOs.Projects.GetProject;
using Domain.DTOs.ListTasks.AddListTask;
using Domain.DTOs.Projects.UpdateProject;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    public class ProjectController : BaseApiController
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        #region Get
        [Authorize]
        [HttpGet]
        [Route("{projectId}")]
        public async Task<ActionResult<string>> GetProject([FromRoute] int projectId)
        {
            var project = await _projectService.GetProject(projectId);
            return Ok(project);
        }
        #endregion

        #region Post
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> Create( AddProjectRequest projectRequest)
        {
            var newProject = await _projectService.CreateProject(projectRequest);
            return Ok(newProject);
        }

        [Authorize]
        [HttpPost]
        [Route("Members")]
        public async Task<ActionResult<string>> AddMember([FromBody] int memberId, [FromHeader] int projectId)
        {
            var result = await _projectService.AddMember(memberId, projectId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("ListTasks")]
        public async Task<ActionResult<string>> CreateListTask([FromHeader] int projectId ,[FromBody] AddListTaskRequest listTaskRequest)
        {
            var newProject = await _projectService.CreateListTask(projectId, listTaskRequest);
            return Ok(newProject);
        }
        #endregion

        #region Put
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<string>> UpdateProject([FromHeader] int projectId, [FromBody] UpdateProjectRequest projectInput)
        {
            var newProject = await _projectService.UpdateProject(projectId, projectInput);
            return Ok(newProject);
        }
        #endregion

        #region Delete
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<string>> DeleteProject([FromBody] int projectId)
        {
            if(!await _projectService.DeleteProject(projectId))
            {
                return BadRequest("Can not delete this project!");
            }
            return Ok("Delete this project successfully!");
        }
        [Authorize]
        [HttpDelete]
        [Route("ListTasks")]
        public async Task<ActionResult<string>> DeleteListTask([FromHeader] int projectId, [FromBody] int listTaskId)
        {
            var newProject = await _projectService.DeleteListTask(projectId, listTaskId);
            return Ok(newProject);
        }
        [Authorize]
        [HttpDelete]
        [Route("Members")]
        public async Task<ActionResult<string>> RemoveMember([FromBody] int memberId, [FromHeader] int projectId)
        {
            var newProject = await _projectService.RemoveMember(memberId, projectId);
            return Ok(newProject);
        }
        #endregion
    }
}

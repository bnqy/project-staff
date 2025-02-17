using Microsoft.AspNetCore.Mvc;
using project_staff.Entities.Models;
using project_staff.Service.Contracts;
using project_staff.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Presentation.Controllers
{
	[ApiController]
	[Route("/api/projects/{projectId}/tasks")]
	public class TasksController : ControllerBase
	{
		private readonly IServiceManager serviceManager;

		public TasksController(IServiceManager serviceManager)
		{
			this.serviceManager = serviceManager;
		}

		[HttpGet]
		public IActionResult GetTasksForProject(Guid projectId)
		{
			var tasks = this.serviceManager.ProjectTaskService.GetTasks(projectId, false);

			return Ok(tasks);
		}

		[HttpGet("{id:guid}", Name = "GetTaskForProject")]
		public IActionResult GetTaskForProject(Guid projectId, Guid id)
		{
			var task = this.serviceManager.ProjectTaskService.GetTask(projectId, id, false);

			return Ok(task);
		}

		[HttpPost]
		public IActionResult CreateTaskForProject(Guid projectId, [FromBody] ProjectTaskForCreationDto projectTaskForCreationDto)
		{
			if (projectTaskForCreationDto is null)
			{
				return BadRequest("ProjectTaskForCreationDto is null.");
			}

			var projectTaskDto = this.serviceManager.ProjectTaskService.CreateTaskForProject(projectId, projectTaskForCreationDto, false);

			return CreatedAtRoute("GetTaskForProject",  new { projectId, projectTaskDto.Id }, projectTaskDto);
		}

		[HttpDelete("{id:guid}")]
		public IActionResult DeleteTaskForProject(Guid projectId, Guid id)
		{
			this.serviceManager.ProjectTaskService.DeleteTaskForProject(projectId, id, false);

			return NoContent();
		}

		[HttpPut("{id:guid}")]
		public IActionResult UpdateTaskForProject(Guid projectId, Guid id, [FromBody] ProjectTaskForUpdateDto projectTaskForUpdateDto)
		{
			if (projectTaskForUpdateDto is null)
			{
				return BadRequest("ProjectTaskForUpdateDto is null.");
			}
			serviceManager.ProjectTaskService.UpdateTaskForProject(projectId, id, projectTaskForUpdateDto, false, true);

			return NoContent();
		}
	}
}

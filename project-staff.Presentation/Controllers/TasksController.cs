using Microsoft.AspNetCore.Mvc;
using project_staff.Entities.Models;
using project_staff.Service.Contracts;
using project_staff.Shared.DTOs;
using project_staff.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
		public async Task<IActionResult> GetTasksForProject(Guid projectId,
			[FromQuery] TaskParameters taskParameters)
		{
			var pagedTasks = await this.serviceManager.ProjectTaskService.GetTasksAsync(projectId, taskParameters, false);

			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedTasks.metaData));

			return Ok(pagedTasks.projectTaskDtos);
		}

		[HttpGet("{id:guid}", Name = "GetTaskForProject")]
		public async Task<IActionResult> GetTaskForProject(Guid projectId, Guid id)
		{
			var task = await this.serviceManager.ProjectTaskService.GetTaskAsync(projectId, id, false);

			return Ok(task);
		}

		[HttpPost]
		public async Task<IActionResult> CreateTaskForProject(Guid projectId, [FromBody] ProjectTaskForCreationDto projectTaskForCreationDto)
		{
			if (projectTaskForCreationDto is null)
			{
				return BadRequest("ProjectTaskForCreationDto is null.");
			}

			if (!ModelState.IsValid)
			{
				return UnprocessableEntity(ModelState);
			}

			var projectTaskDto = await this.serviceManager.ProjectTaskService.CreateTaskForProjectAsync(projectId, projectTaskForCreationDto, false);

			return CreatedAtRoute("GetTaskForProject",  new { projectId, projectTaskDto.Id }, projectTaskDto);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteTaskForProject(Guid projectId, Guid id)
		{
			await this.serviceManager.ProjectTaskService.DeleteTaskForProjectAsync(projectId, id, false);

			return NoContent();
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> UpdateTaskForProject(Guid projectId, Guid id, [FromBody] ProjectTaskForUpdateDto projectTaskForUpdateDto)
		{
			if (projectTaskForUpdateDto is null)
			{
				return BadRequest("ProjectTaskForUpdateDto is null.");
			}

			if (!ModelState.IsValid)
			{
				return UnprocessableEntity(ModelState);
			}

			await serviceManager.ProjectTaskService.UpdateTaskForProjectAsync(projectId, id, projectTaskForUpdateDto, false, true);

			return NoContent();
		}
	}
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_staff.Entities.Models;
using project_staff.Service.Contracts;
using project_staff.Shared.DTOs;
using project_staff.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace project_staff.Presentation.Controllers
{
	[ApiController]
	[Route("/api/projects/{projectId}/tasks")]
	[Authorize]
	//[ResponseCache(CacheProfileName = "120SecondsDuration")]
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
			string? userIdString = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


			if (string.IsNullOrEmpty(userIdString))
			{
				return Unauthorized();
			}

			if (!Guid.TryParse(userIdString, out Guid userId))
			{
				return BadRequest("Invalid user identifier.");
			}

			var pagedTasks = await this.serviceManager.ProjectTaskService.GetTasksAsync(userId, projectId, taskParameters, false);

			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedTasks.metaData));

			return Ok(pagedTasks.projectTaskDtos);
		}

		[HttpGet("{id:guid}", Name = "GetTaskForProject")]
		public async Task<IActionResult> GetTaskForProject(Guid projectId, Guid id)
		{
			string? userIdString = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


			if (string.IsNullOrEmpty(userIdString))
			{
				return Unauthorized();
			}

			if (!Guid.TryParse(userIdString, out Guid userId))
			{
				return BadRequest("Invalid user identifier.");
			}

			var task = await this.serviceManager.ProjectTaskService.GetTaskAsync(userId, projectId, id, false);

			return Ok(task);
		}

		[HttpPost]
		public async Task<IActionResult> CreateTaskForProject(Guid projectId, [FromBody] ProjectTaskForCreationDto projectTaskForCreationDto)
		{
			foreach (var claim in User.Claims)
			{
				Console.WriteLine($"{claim.Type}: {claim.Value}");
			}


			if (projectTaskForCreationDto is null)
			{
				return BadRequest("ProjectTaskForCreationDto is null.");
			}

			if (!ModelState.IsValid)
			{
				return UnprocessableEntity(ModelState);
			}

			string? userIdString = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


			if (string.IsNullOrEmpty(userIdString))
			{
				return Unauthorized();
			}

			if (!Guid.TryParse(userIdString, out Guid userId))
			{
				return BadRequest("Invalid user identifier.");
			}

			var projectTaskDto = await this.serviceManager.ProjectTaskService.CreateTaskForProjectAsync(userId, projectId, projectTaskForCreationDto, false);

			return CreatedAtRoute("GetTaskForProject",  new { projectId, projectTaskDto.Id }, projectTaskDto);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteTaskForProject(Guid projectId, Guid id)
		{
			string? userIdString = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


			if (string.IsNullOrEmpty(userIdString))
			{
				return Unauthorized();
			}

			if (!Guid.TryParse(userIdString, out Guid userId))
			{
				return BadRequest("Invalid user identifier.");
			}

			await this.serviceManager.ProjectTaskService.DeleteTaskForProjectAsync(userId, projectId, id, false);

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

			string? userIdString = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


			if (string.IsNullOrEmpty(userIdString))
			{
				return Unauthorized();
			}

			if (!Guid.TryParse(userIdString, out Guid userId))
			{
				return BadRequest("Invalid user identifier.");
			}

			await serviceManager.ProjectTaskService.UpdateTaskForProjectAsync(userId, projectId, id, projectTaskForUpdateDto, false, true);

			return NoContent();
		}
	}
}

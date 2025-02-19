using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
	[Route("api/projects")]
	[Authorize]
	//[ResponseCache(CacheProfileName = "120SecondsDuration")]
	public class ProjectsController : ControllerBase
	{
		private readonly IServiceManager serviceManager;

		public ProjectsController(IServiceManager serviceManager)
		{
			this.serviceManager = serviceManager;
		}

		[HttpGet]
		[Authorize(Roles = "руководитель, менеджер проекта, сотрудник")]
		public async Task<IActionResult> GetProjects([FromQuery] ProjectParameters projectParameters)
		{
			/*foreach (var claim in User.Claims)
			{
				Console.WriteLine($"{claim.Type}: {claim.Value}");
			}*/

			string? userIdString = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


			if (string.IsNullOrEmpty(userIdString))
			{
				return Unauthorized();
			}

			if (!Guid.TryParse(userIdString, out Guid userId))
			{
				return BadRequest("Invalid user identifier.");
			}

			var pagedProjects = await this.serviceManager.ProjectService.GetAllProjectsAsync(userId, projectParameters,false);

			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedProjects.metaData));

			return Ok(pagedProjects.projectDtos);
		}


		[HttpGet("{id:guid}", Name = "ProjectById")]
		public async Task<IActionResult> GetProject(Guid id)
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

			var project = await this.serviceManager.ProjectService.GetProjectAsync(id, userId, false);

			return Ok(project);
		}

		[HttpPost]
		public async Task<IActionResult> CreateProject([FromBody] ProjectForCreationDto projectForCreationDto)
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

			if (projectForCreationDto is null)
			{
				return BadRequest("ProjectForCreationDto is null.");
			}

			if (!ModelState.IsValid)
			{
				return UnprocessableEntity(ModelState);
			}

			var CreatedProjectDto = await this.serviceManager.ProjectService.CreateProjectAsync(userId, projectForCreationDto);

			return CreatedAtRoute("ProjectById", new { id = CreatedProjectDto.Id}, CreatedProjectDto);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteProject(Guid id)
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

			await this.serviceManager.ProjectService.DeleteProjectAsync(id, userId, false);

			return NoContent();
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectForUpdateDto projectForUpdateDto)
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

			if (projectForUpdateDto is null)
			{
				return BadRequest("ProjectForUpdateDto is null.");
			}

			if (!ModelState.IsValid)
			{
				return UnprocessableEntity(ModelState);
			}

			await this.serviceManager.ProjectService.UpdateProjectAsync(id, userId, projectForUpdateDto, true);

			return NoContent();
		}

		[HttpPost("{projectId}/employees")]
		[Authorize(Roles = "руководитель, менеджер проекта")]
		public async Task<IActionResult> AddEmployeeToProject(Guid projectId, [FromBody] Guid employeeId)
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

			var result = await serviceManager.ProjectService.AddEmployeeToProjectAsync(userId, projectId, employeeId, trackChanges: true);

			if (!result.IsSuccess)
			{
				return NotFound(result.ErrorMessage);
			}

			return Ok();
		}

		[HttpDelete("{projectId}/employees/{employeeId}")]
		[Authorize(Roles = "руководитель, менеджер проекта")]
		public async Task<IActionResult> RemoveEmployeeFromProject(Guid projectId, Guid employeeId)
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

			var result = await serviceManager.ProjectService.RemoveEmployeeFromProjectAsync(userId, projectId, employeeId, trackChanges: true);

			if (!result.IsSuccess)
			{
				return NotFound(result.ErrorMessage);
			}

			return Ok();
		}
	}
}

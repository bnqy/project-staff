using Microsoft.AspNetCore.Mvc;
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
	[Route("api/projects")]
	public class ProjectsController : ControllerBase
	{
		private readonly IServiceManager serviceManager;

		public ProjectsController(IServiceManager serviceManager)
		{
			this.serviceManager = serviceManager;
		}

		[HttpGet]
		public async Task<IActionResult> GetProjects()
		{
			var projects = await this.serviceManager.ProjectService.GetAllProjectsAsync(false);

			return Ok(projects);
		}

		[HttpGet("{id:guid}", Name = "ProjectById")]
		public async Task<IActionResult> GetProject(Guid id)
		{
			var project = await this.serviceManager.ProjectService.GetProjectAsync(id, false);

			return Ok(project);
		}

		[HttpPost]
		public async Task<IActionResult> CreateProject([FromBody] ProjectForCreationDto projectForCreationDto)
		{
			if (projectForCreationDto is null)
			{
				return BadRequest("ProjectForCreationDto is null.");
			}

			if (!ModelState.IsValid)
			{
				return UnprocessableEntity(ModelState);
			}

			var CreatedProjectDto = await this.serviceManager.ProjectService.CreateProjectAsync(projectForCreationDto);

			return CreatedAtRoute("ProjectById", new { id = CreatedProjectDto.Id}, CreatedProjectDto);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteProject(Guid id)
		{
			await this.serviceManager.ProjectService.DeleteProjectAsync(id, false);

			return NoContent();
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectForUpdateDto projectForUpdateDto)
		{
			if (projectForUpdateDto is null)
			{
				return BadRequest("ProjectForUpdateDto is null.");
			}

			if (!ModelState.IsValid)
			{
				return UnprocessableEntity(ModelState);
			}

			await this.serviceManager.ProjectService.UpdateProjectAsync(id, projectForUpdateDto, true);

			return NoContent();
		}
	}
}

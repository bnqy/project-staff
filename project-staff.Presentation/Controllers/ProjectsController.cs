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
		public IActionResult GetProjects()
		{
			var projects = this.serviceManager.ProjectService.GetAllProjects(false);

			return Ok(projects);
		}

		[HttpGet("{id:guid}", Name = "ProjectById")]
		public IActionResult GetProject(Guid id)
		{
			var project = this.serviceManager.ProjectService.GetProject(id, false);

			return Ok(project);
		}

		[HttpPost]
		public IActionResult CreateProject([FromBody] ProjectForCreationDto projectForCreationDto)
		{
			if (projectForCreationDto is null)
			{
				return BadRequest("ProjectForCreationDto is null.");
			}

			var CreatedProjectDto = this.serviceManager.ProjectService.CreateProject(projectForCreationDto);

			return CreatedAtRoute("ProjectById", new { id = CreatedProjectDto.Id}, CreatedProjectDto);
		}
	}
}

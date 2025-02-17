using Microsoft.AspNetCore.Mvc;
using project_staff.Service.Contracts;
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
			try
			{
				var projects = this.serviceManager.ProjectService.GetAllProjects(false);

				return Ok(projects);
			}
			catch
			{
				return StatusCode(500, "Internal server error");
			}
		}
	}
}

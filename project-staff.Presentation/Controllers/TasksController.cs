using Microsoft.AspNetCore.Mvc;
using project_staff.Entities.Models;
using project_staff.Service.Contracts;
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

		[HttpGet("{id:guid}")]
		public IActionResult GetTaskForProject(Guid projectId, Guid id)
		{
			var task = this.serviceManager.ProjectTaskService.GetTask(projectId, id, false);

			return Ok(task);
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using project_staff.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ApplicationUsersController : ControllerBase
	{
		private readonly IServiceManager _service;

		public ApplicationUsersController(IServiceManager service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _service.ApplicationUserService.GetAllUsersAsync(trackChanges: false);
			return Ok(users);
		}
	}
}

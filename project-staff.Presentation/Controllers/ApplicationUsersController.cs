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

		[HttpGet("{id:guid}", Name = "GetUserById")]
		public async Task<IActionResult> GetUserById(Guid id)
		{
			var user = await _service.ApplicationUserService.GetUserByIdAsync(id, trackChanges: false);
			return Ok(user);
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] ApplicationUserForCreationDto userForCreation)
		{
			if (userForCreation == null)
				return BadRequest("UserForCreationDto object is null.");

			var createdUser = await _service.ApplicationUserService.CreateUserAsync(userForCreation);

			return CreatedAtRoute("GetUserById", new { id = createdUser.Id }, createdUser);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteUser(Guid id)
		{
			await _service.ApplicationUserService.DeleteUserAsync(id, trackChanges: false);
			return NoContent();
		}
	}
}

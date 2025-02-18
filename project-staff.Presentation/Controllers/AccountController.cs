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
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly IServiceManager _service;

		public AccountController(IServiceManager service) => _service = service;

		[HttpPost("register")]
		public async Task<IActionResult> RegisterUser([FromBody] ApplicationUserForRegistrationDto userForRegistration)
		{
			var result = await _service.AccountService.RegisterUser(userForRegistration);

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.TryAddModelError(error.Code, error.Description);
				}
				return BadRequest(ModelState);
			}
			return StatusCode(201);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Authenticate([FromBody] ApplicationUserForAuthenticationDto user)
		{
			if (!await _service.AccountService.ValidateUser(user))

				return Unauthorized();

			return Ok(new
			{
				Token = await _service
			.AccountService.CreateToken()
			});
		}
	}
	}

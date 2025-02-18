using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using project_staff.Contracts;
using project_staff.Entities.Models;
using project_staff.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Service
{
	public class AccountService : IAccountService
	{
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;


		public AccountService(ILoggerManager loggerManager, IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration)
		{
			_logger = loggerManager;
			_mapper = mapper;
			_userManager = userManager;
			_configuration = configuration;
		}


		public async Task<IdentityResult> RegisterUser(ApplicationUserForRegistrationDto userForRegistration)
		{
			var user = _mapper.Map<ApplicationUser>(userForRegistration);
			var result = await _userManager.CreateAsync(user, userForRegistration.Password);
			if (result.Succeeded)
				await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
			return result;
		}
	}
}

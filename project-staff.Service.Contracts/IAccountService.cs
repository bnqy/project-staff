using Microsoft.AspNetCore.Identity;
using project_staff.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Service.Contracts
{
	public interface IAccountService
	{
		Task<IdentityResult> RegisterUser(ApplicationUserForRegistrationDto userForRegistration);
		Task<bool> ValidateUser(ApplicationUserForAuthenticationDto userForAuth);
		Task<string> CreateToken();
	}
}

using Microsoft.AspNetCore.Identity;
using project_staff.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Contracts
{
	public interface IAccountService
	{
		Task<IdentityResult> RegisterUser(ApplicationUserForRegistrationDto userForRegistration);
	}
}

﻿using project_staff.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Service.Contracts
{
	public interface IApplicationUserService
	{
		Task<IEnumerable<ApplicationUserDto>> GetAllUsersAsync(bool trackChanges);
	}
}

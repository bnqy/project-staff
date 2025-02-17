﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Shared.DTOs
{
	public record ProjectDto(Guid Id,
		string Name,
		string CustomerCompany,
		string ExecutionCompany,
		DateTime StartDate,
		DateTime EndDate,
		int Priority);
}

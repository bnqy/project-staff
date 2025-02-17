﻿using project_staff.Entities.Models;
using project_staff.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Service.Contracts
{
	public interface IProjectService
	{
		IEnumerable<ProjectDto> GetAllProjects(bool trackChanges);
	}
}

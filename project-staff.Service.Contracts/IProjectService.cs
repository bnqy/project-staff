using project_staff.Entities.Models;
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
		ProjectDto GetProject(Guid projectId, bool trackChanges);
		ProjectDto CreateProject(ProjectForCreationDto projectForCreationDto);
		void DeleteProject(Guid projectId, bool trackChanges);
		void UpdateProject(Guid projectId, ProjectForUpdateDto projectForUpdateDto, bool trackChanges);
	}
}

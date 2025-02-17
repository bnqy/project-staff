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
		Task<IEnumerable<ProjectDto>> GetAllProjectsAsync(bool trackChanges);
		Task<ProjectDto> GetProjectAsync(Guid projectId, bool trackChanges);
		Task<ProjectDto> CreateProjectAsync(ProjectForCreationDto projectForCreationDto);
		Task DeleteProjectAsync(Guid projectId, bool trackChanges);
		Task UpdateProjectAsync(Guid projectId, ProjectForUpdateDto projectForUpdateDto, bool trackChanges);
	}
}

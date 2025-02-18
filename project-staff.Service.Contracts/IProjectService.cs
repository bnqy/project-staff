using project_staff.Entities.Models;
using project_staff.Shared.DTOs;
using project_staff.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Service.Contracts
{
	public interface IProjectService
	{
		Task<(IEnumerable<ProjectDto> projectDtos, MetaData metaData)> GetAllProjectsAsync(ProjectParameters projectParameters, bool trackChanges);
		Task<ProjectDto> GetProjectAsync(Guid projectId, bool trackChanges);
		Task<ProjectDto> CreateProjectAsync(ProjectForCreationDto projectForCreationDto);
		Task DeleteProjectAsync(Guid projectId, bool trackChanges);
		Task UpdateProjectAsync(Guid projectId, ProjectForUpdateDto projectForUpdateDto, bool trackChanges);
		Task<(bool IsSuccess, string ErrorMessage)> AddEmployeeToProjectAsync(Guid projectId, Guid employeeId, bool trackChanges);
		Task<(bool IsSuccess, string ErrorMessage)> RemoveEmployeeFromProjectAsync(Guid projectId, Guid employeeId, bool trackChanges);
	}
}

using project_staff.Entities.Models;
using project_staff.Shared.DTOs;
using project_staff.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Service.Contracts
{
	public interface IProjectService
	{
		Task<(IEnumerable<ProjectDto> projectDtos, MetaData metaData)> GetAllProjectsAsync(Guid userId, ProjectParameters projectParameters, bool trackChanges);
		Task<ProjectDto> GetProjectAsync(Guid projectId, Guid userId, bool trackChanges);
		Task<ProjectDto> CreateProjectAsync(Guid userId, ProjectForCreationDto projectForCreationDto);
		Task DeleteProjectAsync(Guid projectId, Guid userId, bool trackChanges);
		Task UpdateProjectAsync(Guid projectId, Guid userId, ProjectForUpdateDto projectForUpdateDto, bool trackChanges);
		Task<(bool IsSuccess, string ErrorMessage)> AddEmployeeToProjectAsync(Guid userId, Guid projectId, Guid employeeId, bool trackChanges);
		Task<(bool IsSuccess, string ErrorMessage)> RemoveEmployeeFromProjectAsync(Guid userId, Guid projectId, Guid employeeId, bool trackChanges);
	}
}

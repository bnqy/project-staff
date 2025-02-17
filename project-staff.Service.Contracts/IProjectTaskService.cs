using project_staff.Shared.DTOs;
using project_staff.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Service.Contracts
{
	public interface IProjectTaskService
	{
		Task<(IEnumerable<ProjectTaskDto> projectTaskDtos, MetaData metaData)> GetTasksAsync(Guid projectId, TaskParameters taskParameters, bool trackChanges);
		Task<ProjectTaskDto> GetTaskAsync(Guid projectId, Guid id, bool trackChanges);
		Task<ProjectTaskDto> CreateTaskForProjectAsync(Guid projectId, ProjectTaskForCreationDto projectTaskForCreationDto, bool trackChanges);
		Task DeleteTaskForProjectAsync(Guid projectId, Guid id, bool trackChanges);
		Task UpdateTaskForProjectAsync(Guid projectId, Guid id, ProjectTaskForUpdateDto projectTaskForUpdateDto, bool projectTrackChanges, bool taskTrackChanges);
	}
}

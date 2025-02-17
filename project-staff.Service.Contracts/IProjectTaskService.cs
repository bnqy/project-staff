using project_staff.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Service.Contracts
{
	public interface IProjectTaskService
	{
		IEnumerable<ProjectTaskDto> GetTasks(Guid projectId, bool trackChanges);
		ProjectTaskDto GetTask(Guid projectId, Guid id, bool trackChanges);
		ProjectTaskDto CreateTaskForProject(Guid projectId, ProjectTaskForCreationDto projectTaskForCreationDto, bool trackChanges);
		void DeleteTaskForProject(Guid projectId, Guid id, bool trackChanges);
	}
}

using project_staff.Entities.Models;
using project_staff.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Contracts
{
	public interface IProjectTaskRepository
	{
		Task<PagedList<ProjectTask>> GetTasksAsync(Guid projectId, TaskParameters taskParameters, bool trackChanges);
		Task<ProjectTask> GetTaskAsync(Guid projectId, Guid id, bool trackChanges);
		void CreateTaskForProject(Guid projectId, ProjectTask projectTask);
		void DeleteTask(ProjectTask projectTask);
	}
}

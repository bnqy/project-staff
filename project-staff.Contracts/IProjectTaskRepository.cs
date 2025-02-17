using project_staff.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Contracts
{
	public interface IProjectTaskRepository
	{
		Task<IEnumerable<ProjectTask>> GetTasksAsync(Guid projectId, bool trackChanges);
		Task<ProjectTask> GetTaskAsync(Guid projectId, Guid id, bool trackChanges);
		void CreateTaskForProject(Guid projectId, ProjectTask projectTask);
		void DeleteTask(ProjectTask projectTask);
	}
}

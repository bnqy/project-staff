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
		IEnumerable<ProjectTask> GetTasks(Guid projectId, bool trackChanges);
		ProjectTask GetTask(Guid projectId, Guid id, bool trackChanges);
	}
}

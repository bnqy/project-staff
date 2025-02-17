using project_staff.Contracts;
using project_staff.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Repository
{
	public class ProjectTaskRepository : RepositoryBase<ProjectTask>, IProjectTaskRepository
	{
		public ProjectTaskRepository(RepositoryContext repositoryContext) : base(repositoryContext)
		{
		}

		public IEnumerable<ProjectTask> GetTasks(Guid projectId, bool trackChanges)
		{
			return FindByCondition(t => t.ProjectId.Equals(projectId), trackChanges)
				.OrderBy(t => t.Name).ToList();
		}
	}
}

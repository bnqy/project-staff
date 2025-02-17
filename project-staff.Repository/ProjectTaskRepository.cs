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

		public void CreateTaskForProject(Guid projectId, ProjectTask projectTask)
		{
			projectTask.ProjectId = projectId;

			Create(projectTask);
		}

		public void DeleteTask(ProjectTask projectTask)
		{
			Delete(projectTask);
		}

		public ProjectTask GetTask(Guid projectId, Guid id, bool trackChanges)
		{
			return FindByCondition(e => e.ProjectId.Equals(projectId) && e.Id.Equals(id),trackChanges)
				.SingleOrDefault();
		}

		public IEnumerable<ProjectTask> GetTasks(Guid projectId, bool trackChanges)
		{
			return FindByCondition(t => t.ProjectId.Equals(projectId), trackChanges)
				.OrderBy(t => t.Name).ToList();
		}
	}
}

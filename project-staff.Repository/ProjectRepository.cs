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
	public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
	{
		public ProjectRepository(RepositoryContext repositoryContext) : base(repositoryContext)
		{
		}

		public void CreateProject(Project project)
		{
			Create(project);
		}

		public IEnumerable<Project> GetAllProjects(bool trackChanges)
		{
			return FindAll(trackChanges)
				.OrderBy(p => p.Name)
				.ToList();
		}

		public Project GetProject(Guid projectId, bool trackChanges)
		{
			return FindByCondition(p => p.Id.Equals(projectId), trackChanges)
				.SingleOrDefault();
		}
	}
}

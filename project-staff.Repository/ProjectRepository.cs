using Microsoft.EntityFrameworkCore;
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

		public void DeleteProject(Project project)
		{
			Delete(project);
		}

		public async Task<IEnumerable<Project>> GetAllProjectsAsync(bool trackChanges)
		{
			return await FindAll(trackChanges)
				.OrderBy(p => p.Name)
				.ToListAsync();
		}

		public async Task<Project> GetProjectAsync(Guid projectId, bool trackChanges)
		{
			return await FindByCondition(p => p.Id.Equals(projectId), trackChanges)
				.SingleOrDefaultAsync();
		}
	}
}

using Microsoft.EntityFrameworkCore;
using project_staff.Contracts;
using project_staff.Entities.Models;
using project_staff.Repository.Extensions;
using project_staff.Shared.RequestFeatures;
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

		public async Task<PagedList<Project>> GetAllProjectsAsync(
			Guid userId, ProjectParameters projectParameters, bool trackChanges)
		{
			var projects = await FindAll(trackChanges)
				.Where(p => p.ManagerId == userId)
				.FilterProjects(projectParameters)
				.Search(projectParameters.SearchTerm)
				//.OrderBy(p => p.Name)
				.Sort(projectParameters.OrderBy)
				.ToListAsync();

			return PagedList<Project>
				.ToPagedList(projects, projectParameters.PageNumber, projectParameters.PageSize);
		}

		public async Task<Project> GetProjectAsync(Guid projectId, Guid userId, bool trackChanges)
		{
			return await FindByCondition(p => (p.Id.Equals(projectId) && p.ManagerId == userId), trackChanges)
				.Include(p => p.Employees)
				.SingleOrDefaultAsync();
		}
	}
}

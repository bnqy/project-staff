using project_staff.Contracts;
using project_staff.Entities.Models;
using System;
using System.Collections.Generic;
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
	}
}

using project_staff.Contracts;
using project_staff.Entities.Models;
using System;
using System.Collections.Generic;
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
	}
}

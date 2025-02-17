using project_staff.Entities.Models;
using project_staff.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace project_staff.Contracts
{
	public interface IProjectRepository
	{
		Task<PagedList<Project>> GetAllProjectsAsync(ProjectParameters projectParameters, bool trackChanges);
		Task<Project> GetProjectAsync(Guid projectId, bool trackChanges);
		void CreateProject(Project project);
		void DeleteProject(Project project);
	}
}

using project_staff.Contracts;
using project_staff.Entities.Models;
using project_staff.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Service
{
	internal sealed class ProjectService : IProjectService
	{
		private readonly IRepositoryManager repositoryManager;
		private readonly ILoggerManager loggerManager;

		public ProjectService(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
		{
			this.repositoryManager = repositoryManager;
			this.loggerManager = loggerManager;
		}

		public IEnumerable<Project> GetAllProjects(bool trackChanges)
		{
			try
			{
				var projects = this.repositoryManager.Project.GetAllProjects(trackChanges);

				return projects;
			}
			catch (Exception ex)
			{
				loggerManager.LogError($"Something went wrong in the { nameof(GetAllProjects)} service method {ex}"); 
		    
				throw;
			}
		}
	}
}

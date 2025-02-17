using AutoMapper;
using project_staff.Contracts;
using project_staff.Entities.Exceptions;
using project_staff.Service.Contracts;
using project_staff.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Service
{
	internal sealed class ProjectTaskService : IProjectTaskService
	{
		private readonly IRepositoryManager repositoryManager;
		private readonly ILoggerManager loggerManager;
		private readonly IMapper mapper;

		public ProjectTaskService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
		{
			this.repositoryManager = repositoryManager;
			this.loggerManager = loggerManager;
			this.mapper = mapper;
		}

		public IEnumerable<ProjectTaskDto> GetTasks(Guid projectId, bool trackChanges)
		{
			var project = this.repositoryManager.Project.GetProject(projectId, trackChanges);

			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			var tasks = this.repositoryManager.ProjectTask.GetTasks(projectId, trackChanges);

			var tasksDto = this.mapper.Map<IEnumerable<ProjectTaskDto>>(tasks);

			return tasksDto;
		}
	}
}

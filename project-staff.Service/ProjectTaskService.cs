using AutoMapper;
using project_staff.Contracts;
using project_staff.Entities.Exceptions;
using project_staff.Entities.Models;
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

		public ProjectTaskDto CreateTaskForProject(Guid projectId, ProjectTaskForCreationDto projectTaskForCreationDto, bool trackChanges)
		{
			var project = this.repositoryManager.Project.GetProject(projectId, trackChanges);

			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			var projectTask = this.mapper.Map<ProjectTask>(projectTaskForCreationDto);

			this.repositoryManager.ProjectTask.CreateTaskForProject(projectId, projectTask);
			this.repositoryManager.Save();

			var projectTaskDto = this.mapper.Map<ProjectTaskDto>(projectTask);

			return projectTaskDto;
		}

		public void DeleteTaskForProject(Guid projectId, Guid id, bool trackChanges)
		{
			var project = this.repositoryManager.Project.GetProject(projectId, trackChanges);

			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			var task = this.repositoryManager.ProjectTask.GetTask(projectId, id, trackChanges);

			if (task is null)
			{
				throw new TaskNotFoundException(id);
			}

			this.repositoryManager.ProjectTask.DeleteTask(task);
			this.repositoryManager.Save();
		}

		public ProjectTaskDto GetTask(Guid projectId, Guid id, bool trackChanges)
		{
			var project = this.repositoryManager.Project.GetProject(projectId, trackChanges);
			
			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			var task = this.repositoryManager.ProjectTask.GetTask(projectId, id, trackChanges);

			if (task is null)
			{
				throw new TaskNotFoundException(id);
			}

			var taskDto = this.mapper.Map<ProjectTaskDto>(task);

			return taskDto;
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

		public void UpdateTaskForProject(Guid projectId, Guid id, ProjectTaskForUpdateDto projectTaskForUpdateDto, bool projectTrackChanges, bool taskTrackChanges)
		{
			var project = this.repositoryManager.Project.GetProject(projectId, projectTrackChanges);

			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			var task = this.repositoryManager.ProjectTask.GetTask(projectId, id, taskTrackChanges);

			if (task is null)
			{
				throw new TaskNotFoundException(id);
			}

			this.mapper.Map(projectTaskForUpdateDto, task);
			this.repositoryManager.Save();
		}
	}
}

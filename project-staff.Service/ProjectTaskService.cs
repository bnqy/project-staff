using AutoMapper;
using project_staff.Contracts;
using project_staff.Entities.Exceptions;
using project_staff.Entities.Models;
using project_staff.Service.Contracts;
using project_staff.Shared.DTOs;
using project_staff.Shared.RequestFeatures;
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

		public async Task<ProjectTaskDto> CreateTaskForProjectAsync(Guid projectId, ProjectTaskForCreationDto projectTaskForCreationDto, bool trackChanges)
		{
			var project = await this.repositoryManager.Project.GetProjectAsync(projectId, trackChanges);

			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			var projectTask = this.mapper.Map<ProjectTask>(projectTaskForCreationDto);

			this.repositoryManager.ProjectTask.CreateTaskForProject(projectId, projectTask);
			await this.repositoryManager.SaveAsync();

			var projectTaskDto = this.mapper.Map<ProjectTaskDto>(projectTask);

			return projectTaskDto;
		}

		public async Task DeleteTaskForProjectAsync(Guid projectId, Guid id, bool trackChanges)
		{
			var project = await this.repositoryManager.Project.GetProjectAsync(projectId, trackChanges);

			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			var task = await this.repositoryManager.ProjectTask.GetTaskAsync(projectId, id, trackChanges);

			if (task is null)
			{
				throw new TaskNotFoundException(id);
			}

			this.repositoryManager.ProjectTask.DeleteTask(task);
			await this.repositoryManager.SaveAsync();
		}

		public async Task<ProjectTaskDto> GetTaskAsync(Guid projectId, Guid id, bool trackChanges)
		{
			var project = await this.repositoryManager.Project.GetProjectAsync(projectId, trackChanges);
			
			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			var task = await this.repositoryManager.ProjectTask.GetTaskAsync(projectId, id, trackChanges);

			if (task is null)
			{
				throw new TaskNotFoundException(id);
			}

			var taskDto = this.mapper.Map<ProjectTaskDto>(task);

			return taskDto;
		}

		public async Task<(IEnumerable<ProjectTaskDto> projectTaskDtos, MetaData metaData)> GetTasksAsync(Guid projectId, TaskParameters taskParameters, bool trackChanges)
		{
			var project = await this.repositoryManager.Project.GetProjectAsync(projectId, trackChanges);

			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			var tasks = await this.repositoryManager.ProjectTask.GetTasksAsync(projectId, taskParameters, trackChanges);

			var tasksDtos = this.mapper.Map<IEnumerable<ProjectTaskDto>>(tasks);

			return (tasksDtos, tasks.MetaData);
		}

		public async Task UpdateTaskForProjectAsync(Guid projectId, Guid id, ProjectTaskForUpdateDto projectTaskForUpdateDto, bool projectTrackChanges, bool taskTrackChanges)
		{
			var project = await this.repositoryManager.Project.GetProjectAsync(projectId, projectTrackChanges);

			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			var task = await this.repositoryManager.ProjectTask.GetTaskAsync(projectId, id, taskTrackChanges);

			if (task is null)
			{
				throw new TaskNotFoundException(id);
			}

			this.mapper.Map(projectTaskForUpdateDto, task);
			await this.repositoryManager.SaveAsync();
		}
	}
}

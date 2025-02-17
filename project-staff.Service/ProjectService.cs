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
	internal sealed class ProjectService : IProjectService
	{
		private readonly IRepositoryManager repositoryManager;
		private readonly ILoggerManager loggerManager;
		private readonly IMapper mapper;

		public ProjectService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
		{
			this.repositoryManager = repositoryManager;
			this.loggerManager = loggerManager;
			this.mapper = mapper;
		}

		public async Task<ProjectDto> CreateProjectAsync(ProjectForCreationDto projectForCreationDto)
		{
			var project = this.mapper.Map<Project>(projectForCreationDto);

			this.repositoryManager.Project.CreateProject(project);
			await this.repositoryManager.SaveAsync();

			var projectDto = this.mapper.Map<ProjectDto>(project);

			return projectDto;
		}

		public async Task DeleteProjectAsync(Guid projectId, bool trackChanges)
		{
			var project = await this.repositoryManager.Project.GetProjectAsync(projectId, trackChanges);

			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			this.repositoryManager.Project.DeleteProject(project);
			await this.repositoryManager.SaveAsync();
		}

		public async Task<(IEnumerable<ProjectDto> projectDtos, MetaData metaData)> GetAllProjectsAsync(ProjectParameters projectParameters, bool trackChanges)
		{
			var projects = await this.repositoryManager.Project.GetAllProjectsAsync(projectParameters, trackChanges);
				
			var projectsDtos = this.mapper.Map<IEnumerable<ProjectDto>>(projects);
				
			return (projectsDtos, projects.MetaData);
		}

		public async Task<ProjectDto> GetProjectAsync(Guid projectId, bool trackChanges)
		{
			var project = await this.repositoryManager.Project.GetProjectAsync(projectId, trackChanges);

			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			var projectDto = this.mapper.Map<ProjectDto>(project);

			return projectDto;
		}

		public async Task UpdateProjectAsync(Guid projectId, ProjectForUpdateDto projectForUpdateDto, bool trackChanges)
		{
			var project = await this.repositoryManager.Project.GetProjectAsync(projectId, trackChanges);

			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			this.mapper.Map(projectForUpdateDto, project);
			await this.repositoryManager.SaveAsync();
		}
	}
}

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

		public ProjectDto CreateProject(ProjectForCreationDto projectForCreationDto)
		{
			var project = this.mapper.Map<Project>(projectForCreationDto);

			this.repositoryManager.Project.CreateProject(project);
			this.repositoryManager.Save();

			var projectDto = this.mapper.Map<ProjectDto>(project);

			return projectDto;
		}

		public IEnumerable<ProjectDto> GetAllProjects(bool trackChanges)
		{
			var projects = this.repositoryManager.Project.GetAllProjects(trackChanges);
				
			var projectsDto = this.mapper.Map<IEnumerable<ProjectDto>>(projects);
				
			return projectsDto;
		}

		public ProjectDto GetProject(Guid projectId, bool trackChanges)
		{
			var project = this.repositoryManager.Project.GetProject(projectId, trackChanges);

			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			var projectDto = this.mapper.Map<ProjectDto>(project);

			return projectDto;
		}
	}
}

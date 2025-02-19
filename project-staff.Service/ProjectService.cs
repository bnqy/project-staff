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

		public async Task<(bool IsSuccess, string ErrorMessage)> AddEmployeeToProjectAsync(Guid userId, Guid projectId, Guid employeeId, bool trackChanges)
		{
			var project = await repositoryManager.Project.GetProjectAsync(projectId, userId, trackChanges);

			if (project == null)
				return (false, $"Project with id {projectId} not found.");

			var user = await repositoryManager.ApplicationUser.GetUserByIdAsync(employeeId, trackChanges: false);
			if (user == null)
				return (false, $"Employee with id {employeeId} not found.");

			bool alreadyAssigned = project.Employees.Any(e => e.Id == employeeId);

			if (alreadyAssigned)
				return (false, "Employee is already assigned to this project.");

			project.Employees.Add(user);

			await repositoryManager.SaveAsync();

			return (true, null);
		}

		public async Task<ProjectDto> CreateProjectAsync(Guid userId, ProjectForCreationDto projectForCreationDto)
		{
			var project = this.mapper.Map<Project>(projectForCreationDto);

			project.ManagerId = userId;

			this.repositoryManager.Project.CreateProject(project);
			await this.repositoryManager.SaveAsync();

			var projectDto = this.mapper.Map<ProjectDto>(project);

			return projectDto;
		}

		public async Task DeleteProjectAsync(Guid projectId, Guid userId, bool trackChanges)
		{
			var project = await this.repositoryManager.Project.GetProjectAsync(projectId, userId, trackChanges);

			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			this.repositoryManager.Project.DeleteProject(project);
			await this.repositoryManager.SaveAsync();
		}

		public async Task<(IEnumerable<ProjectDto> projectDtos, MetaData metaData)> GetAllProjectsAsync(
			Guid userId, ProjectParameters projectParameters, bool trackChanges)
		{
			if (!projectParameters.ValidDateRange)
			{
				throw new DateRangeBadRequestException();
			}

			var projects = await this.repositoryManager.Project.GetAllProjectsAsync(userId, projectParameters, trackChanges);
				
			var projectsDtos = this.mapper.Map<IEnumerable<ProjectDto>>(projects);
				
			return (projectsDtos, projects.MetaData);
		}

		public async Task<ProjectDto> GetProjectAsync(Guid projectId, Guid userId, bool trackChanges)
		{
			var project = await this.repositoryManager.Project.GetProjectAsync(projectId, userId, trackChanges);

			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			var projectDto = this.mapper.Map<ProjectDto>(project);

			return projectDto;
		}

		public async Task<(bool IsSuccess, string ErrorMessage)> RemoveEmployeeFromProjectAsync(Guid userId, Guid projectId, Guid employeeId, bool trackChanges)
		{
			var project = await repositoryManager.Project.GetProjectAsync(projectId, userId, trackChanges);
			if (project == null)
				return (false, $"Project with id {projectId} not found.");


			var user = project.Employees.FirstOrDefault(e => e.Id == employeeId);

			if (user == null)
				return (false, $"Employee with id {employeeId} is not assigned to this project.");

			project.Employees.Remove(user);

			await repositoryManager.SaveAsync();

			return (true, null);
		}

		public async Task UpdateProjectAsync(Guid projectId, Guid userId,ProjectForUpdateDto projectForUpdateDto, bool trackChanges)
		{
			var project = await this.repositoryManager.Project.GetProjectAsync(projectId, userId, trackChanges);

			if (project is null)
			{
				throw new ProjectNotFoundException(projectId);
			}

			this.mapper.Map(projectForUpdateDto, project);
			await this.repositoryManager.SaveAsync();
		}
	}
}

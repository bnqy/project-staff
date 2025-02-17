﻿using AutoMapper;
using project_staff.Contracts;
using project_staff.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Service
{
	public sealed class ServiceManager : IServiceManager
	{
		private readonly Lazy<IApplicationUserService> applicationUserService;
		private readonly Lazy<IProjectService> projectService;
		private readonly Lazy<IProjectTaskService> projectTaskService;


		public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
		{
			this.applicationUserService = new Lazy<IApplicationUserService>(() => new ApplicationUserService(repositoryManager, loggerManager, mapper));
			this.projectService = new Lazy<IProjectService>(() => new ProjectService(repositoryManager, loggerManager, mapper));
			this.projectTaskService = new Lazy<IProjectTaskService>(() => new ProjectTaskService(repositoryManager, loggerManager, mapper));
		}

		public IApplicationUserService ApplicationUserService => this.applicationUserService.Value;

		public IProjectService ProjectService => this.projectService.Value;

		public IProjectTaskService ProjectTaskService => this.projectTaskService.Value;
	}
}

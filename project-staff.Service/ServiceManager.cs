using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
	public sealed class ServiceManager : IServiceManager
	{
		private readonly Lazy<IApplicationUserService> applicationUserService;
		private readonly Lazy<IProjectService> projectService;
		private readonly Lazy<IProjectTaskService> projectTaskService;
		private readonly Lazy<IAccountService> accountService;


		public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper
			, UserManager<ApplicationUser> userManager,
			IConfiguration configuration)
		{
			this.applicationUserService = new Lazy<IApplicationUserService>(() => new ApplicationUserService(repositoryManager, loggerManager, mapper));
			this.projectService = new Lazy<IProjectService>(() => new ProjectService(repositoryManager, loggerManager, mapper));
			this.projectTaskService = new Lazy<IProjectTaskService>(() => new ProjectTaskService(repositoryManager, loggerManager, mapper));
			this.accountService = new Lazy<IAccountService>(() => new AccountService(loggerManager, mapper, userManager, configuration));
		}

		public IApplicationUserService ApplicationUserService => this.applicationUserService.Value;

		public IProjectService ProjectService => this.projectService.Value;

		public IProjectTaskService ProjectTaskService => this.projectTaskService.Value;

		public IAccountService AccountService => this.accountService.Value;
	}
}

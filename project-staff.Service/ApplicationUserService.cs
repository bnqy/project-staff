using project_staff.Contracts;
using project_staff.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Service
{
	internal sealed class ApplicationUserService : IApplicationUserService
	{
		private readonly IRepositoryManager repositoryManager;
		private readonly ILoggerManager loggerManager;

		public ApplicationUserService(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
		{
			this.repositoryManager = repositoryManager;
			this.loggerManager = loggerManager;
		}
	}
}

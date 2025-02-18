using project_staff.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Service.Contracts
{
	public interface IServiceManager
	{
		IApplicationUserService ApplicationUserService { get; }
		IProjectService ProjectService { get; }
		IProjectTaskService ProjectTaskService { get; }
		IAccountService AccountService { get; }
	}
}

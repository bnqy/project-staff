using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Contracts
{
	public interface IRepositoryManager
	{
		IApplicationUserRepository ApplicationUser { get; }
		IProjectRepository Project { get; }
		IProjectTaskRepository ProjectTask { get; }
		Task SaveAsync();
	}
}

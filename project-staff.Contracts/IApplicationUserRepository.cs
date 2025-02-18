using project_staff.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Contracts
{
	public interface IApplicationUserRepository
	{
		Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(bool trackChanges);
		Task<ApplicationUser> GetUserByIdAsync(Guid userId, bool trackChanges);
		void CreateUser(ApplicationUser user);
		void DeleteUser(ApplicationUser user);
	}
}

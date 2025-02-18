using Microsoft.EntityFrameworkCore;
using project_staff.Contracts;
using project_staff.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Repository
{
	public class ApplicationUserRepository : RepositoryBase<ApplicationUser>, IApplicationUserRepository
	{
		public ApplicationUserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
		{
		}

		public void CreateUser(ApplicationUser user)
		{
			throw new NotImplementedException();
		}

		public void DeleteUser(ApplicationUser user)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(bool trackChanges)
		{
			return await FindAll(trackChanges)
				.OrderBy(u => u.UserName)
				.ToListAsync();
		}

		public Task<ApplicationUser> GetUserByIdAsync(string userId, bool trackChanges)
		{
			throw new NotImplementedException();
		}
	}
}

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
			Create(user);
		}

		public void DeleteUser(ApplicationUser user)
		{
			Delete(user);
		}

		public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync(bool trackChanges)
		{
			return await FindAll(trackChanges)
				.OrderBy(u => u.UserName)
				.ToListAsync();
		}

		public async Task<ApplicationUser> GetUserByIdAsync(Guid userId, bool trackChanges)
		{
			return await FindByCondition(u => u.Id == userId, trackChanges)
				.SingleOrDefaultAsync();
		}
	}
}

using AutoMapper;
using project_staff.Contracts;
using project_staff.Service.Contracts;
using project_staff.Shared.DTOs;
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
		private readonly IMapper mapper;

		public ApplicationUserService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
		{
			this.repositoryManager = repositoryManager;
			this.loggerManager = loggerManager;
			this.mapper = mapper;
		}

		public async Task<IEnumerable<ApplicationUserDto>> GetAllUsersAsync(bool trackChanges)
		{
			var users = await repositoryManager.ApplicationUser.GetAllUsersAsync(trackChanges);
			var usersDto = mapper.Map<IEnumerable<ApplicationUserDto>>(users);
			return usersDto;
		}
	}
}

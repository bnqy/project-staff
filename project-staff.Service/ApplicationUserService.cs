using AutoMapper;
using project_staff.Contracts;
using project_staff.Entities.Exceptions;
using project_staff.Entities.Models;
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

		public async Task<ApplicationUserDto> CreateUserAsync(ApplicationUserForCreationDto userForCreation)
		{
			var userEntity = mapper.Map<ApplicationUser>(userForCreation);

			repositoryManager.ApplicationUser.CreateUser(userEntity);

			await repositoryManager.SaveAsync();

			var userToReturn = mapper.Map<ApplicationUserDto>(userEntity);

			return userToReturn;
		}

		public async Task DeleteUserAsync(Guid userId, bool trackChanges)
		{
			var user = await repositoryManager.ApplicationUser.GetUserByIdAsync(userId, trackChanges);

			/*if (user == null)
				throw new NotFoundException($"User with id {userId} does not exist.");*/

			repositoryManager.ApplicationUser.DeleteUser(user);

			await repositoryManager.SaveAsync();
		}

		public async Task<IEnumerable<ApplicationUserDto>> GetAllUsersAsync(bool trackChanges)
		{
			var users = await repositoryManager.ApplicationUser.GetAllUsersAsync(trackChanges);
			var usersDto = mapper.Map<IEnumerable<ApplicationUserDto>>(users);
			return usersDto;
		}

		public async Task<ApplicationUserDto> GetUserByIdAsync(Guid userId, bool trackChanges)
		{
			var user = await repositoryManager.ApplicationUser.GetUserByIdAsync(userId, trackChanges);

			/*if (user == null)
				throw new NotFoundException($"User with id {userId} does not exist.");*/

			var userDto = mapper.Map<ApplicationUserDto>(user);

			return userDto;
		}
	}
}

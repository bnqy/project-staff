using project_staff.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Repository
{
	public class RepositoryManager : IRepositoryManager
	{
		private readonly RepositoryContext repositoryContext;
		private readonly Lazy<IApplicationUserRepository> applicationUserRepository;
		private readonly Lazy<IProjectRepository> projectRepository;
		private readonly Lazy<IProjectTaskRepository> projectTaskRepository;

		public RepositoryManager(RepositoryContext repositoryContext)
		{
			this.repositoryContext = repositoryContext;
			this.applicationUserRepository = new Lazy<IApplicationUserRepository>(() => new ApplicationUserRepository(repositoryContext));
			this.projectRepository = new Lazy<IProjectRepository>(() => new ProjectRepository(repositoryContext));
			this.projectTaskRepository = new Lazy<IProjectTaskRepository>(() => new ProjectTaskRepository(repositoryContext));
		}

		public IApplicationUserRepository ApplicationUser => this.applicationUserRepository.Value;

		public IProjectRepository Project => this.projectRepository.Value;

		public IProjectTaskRepository ProjectTask => this.projectTaskRepository.Value;

		public async Task SaveAsync()
		{
			await this.repositoryContext.SaveChangesAsync();
		}
	}
}

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using project_staff.Repository;

namespace project_staff.ContextFactory
{
	/// <summary>
	/// Gets Repository context from other project.
	/// </summary>
	public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
	{
		public RepositoryContext CreateDbContext(string[] args)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var builder = new DbContextOptionsBuilder<RepositoryContext>()
				.UseSqlServer(config.GetConnectionString("sqlConnection"),
				b => b.MigrationsAssembly("project-staff")); // Because migration assembly is not in MAIN project. It is in Repository project. And we changes it to MAIN project.

			return new RepositoryContext(builder.Options);
		}
	}
}

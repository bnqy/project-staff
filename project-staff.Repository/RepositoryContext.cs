using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project_staff.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Repository
{
	public class RepositoryContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
	{
		public RepositoryContext(DbContextOptions<RepositoryContext> options)
			: base(options)
		{

		}

		public DbSet<Project> Projects { get; set; }

		public DbSet<ProjectTask> ProjectTasks { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Configure many-to-many relationship between Project and ApplicationUser for Employees.
			builder.Entity<Project>()
				.HasMany(p => p.Employees)
				.WithMany(u => u.Projects)
				.UsingEntity(j => j.ToTable("ProjectEmployees"));

			// Configure one-to-many relationship: Project Manager.
			builder.Entity<Project>()
				.HasOne(p => p.Manager)
				.WithMany(u => u.ManagedProjects)
				.HasForeignKey(p => p.ManagerId)
				.OnDelete(DeleteBehavior.Restrict);

			// Configure relationship for ProjectTask: Author.
			builder.Entity<ProjectTask>()
				.HasOne(t => t.Author)
				.WithMany(u => u.AuthoredTasks)
				.HasForeignKey(t => t.AuthorId)
				.OnDelete(DeleteBehavior.Restrict);

			// Configure relationship for ProjectTask: Executor.
			builder.Entity<ProjectTask>()
				.HasOne(t => t.Executor)
				.WithMany(u => u.ExecutedTasks)
				.HasForeignKey(t => t.ExecutorId)
				.OnDelete(DeleteBehavior.Restrict);
		}

	}
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using project_staff.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Repository.Configuration
{
	public class ProjectConfiguration : IEntityTypeConfiguration<Project>
	{
		public void Configure(EntityTypeBuilder<Project> builder)
		{
			var projectId = new Guid("d97e8cfc-bc6f-4f94-bc0c-68fcb3c6362b");
			var adminId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a");
			var employeeId = new Guid("fa5bb702-8b95-4fa9-9f55-60cc62d3b159");

			builder.HasData(
				new Project
				{
					Id = projectId,
					Name = "Smart City AI",
					CustomerCompany = "Kyrgyz Innovations",
					ExecutionCompany = "Turkish Tech",
					StartDate = DateTime.UtcNow,
					EndDate = DateTime.UtcNow.AddMonths(6),
					Priority = 1,
					ManagerId = adminId // Use the specific GUID for the manager
				}
			);

			builder.HasMany(p => p.Employees)
			.WithMany(u => u.Projects)
			.UsingEntity(j => j.HasData(
				new { ProjectsId = projectId, EmployeesId = adminId },
				new { ProjectsId = projectId, EmployeesId = employeeId }
			));
		}
	}
}

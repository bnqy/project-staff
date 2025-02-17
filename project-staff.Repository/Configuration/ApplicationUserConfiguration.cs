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
	public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			var adminId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a");
			var employeeId = new Guid("fa5bb702-8b95-4fa9-9f55-60cc62d3b159");

			builder.HasData(
			new ApplicationUser
			{
				Id = adminId,
				FirstName = "Admin",
				LastName = "User",
				MiddleName = "Super",
				UserName = "admin@example.com",
				NormalizedUserName = "ADMIN@EXAMPLE.COM",
				Email = "admin@example.com",
				NormalizedEmail = "ADMIN@EXAMPLE.COM",
				EmailConfirmed = true,
				PasswordHash = "AQAAAAEAACcQAAAAEHXz...", // Pre-hashed password
				SecurityStamp = Guid.NewGuid().ToString(),
				ConcurrencyStamp = Guid.NewGuid().ToString()
			},
			new ApplicationUser
			{
				Id = employeeId,
				FirstName = "Kyrgyz",
				LastName = "Employee",
				MiddleName = "Tech",
				UserName = "employee@example.com",
				NormalizedUserName = "EMPLOYEE@EXAMPLE.COM",
				Email = "employee@example.com",
				NormalizedEmail = "EMPLOYEE@EXAMPLE.COM",
				EmailConfirmed = true,
				PasswordHash = "AQAAAAEAACcQAAAAEHXz...",
				SecurityStamp = Guid.NewGuid().ToString(),
				ConcurrencyStamp = Guid.NewGuid().ToString()
			}
		);
		}
	}
}

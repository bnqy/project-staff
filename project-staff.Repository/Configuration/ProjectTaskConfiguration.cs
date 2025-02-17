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
	public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
	{
		public void Configure(EntityTypeBuilder<ProjectTask> builder)
		{
			var taskId = new Guid("10d7c6f3-85fc-4fa7-a9a1-2f828c053c0c");
			var authorId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a");
			var executorId = new Guid("fa5bb702-8b95-4fa9-9f55-60cc62d3b159");
			var projectId = new Guid("d97e8cfc-bc6f-4f94-bc0c-68fcb3c6362b");

			builder.HasData(
				new ProjectTask
				{
					Id = taskId,
					Name = "Develop AI Model",
					AuthorId = authorId,  // Use the specific GUID for the author
					ExecutorId = executorId,  // Use the specific GUID for the executor
					Status = Entities.Models.TaskStatus.ToDo,
					Comment = "First stage of AI implementation.",
					Priority = 1,
					ProjectId = projectId // Use the specific GUID for the project
				}
			);
		}
	}
}

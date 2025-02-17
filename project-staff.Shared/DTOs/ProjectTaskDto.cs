using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Shared.DTOs
{
	public record ProjectTaskDto
	{
		public Guid Id { get; init; }
		public string? Name { get; init; }
		public Guid AuthorId { get; init; }
		public Guid ExecutorId { get; init; }
		public project_staff.Entities.Models.TaskStatus Status { get; init; }
		public string? Comment { get; init; }
		public int Priority { get; init; }
	}
}

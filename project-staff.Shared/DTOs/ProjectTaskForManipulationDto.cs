using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Shared.DTOs
{
	public abstract record ProjectTaskForManipulationDto
	{
		[Required(ErrorMessage = "Name is required.")]
		public string? Name { get; init; }

		//public Guid AuthorId { get; init; }

		//public Guid ExecutorId { get; init; }

		[Required(ErrorMessage = "Status is required.")]
		public project_staff.Entities.Models.TaskStatus Status { get; init; }

		[StringLength(500, ErrorMessage = "Comment must not exceed 500 characters.")]
		public string? Comment { get; init; }

		[Required(ErrorMessage = "Priority is required.")]
		[Range(0, 10, ErrorMessage = "Priority must be between 0 and 10.")]
		public int Priority { get; init; }
	}
}

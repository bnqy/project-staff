using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Shared.DTOs
{
	public abstract record ProjectForManipulationDto
	{
		[Required(ErrorMessage = "Name is required.")]
		public string? Name { get; init; }

		[Required(ErrorMessage = "CustomerCompany is required.")]
		public string? CustomerCompany { get; init; }

		[Required(ErrorMessage = "ExecutionCompany is required.")]
		public string? ExecutionCompany { get; init; }
		public DateTime StartDate { get; init; }
		public DateTime EndDate { get; init; }

		[Required(ErrorMessage = "Priority is required.")]
		[Range(0, 10, ErrorMessage = "Can not be negative.0-10")]
		public int Priority { get; init; }
		public Guid ManagerId { get; init; }
	}
}

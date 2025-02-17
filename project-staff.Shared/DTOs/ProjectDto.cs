using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Shared.DTOs
{
	public record ProjectDto
	{
		public Guid Id { get; init; }
		public string? Name { get; init; }
		public string? CustomerCompany { get; init; }
		public string? ExecutionCompany { get; init; }
		public DateTime StartDate { get; init; }
		public DateTime EndDate { get; init; }
		public int Priority { get; init; }
	}
}

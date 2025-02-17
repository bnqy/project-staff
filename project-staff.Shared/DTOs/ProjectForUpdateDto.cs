using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Shared.DTOs
{
	public record ProjectForUpdateDto : ProjectForManipulationDto
	{
		public IEnumerable<ProjectTaskForCreationDto>? Tasks { get; init; }
	};
}

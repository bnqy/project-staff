using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Shared.DTOs
{
	public record ProjectTaskForUpdateDto(
		string Name,
		Guid AuthorId,
		Guid ExecutorId,
		project_staff.Entities.Models.TaskStatus Status,
		string Comment,
		int Priority);
}

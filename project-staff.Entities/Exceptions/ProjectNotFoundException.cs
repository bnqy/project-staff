using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Entities.Exceptions
{
	public sealed class ProjectNotFoundException : NotFoundException
	{
		public ProjectNotFoundException(Guid projectId) : base($"Project with id: {projectId} is not found.")
		{
		}
	}
}

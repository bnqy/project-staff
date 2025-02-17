using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Entities.Exceptions
{
	public sealed class TaskNotFoundException : NotFoundException
	{
		public TaskNotFoundException(Guid taskId) : base($"Task with id: {taskId} is not found.")
		{
		}
	}
}

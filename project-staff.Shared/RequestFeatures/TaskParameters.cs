using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Shared.RequestFeatures
{
	public class TaskParameters : RequestParameters
	{
		public project_staff.Entities.Models.TaskStatus? Status { get; set; }
	}
}

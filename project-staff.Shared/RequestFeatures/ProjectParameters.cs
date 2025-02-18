using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Shared.RequestFeatures
{
	public class ProjectParameters : RequestParameters
	{
		public DateTime? StartDate { get; set; } = DateTime.MinValue;
		public DateTime? EndDate { get; set; } = DateTime.MaxValue;

		public string? SearchTerm { get; set; }

		public bool ValidDateRange => StartDate < EndDate;
	}
}

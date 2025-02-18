using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Entities.Exceptions
{
	public sealed class DateRangeBadRequestException : BadRequestException
	{
		public DateRangeBadRequestException() 
			: base("Date range is invalid. Start date can not be after end date.")
		{
		}
	}
}

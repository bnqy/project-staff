using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Shared.DTOs
{
	public record ApplicationUserDto(Guid Id,
		string FirstName,
		string LastName,
		string MiddleName,
		string Email
		);
}

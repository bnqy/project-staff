﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Shared.DTOs
{
	public record ApplicationUserDto
	{
		public Guid Id { get; init; }
		public string? FirstName { get; init; }
		public string? LastName { get; init; }
		public string? MiddleName { get; init; }
		public string? Email { get; init; }
	}
}

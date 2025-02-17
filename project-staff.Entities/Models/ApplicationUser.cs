using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Entities.Models
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		
		// First name of the employee.
		[Required(ErrorMessage = "First name of employee is required.")]
		public string? FirstName { get; set; }

		// Last name of the employee.
		[Required(ErrorMessage = "Last name of employee is required.")]
		public string? LastName { get; set; }

		// Middle name of the employee.
		public string? MiddleName { get; set; }

		// Projects where the user is the project manager.
		public virtual ICollection<Project> ManagedProjects { get; set; } = new List<Project>();

		// Projects where the user is assigned as an employee.
		public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

		// Tasks authored by the user.
		public virtual ICollection<ProjectTask> AuthoredTasks { get; set; } = new List<ProjectTask>();

		// Tasks where the user is the executor.
		public virtual ICollection<ProjectTask> ExecutedTasks { get; set; } = new List<ProjectTask>();
	}
}

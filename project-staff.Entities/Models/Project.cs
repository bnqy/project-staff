using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Entities.Models
{
	/// <summary>
	/// Represents a project with associated companies, dates, priority, manager, employees, and tasks.
	/// </summary>
	public class Project
	{
		// Primary key.
		[Column("ProjectId")]
		public Guid Id { get; set; }

		// Name of the project.
		[Required(ErrorMessage = "Project's Name is required.")]
		public string? Name { get; set; }

		// Customer company name.
		[Required(ErrorMessage = "Project's CustomerCompany is required.")]
		public string? CustomerCompany { get; set; }

		// Execution company name.
		[Required(ErrorMessage = "Project's ExecutionCompany is required.")]
		public string? ExecutionCompany { get; set; }

		// Project start date.
		public DateTime StartDate { get; set; }

		// Project end date.
		public DateTime EndDate { get; set; }

		// Project priority.
		public int Priority { get; set; }

		// Foreign key to the project manager (ApplicationUser).
		[ForeignKey(nameof(Manager))]
		public Guid ManagerId { get; set; }

		// Navigation property for the project manager.
		public ApplicationUser? Manager { get; set; }

		// Many-to-many relationship: Employees assigned to the project.
		public virtual ICollection<ApplicationUser> Employees { get; set; } = new List<ApplicationUser>();

		// One-to-many relationship: Tasks associated with the project.
		public virtual ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
	}
}

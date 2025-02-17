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
	/// Enumeration for task status.
	/// </summary>
	public enum TaskStatus
	{
		ToDo,
		InProgress,
		Done
	}

	/// <summary>
	/// Represents a task within a project.
	/// </summary>
	public class ProjectTask
	{
		// Primary key.
		[Column("TaskId")]
		public Guid Id { get; set; }

		// Task name.
		[Required(ErrorMessage = "Task's Name is required.")]
		public string? Name { get; set; }

		// Foreign key to the author (creator) of the task.
		[ForeignKey(nameof(Author))]
		public Guid AuthorId { get; set; }

		// Navigation property for the author.
		public ApplicationUser? Author { get; set; }

		// Foreign key to the executor (assigned employee) of the task.
		[ForeignKey(nameof(Executor))]
		public Guid ExecutorId { get; set; }

		// Navigation property for the executor.
		public ApplicationUser? Executor { get; set; }

		// Current status of the task.
		public TaskStatus Status { get; set; }

		// Additional comments about the task.
		public string? Comment { get; set; }

		// Task priority.
		public int Priority { get; set; }

		// Foreign key to the associated project.
		[ForeignKey(nameof(Project))]
		public Guid ProjectId { get; set; }

		// Navigation property for the project.
		public Project? Project { get; set; }
	}
}

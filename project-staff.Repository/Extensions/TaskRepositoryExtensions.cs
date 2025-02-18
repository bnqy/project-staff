using project_staff.Entities.Models;
using project_staff.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Repository.Extensions
{
	public static class TaskRepositoryExtensions
	{
		public static IQueryable<ProjectTask> Search(this IQueryable<ProjectTask> tasks, string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
			{
				return tasks;
			}

			var lowerCaseTerm = searchTerm.Trim().ToLower();

			return tasks.Where(t => t.Name.ToLower().Contains(lowerCaseTerm));
		}
	}
}

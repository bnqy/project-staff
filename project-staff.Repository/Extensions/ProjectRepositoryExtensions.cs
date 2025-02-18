using project_staff.Entities.Models;
using project_staff.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Repository.Extensions
{
	public static class ProjectRepositoryExtensions
	{
		public static IQueryable<Project> Search(this IQueryable<Project> projects, string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
			{
				return projects;
			}

			var lowerCaseTerm = searchTerm.Trim().ToLower();

			return projects.Where(t => t.Name.ToLower().Contains(lowerCaseTerm));
		}

		public static IQueryable<Project> FilterProjects(this IQueryable<Project> projects, ProjectParameters projectParameters) =>

			projects.Where(p =>
			(!projectParameters.StartDate.HasValue || p.StartDate >= projectParameters.StartDate) && // Filter by start date																		
				(!projectParameters.EndDate.HasValue || p.EndDate <= projectParameters.EndDate));
	}
}

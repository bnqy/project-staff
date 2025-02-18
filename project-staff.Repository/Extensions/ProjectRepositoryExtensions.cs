using project_staff.Entities.Models;
using project_staff.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Reflection;

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


		public static IQueryable<Project> Sort(this IQueryable<Project> projects, string orderByQueryString)
		{
			if (string.IsNullOrWhiteSpace(orderByQueryString))
			{
				return projects.OrderBy(t => t.Name);
			}

			var orderParams = orderByQueryString.Trim().Split(',');

			var propertyInfos = typeof(Project).GetProperties(BindingFlags.Public | BindingFlags.Instance);

			var orderQueryBuilder = new StringBuilder();

			foreach (var param in orderParams)
			{
				if (string.IsNullOrWhiteSpace(param))
					continue;

				var propertyFromQueryName = param.Split(" ")[0];
				var objectProperty = propertyInfos.FirstOrDefault(pi =>
				pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

				if (objectProperty == null)
					continue;

				var direction = param.EndsWith(" desc") ? "descending" : "ascending";

				orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
			}

			var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

			if (string.IsNullOrWhiteSpace(orderQuery))
			{
				return projects.OrderBy(e => e.Name);
			}

			return projects.OrderBy(orderQuery);
		}
	}
}

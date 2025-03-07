﻿using project_staff.Entities.Models;
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

		public static IQueryable<ProjectTask> Sort(this IQueryable<ProjectTask> tasks, string orderByQueryString)
		{
			if (string.IsNullOrWhiteSpace(orderByQueryString))
			{
				return tasks.OrderBy(t => t.Name);
			}

			var orderParams = orderByQueryString.Trim().Split(',');

			var propertyInfos = typeof(ProjectTask).GetProperties(BindingFlags.Public | BindingFlags.Instance);

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
				return tasks.OrderBy(e => e.Name);
			}

			return tasks.OrderBy(orderQuery);
		}
	}
}

﻿using Microsoft.EntityFrameworkCore;
using project_staff.Contracts;
using project_staff.Entities.Models;
using project_staff.Repository.Extensions;
using project_staff.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Repository
{
	public class ProjectTaskRepository : RepositoryBase<ProjectTask>, IProjectTaskRepository
	{
		public ProjectTaskRepository(RepositoryContext repositoryContext) : base(repositoryContext)
		{
		}

		public void CreateTaskForProject(Guid projectId, ProjectTask projectTask)
		{
			projectTask.ProjectId = projectId;

			Create(projectTask);
		}

		public void DeleteTask(ProjectTask projectTask)
		{
			Delete(projectTask);
		}

		public async Task<ProjectTask> GetTaskAsync(Guid projectId, Guid id, bool trackChanges)
		{
			return await FindByCondition(e => e.ProjectId.Equals(projectId) && e.Id.Equals(id),trackChanges)
				.SingleOrDefaultAsync();
		}

		public async Task<PagedList<ProjectTask>> GetTasksAsync(Guid projectId, TaskParameters taskParameters, bool trackChanges)
		{
			/*var projectTasks = await FindByCondition(t => t.ProjectId.Equals(projectId), trackChanges)
				.OrderBy(t => t.Name)
				.ToListAsync();*/

			var query = FindByCondition(t => t.ProjectId.Equals(projectId), trackChanges);

			if (taskParameters.Status.HasValue)
			{
				query = query.Where(t => t.Status == taskParameters.Status.Value);
			}

			var projectTasks = await query
				.Search(taskParameters.SearchTerm)
				//.OrderBy(t => t.Name)
				.Sort(taskParameters.OrderBy)
				.ToListAsync();

			return PagedList<ProjectTask>
				.ToPagedList(projectTasks, taskParameters.PageNumber, taskParameters.PageSize);
		}
	}
}

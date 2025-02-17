using Microsoft.EntityFrameworkCore;
using project_staff.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Repository
{
	public class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		private readonly RepositoryContext repositoryContext;

		public RepositoryBase(RepositoryContext repositoryContext)
		{
			this.repositoryContext = repositoryContext;
		}

		public void Create(T entity)
		{
			this.repositoryContext.Set<T>().Add(entity);
		}

		public void Delete(T entity)
		{
			this.repositoryContext.Set<T>().Remove(entity);
		}

		public IQueryable<T> FindAll(bool trackChanges)
		{
			return !trackChanges ? this.repositoryContext.Set<T>()
				.AsNoTracking() : this.repositoryContext.Set<T>();
		}

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
		{
			return !trackChanges ? this.repositoryContext.Set<T>()
				.Where(expression)
				.AsNoTracking() : this.repositoryContext.Set<T>()
				.Where(expression);
		}

		public void Update(T entity)
		{
			this.repositoryContext.Set<T>().Update(entity);
		}
	}
}

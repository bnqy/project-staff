using project_staff.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Contracts
{
	public interface IProjectRepository
	{
		IEnumerable<Project> GetAllProjects(bool trackChanges);
	}
}

using Microsoft.AspNetCore.Mvc;
using project_staff.Services.WebApi;
using project_staff.Shared.DTOs;
using project_staff.Shared.RequestFeatures;

namespace project_staff.WebApp.Controllers
{
	public class ProjectsController : Controller
	{
		private readonly ProjectApiClient _projectApiClient;

		public ProjectsController(ProjectApiClient projectApiClient)
		{
			_projectApiClient = projectApiClient;
		}

		// GET: /Projects
		public async Task<IActionResult> Index(ProjectParameters parameters)
		{
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var pagedProjects = await _projectApiClient.GetProjectsAsync(parameters, token);

			return View(pagedProjects);
		}

		// GET: /Projects/Details/{id}
		public async Task<IActionResult> Details(Guid id)
		{
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var project = await _projectApiClient.GetProjectByIdAsync(id, token);
			if (project == null)
				return NotFound();
			return View(project);
		}

		// GET: /Projects/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: /Projects/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProjectForCreationDto projectDto)
		{
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
				return View(projectDto);

			var success = await _projectApiClient.CreateProjectAsync(projectDto, token);
			if (success)
				return RedirectToAction(nameof(Index));

			ModelState.AddModelError("", "Failed to create project.");
			return View(projectDto);
		}

		// GET: /Projects/Edit/{id}
		public async Task<IActionResult> Edit(Guid id)
		{
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var project = await _projectApiClient.GetProjectByIdAsync(id, token);
			if (project == null)
				return NotFound();

			var updateDto = new ProjectForUpdateDto
			{
				Name = project.Name,
				CustomerCompany = project.CustomerCompany,
				ExecutionCompany = project.ExecutionCompany,
				Priority = project.Priority,
				StartDate = project.StartDate,
				EndDate = project.EndDate
			};

			return View(updateDto);
		}

		// POST: /Projects/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, ProjectForUpdateDto projectDto)
		{
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
				return View(projectDto);

			var success = await _projectApiClient.UpdateProjectAsync(id, projectDto, token);
			if (success)
				return RedirectToAction(nameof(Index));

			ModelState.AddModelError("", "Failed to update project.");
			return View(projectDto);
		}

		// GET: /Projects/Delete/{id}
		public async Task<IActionResult> Delete(Guid id)
		{
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var project = await _projectApiClient.GetProjectByIdAsync(id, token);
			if (project == null)
				return NotFound();
			return View(project);
		}

		// POST: /Projects/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var success = await _projectApiClient.DeleteProjectAsync(id, token);
			return RedirectToAction(nameof(Index));
		}

		// POST: /Projects/AddEmployee
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddEmployee(Guid projectId, Guid employeeId)
		{
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var success = await _projectApiClient.AddEmployeeToProjectAsync(projectId, employeeId, token);
			if (!success)
				TempData["Error"] = "Failed to add employee.";
			return RedirectToAction(nameof(Details), new { id = projectId });
		}

		// POST: /Projects/RemoveEmployee
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RemoveEmployee(Guid projectId, Guid employeeId)
		{
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var success = await _projectApiClient.RemoveEmployeeFromProjectAsync(projectId, employeeId, token);
			if (!success)
				TempData["Error"] = "Failed to remove employee.";
			return RedirectToAction(nameof(Details), new { id = projectId });
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using project_staff.Services.WebApi;
using project_staff.Shared.DTOs;
using project_staff.Shared.RequestFeatures;
using System.Security.Claims;

namespace project_staff.WebApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskApiClient _taskApiClient;

        public TasksController(TaskApiClient taskApiClient)
        {
            _taskApiClient = taskApiClient;
        }

        public async Task<IActionResult> Index(Guid projectId, [FromQuery] TaskParameters parameters)
        {
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var pagedTasks = await _taskApiClient.GetTasksAsync(projectId, parameters, token);
            ViewBag.ProjectId = projectId;
            return View(pagedTasks);
        }


        // GET: /Tasks/Details?projectId={projectId}&taskId={taskId}
        public async Task<IActionResult> Details(Guid projectId, Guid taskId)
        {
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var task = await _taskApiClient.GetTaskByIdAsync(projectId, taskId, token);
            if (task == null)
                return NotFound();
            ViewBag.ProjectId = projectId;
            return View(task);
        }

        // GET: /Tasks/Create?projectId={projectId}
        public IActionResult Create(Guid projectId)
        {
            ViewBag.ProjectId = projectId;
            return View();
        }

        // POST: /Tasks/Create?projectId={projectId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid projectId, ProjectTaskForCreationDto taskDto)
        {
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }


            if (!ModelState.IsValid)
            {
                ViewBag.ProjectId = projectId;
                return View(taskDto);
            }

            var success = await _taskApiClient.CreateTaskForProjectAsync(projectId, taskDto, token);
            if (success)
                return RedirectToAction("Index", new { projectId });

            ModelState.AddModelError("", "Failed to create task.");
            ViewBag.ProjectId = projectId;
            return View(taskDto);
        }

        // GET: /Tasks/Edit?projectId={projectId}&taskId={taskId}
        public async Task<IActionResult> Edit(Guid projectId, Guid taskId)
        {
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.ProjectId = projectId;
            ViewBag.TaskId = taskId;

            var task = await _taskApiClient.GetTaskByIdAsync(projectId, taskId, token);
            if (task == null)
                return NotFound();

            var updateDto = new ProjectTaskForUpdateDto
            {
                Name = task.Name,
                Status = task.Status,
                Comment = task.Comment,
                Priority = task.Priority,
            };
            return View(updateDto);
        }

        // POST: /Tasks/Edit?projectId={projectId}&taskId={taskId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid projectId, Guid taskId, ProjectTaskForUpdateDto taskDto)
        {
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ProjectId = projectId;
                return View(taskDto);
            }

            var success = await _taskApiClient.UpdateTaskForProjectAsync(projectId, taskId, taskDto, token);
            if (success)
                return RedirectToAction("Index", new { projectId });


            return View(taskDto);
        }

        // GET: /Tasks/Delete?projectId={projectId}&taskId={taskId}
        public async Task<IActionResult> Delete(Guid projectId, Guid taskId)
        {
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var task = await _taskApiClient.GetTaskByIdAsync(projectId, taskId, token);
            if (task == null)
                return NotFound();
            ViewBag.ProjectId = projectId;
            return View(task);
        }

        // POST: /Tasks/Delete?projectId={projectId}&taskId={taskId}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid projectId, Guid taskId)
        {
            var token = Request.Cookies["JWToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var success = await _taskApiClient.DeleteTaskForProjectAsync(projectId, taskId, token);
            return RedirectToAction("Index", new { projectId });
        }
    }
}

using project_staff.Shared.DTOs;
using project_staff.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace project_staff.Services.WebApi
{
	public class ProjectApiClient
	{
		private readonly HttpClient _httpClient;

		public ProjectApiClient(HttpClient client)
		{
			_httpClient = client;
			// Если нужно, можно добавить DelegatingHandler для автоматической передачи JWT.
		}

		public async Task<IEnumerable<ProjectDto>> GetProjectsAsync(ProjectParameters parameters, string? token)
		{
			// Формируем строку запроса с необходимыми параметрами
			var query = $"api/projects?PageNumber={parameters.PageNumber}&PageSize={parameters.PageSize}&OrderBy={parameters.OrderBy}";
			if (parameters.StartDate != null)
				query += $"&StartDate={parameters.StartDate:O}";
			if (parameters.EndDate != null)
				query += $"&EndDate={parameters.EndDate:O}";
			if (!string.IsNullOrEmpty(parameters.SearchTerm))
				query += $"&SearchTerm={parameters.SearchTerm}";

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.GetAsync(query);

			response.EnsureSuccessStatusCode();

			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

			var pagedData = await response.Content.ReadFromJsonAsync<IEnumerable<ProjectDto>>(options);

			return pagedData;
		}

		public async Task<ProjectDto> GetProjectByIdAsync(Guid projectId, string? token)
		{
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.GetAsync($"api/projects/{projectId}");
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadFromJsonAsync<ProjectDto>();
		}

		public async Task<bool> CreateProjectAsync(ProjectForCreationDto projectDto, string? token)
		{
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.PostAsJsonAsync("api/projects", projectDto);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> UpdateProjectAsync(Guid projectId, ProjectForUpdateDto projectDto, string? token)
		{
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.PutAsJsonAsync($"api/projects/{projectId}", projectDto);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteProjectAsync(Guid projectId, string? token)
		{
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.DeleteAsync($"api/projects/{projectId}");
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> AddEmployeeToProjectAsync(Guid projectId, Guid employeeId, string? token)
		{
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Передаём employeeId как тело запроса (тип Guid, который сериализуется в строку)
            var response = await _httpClient.PostAsJsonAsync($"api/projects/{projectId}/employees", employeeId);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> RemoveEmployeeFromProjectAsync(Guid projectId, Guid employeeId, string? token)
		{
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.DeleteAsync($"api/projects/{projectId}/employees/{employeeId}");
			return response.IsSuccessStatusCode;
		}
	}
}

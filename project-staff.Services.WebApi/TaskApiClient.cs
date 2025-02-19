using project_staff.Shared.DTOs;
using project_staff.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Services.WebApi
{
    public class TaskApiClient
    {
        private readonly HttpClient _httpClient;

        public TaskApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetTasksAsync(Guid projectId, TaskParameters parameters, string? token)
        {
            // Формирование строки запроса с параметрами
            var query = $"api/projects/{projectId}/tasks?PageNumber={parameters.PageNumber}&PageSize={parameters.PageSize}&OrderBy={parameters.OrderBy}";
            if (parameters.Status.HasValue)
                query += $"&Status={(int)parameters.Status}";
            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
                query += $"&SearchTerm={parameters.SearchTerm}";

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.GetAsync(query);
            response.EnsureSuccessStatusCode();
            var pagedTasks = await response.Content.ReadFromJsonAsync<IEnumerable<ProjectTaskDto>>();
            return pagedTasks;
        }


        public async Task<ProjectTaskDto> GetTaskByIdAsync(Guid projectId, Guid taskId, string? token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.GetAsync($"api/projects/{projectId}/tasks/{taskId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProjectTaskDto>();
        }

        public async Task<bool> CreateTaskForProjectAsync(Guid projectId, ProjectTaskForCreationDto taskDto, string? token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.PostAsJsonAsync($"api/projects/{projectId}/tasks", taskDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTaskForProjectAsync(Guid projectId, Guid taskId, ProjectTaskForUpdateDto taskDto, string? token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.PutAsJsonAsync($"api/projects/{projectId}/tasks/{taskId}", taskDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTaskForProjectAsync(Guid projectId, Guid taskId, string? token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.DeleteAsync($"api/projects/{projectId}/tasks/{taskId}");
            return response.IsSuccessStatusCode;
        }
    }
}

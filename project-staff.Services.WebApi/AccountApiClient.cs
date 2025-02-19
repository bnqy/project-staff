using project_staff.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace project_staff.Services.WebApi
{
    public class AccountApiClient
    {
        private readonly HttpClient _httpClient;

        public AccountApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterAsync(ApplicationUserForRegistrationDto registrationDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Account/register", registrationDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> LoginAsync(ApplicationUserForAuthenticationDto authenticationDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Account/login", authenticationDto);
            if (response.IsSuccessStatusCode)
            {
                // Предполагаем, что API возвращает объект { "Token": "..." }
                var result = await response.Content.ReadFromJsonAsync<LoginResult>();
                return result?.Token;
            }
            return null;
        }
    }

    public class LoginResult
    {
        public string Token { get; set; }
    }
}

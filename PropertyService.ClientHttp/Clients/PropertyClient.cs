using PropertyService.ClientHttp.Interfaces;
using PropertyService.Shared.dtos;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace PropertyService.ClientHttp.Clients
{
    public class PropertyClient : IPropertyClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PropertyClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<PropertyDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PropertyDto>>("api/properties") ?? [];
        }

        public async Task<PropertyDto?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PropertyDto>($"api/properties/{id}");
        }


        public async Task AddAsync(CreatePropertyDto property)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/properties");
            request.Content = JsonContent.Create(property);

            AddAuthorizationHeader(request);

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, UpdatePropertyDto property)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"api/properties/{id}");
            request.Content = JsonContent.Create(property);

            AddAuthorizationHeader(request);

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"api/properties/{id}");

            AddAuthorizationHeader(request);

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }


        private void AddAuthorizationHeader(HttpRequestMessage request)
        {
            string authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                string token = authHeader.Substring("Bearer ".Length).Trim();

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
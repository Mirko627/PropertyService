using PropertyService.ClientHttp.Interfaces;
using PropertyService.Shared.dtos;
using System.Net.Http.Json;

namespace PropertyService.ClientHttp.Clients
{
    public class PropertyClient : IPropertyClient
    {
        private readonly HttpClient _httpClient;
        public PropertyClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<PropertyDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PropertyDto>>("api/property") ?? [];
        }

        public async Task<PropertyDto?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PropertyDto>($"api/property/{id}");
        }

        public async Task AddAsync(CreatePropertyDto property)
        {
            var response = await _httpClient.PostAsJsonAsync("api/property", property);
            response.EnsureSuccessStatusCode(); 
        }

        public async Task UpdateAsync(int id, UpdatePropertyDto property)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/property/{id}", property);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/property/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}

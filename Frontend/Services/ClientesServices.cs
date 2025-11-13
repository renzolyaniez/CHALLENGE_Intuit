using Frontend.Models;

namespace Frontend.Services
{
    public class ClientesService
    {
        private readonly HttpClient _httpClient;

        public ClientesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://LocalHost:44383/Clientes"); // tu API
        }

        public async Task<IEnumerable<ClienteViewModel>> GetClientesAsync()
            => await _httpClient.GetFromJsonAsync<IEnumerable<ClienteViewModel>>("clientes");

        public async Task<ClienteViewModel> GetClienteByIdAsync(int id)
            => await _httpClient.GetFromJsonAsync<ClienteViewModel>($"clientes/{id}");

        public async Task<bool> CreateClienteAsync(ClienteViewModel cliente)
        {
            var response = await _httpClient.PostAsJsonAsync("clientes", cliente);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClienteAsync(int id, ClienteViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync("clientes", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"clientes/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
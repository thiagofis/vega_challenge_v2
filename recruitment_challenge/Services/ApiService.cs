using recruitment_challenge.Classes;
using recruitment_challenge.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace recruitment_challenge.Services
{
    internal class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(string baseAddress)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "challenge");
        }

        public async Task<string> AuthenticateAsync(string login, string password)
        {
            try
            {
                var authPayload = JsonSerializer.Serialize(new { login, password });
                var authRequest = new HttpRequestMessage(HttpMethod.Post, "Authentication/login")
                {
                    Content = new StringContent(authPayload, Encoding.UTF8, "application/json"),
                    Headers =
                    {
                        { "TraceId", "challenge-auth" }
                    }
                };

                var authResponse = await _httpClient.SendAsync(authRequest);
                authResponse.EnsureSuccessStatusCode(); 
                return await authResponse.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro na requisição HTTP: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                throw;
            }
        }

        public async Task<string> GetUnitsAsync(string token)
        {
            try
            {
                var unitRequest = new HttpRequestMessage(HttpMethod.Get, "/Unit/list")
                {
                    Headers =
                    {
                        { "TraceId", "challenge-get-units" }
                    }
                };
                unitRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var unitResponse = await _httpClient.SendAsync(unitRequest);
                unitResponse.EnsureSuccessStatusCode();

                var units = await unitResponse.Content.ReadFromJsonAsync<UnitCollection>();

                var dic = units.Items.GroupBy(x => x.UnitGroupId).ToDictionary(
                    x => x.Key,
                    y => y.ToList().Select(z => new { unit = z.Name, location = string.IsNullOrWhiteSpace(z.AddressInformation) ? null : z.AddressInformation })
                );

                return JsonSerializer.Serialize(
                    dic,
                    options: new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    });
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro na requisição HTTP: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                throw;
            }
        }
    }
}

// See https://aka.ms/new-console-template for more information

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var client = new HttpClient();

client.BaseAddress = new Uri("https://dev.feedback-v3.api.vegait.com/");

var authPayload = JsonSerializer.Serialize(new { login = "candidate", password = "Senha1234" });
var authRequest = new HttpRequestMessage(HttpMethod.Post, "Authentication/login")
{
    Content = new StringContent(authPayload, Encoding.UTF8, "application/json"),
    Headers =
    {
        {"User-Agent", "challenge"},
        {"TraceId", "challenge-auth"}
    }
};

var authResponse = await client.SendAsync(authRequest);
var token = await authResponse.Content.ReadAsStringAsync();

var unitRequest = new HttpRequestMessage(HttpMethod.Get, "/Unit/list")
{
    Headers =
    {
        {"User-Agent", "challenge"},
        {"TraceId", "challenge-get-units"}
    }
};
unitRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
var unitResponse = await client.SendAsync(unitRequest);
var units = await unitResponse.Content.ReadFromJsonAsync<UnitCollection>();

var dic = units.Items.GroupBy(x => x.UnitGroupId).ToDictionary(x => x.Key,
    y => y.ToList().Select(z => new { unit = z.Name, location = string.IsNullOrWhiteSpace(z.AddressInformation) ? null : z.AddressInformation }));

var json = JsonSerializer.Serialize(dic, options: new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull});


Console.WriteLine("Hello, World!");


class UnitCollection
{
    public List<Unit> Items { get; init; }
}

class Unit
{
    public Guid UnitGroupId { get; init; }
    public string Name { get; init; }
    public string AddressInformation { get; init; }
    
}

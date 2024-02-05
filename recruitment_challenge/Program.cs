// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using recruitment_challenge.Interfaces;
using recruitment_challenge.Services;

var serviceProvider = new ServiceCollection()
            .AddScoped<IApiService, ApiService>(provider => new ApiService("https://dev.feedback-v3.api.vegait.com/"))
            .BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var apiService = scope.ServiceProvider.GetRequiredService<IApiService>();

    var token = apiService.AuthenticateAsync("candidate", "Senha1234").Result;
    var resultJson = apiService.GetUnitsAsync(token).Result;

    Console.WriteLine();
    Console.WriteLine(resultJson);
    Console.ReadKey();
}
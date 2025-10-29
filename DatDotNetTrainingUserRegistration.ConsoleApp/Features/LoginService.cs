using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DatDotNetTrainingUserRegistration.ConsoleApp.Features;

public class LoginService
{
    public async Task Login()
    {
        Console.Write("\nEnter Username: ");
        var username = Console.ReadLine();

        Console.Write("Enter Password: ");
        var password = Console.ReadLine();

        var loginRequest = new LoginRequestDto
        {
            UserName = username,
            Password = password
        };

        using var client = new HttpClient();
        client.BaseAddress = new Uri(ApiUrls.DomainUrl);

        try
        {
            var response = await client.PostAsJsonAsync(ApiUrls.LoginApi, loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                Console.WriteLine($"\n✅ {result.Message}");
                Console.WriteLine($"UserId: {result.UserId}");
                Console.WriteLine($"SessionId: {result.SessionId}");
            }
            else
            {
                var error = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                Console.WriteLine($"\n❌ {error?.Message ?? "Login failed."}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n🚨 Error: {ex.Message}");
        }
    }
}

public class LoginRequestDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class LoginResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public Guid UserId { get; set; }
    public Guid SessionId { get; set; }
}

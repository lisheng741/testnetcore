using System.ComponentModel;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace HttpClientTest.Code;

public class SimpleAppService : ISimpleAppService
{
    private readonly HttpClient _httpClient;

    public SimpleAppService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("http://175.178.244.200:12060/");
    }

    public async Task LoginAsync()
    {
        var loginModel = new
        {
            Account = "superAdmin",
            Password = "123456"
        };
        var content = new StringContent(
            JsonSerializer.Serialize(loginModel),
            Encoding.UTF8,
            Application.Json);
        //using var response = await _httpClient.PostAsync("/api/account/login", content);
        using var response = await _httpClient.PostAsJsonAsync("/api/account/login", loginModel);
        response.EnsureSuccessStatusCode();

        var headers = response.Content.Headers;
        var result = await response.Content.ReadFromJsonAsync<LoginResultModel>();
    }
}

public record LoginResultModel(int Code, string Message, string Data, bool Success);

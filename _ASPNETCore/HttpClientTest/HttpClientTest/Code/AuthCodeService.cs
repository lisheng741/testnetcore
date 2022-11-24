using System.IO.Pipelines;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;
using System.Net;
using Microsoft.Extensions.ObjectPool;

namespace HttpClientTest.Code;

public class AuthCodeService
{
    private readonly HttpClient _httpClient;
	static List<string> Cookies { get; set; } = new List<string>();

	public AuthCodeService(HttpClient httpClient)
	{
		_httpClient= httpClient;

		_httpClient.BaseAddress = new Uri("https://www.qb5.tw");
		_httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36 Edg/107.0.1418.35");
		_httpClient.DefaultRequestHeaders.Add("Referer", "https://www.qb5.tw");
	}

	public async Task GetAuthCodeAsync()
	{
		using var response = await _httpClient.GetAsync("/checkcode.php");
		response.EnsureSuccessStatusCode();

		Cookies.AddRange(response.Headers.GetValues("Set-Cookie"));

		using var stream = await response.Content.ReadAsStreamAsync();
		using var fs = File.Create("authcode.png");
		await stream.CopyToAsync(fs);
		fs.Flush();
	}

	public async Task LoginAsync(string code)
	{
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var loginModel = new LoginModel("test642577006", "123456", "315360000", code, "login", "%C1%A2%BC%B4%B5%C7%C2%BC");

		foreach(var cookie in Cookies)
		{
            _httpClient.DefaultRequestHeaders.Add("Cookie", cookie);
        }

        //var content = new StringContent(
        //    JsonSerializer.Serialize(loginModel),
        //    Encoding.UTF8,
        //    Application.Json);
        var content = new FormUrlEncodedContent(GetParamList(loginModel));

        using var response = await _httpClient.PostAsync("/login.php?do=submit", content);
        response.EnsureSuccessStatusCode();

        Cookies.AddRange(response.Headers.GetValues("Set-Cookie"));

        var headers = response.Content.Headers;
        var bytes = await response.Content.ReadAsByteArrayAsync();
        var result = Encoding.GetEncoding("GB2312").GetString(bytes);

        //var result = await response.Content.ReadAsStringAsync();
    }

	public async Task<string> GetInfoAsync()
    {
        foreach (var cookie in Cookies)
        {
            _httpClient.DefaultRequestHeaders.Add("Cookie", cookie);
        }

        var response = await _httpClient.GetAsync("/userdetail.php");
		response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
		return result;
    }

    //通过反射，将 objec 转为 KeyValuePair
    private List<KeyValuePair<string, string>> GetParamList(object data)
    {
        List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>();
        Type t = data.GetType();
        var properties = t.GetProperties().Where(t => t.CanRead).ToArray();
        for (int i = 0; i < properties.Length; i++)
        {
            KeyValuePair<string, string> key = new KeyValuePair<string, string>(properties[i].Name, properties[i].GetValue(data).ToString());
            paramList.Add(key);
        }
        return paramList;
    }
}

public record LoginModel(string username, string password, string usecookie, string checkcode, string action, string submit);

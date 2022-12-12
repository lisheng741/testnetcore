using HtmlAgilityPack;

HttpClient client = new HttpClient();
client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36");

var url = "https://www.jianshu.com/";
using var response = await client.GetAsync(url);
var document = new HtmlDocument();
using var stream = await response.Content.ReadAsStreamAsync();
document.Load(stream);

var meta = document.DocumentNode.SelectSingleNode("/html/head/meta[1]");
var charset = meta.GetAttributeValue("charset", "");

Console.WriteLine(charset);

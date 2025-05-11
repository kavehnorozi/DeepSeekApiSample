using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

class Program
{
    private static readonly string apiKey = "YOUR_DEEPSEEK_API_KEY"; // جایگزین کن
    private static readonly string apiUrl = "https://api.deepseek.com/v1/chat/completions"; 

    static async Task Main(string[] args)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "deepseek-chat",  // بسته به مدل واقعی
                messages = new[]
                {
                    new { role = "user", content = "سلام دیپ سیک!" }
                },
                temperature = 0.7
            };

            var json = System.Text.Json.JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                Console.WriteLine("✅ پاسخ از DeepSeek:");
                Console.WriteLine(responseText);
            }
            else
            {
                Console.WriteLine($"❌ خطا: {response.StatusCode}");
                var errorText = await response.Content.ReadAsStringAsync();
                Console.WriteLine(errorText);
            }
        }
    }
}

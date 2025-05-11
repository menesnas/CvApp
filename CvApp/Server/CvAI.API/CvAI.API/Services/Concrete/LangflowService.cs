using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CvAI.API.Services.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CvAI.API.Services.Concrete
{
    public class LangflowService : ILangflowService
    {
        private readonly HttpClient _httpClient;
        private const string langflowToken = "token";
        private const string langflowLink = "link";
        public LangflowService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendCvAsync(string extractedText)
        {
            var payload = new
            {
                input_value = extractedText,
                output_type = "chat",
                input_type = "text",
            };

            var json = JsonSerializer.Serialize(payload);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {langflowToken}");

            var response = await _httpClient.PostAsync(
                langflowLink,
                content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return "error";
            }
            var langflowResult = await response.Content.ReadAsStringAsync();

            return langflowResult;
        }
    }
}
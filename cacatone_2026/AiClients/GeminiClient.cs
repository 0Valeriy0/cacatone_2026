using Domain.Models; // Для доступу до Question
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace cacatone_2026.AiClients
{
    public class GeminiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public GeminiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Gemini:ApiKey"]
                      ?? throw new InvalidOperationException("Gemini API key is missing in appsettings.json");
        }
        public async Task<string> GenerateTextAsync(string prompt)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.5-flash:generateContent?key={_apiKey}";
            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");
            var response = await _httpClient.PostAsync(url, jsonContent);
            var responseString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Gemini API Error: {responseString}");
            }
            // Дістаємо згенеровану відповідь із JSON
            using var doc = JsonDocument.Parse(responseString);
            var result = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();
            return result ?? string.Empty;
        }

        public async Task<string> AnalyzeSelectedAnswerAsync(Question question, List<string> characteristics)
        {
            // 1. Формуємо список УСІХ варіантів, які бачив користувач
            string allOptions = (question.AllAnswers != null && question.AllAnswers.Any())
                ? string.Join("\n", question.AllAnswers.Select((a, i) => $"{i + 1}. {a.Text}"))
                : "Варіанти не вказані";
            // 2. Формуємо список варіантів, які користувач ОБРАВ
            string selectedOptions = (question.SelectedAnswers != null && question.SelectedAnswers.Any())
                ? string.Join("\n", question.SelectedAnswers.Select(a => $"✔ {a.Text}"))
                : "Користувач нічого не обрав";
            // 3. Список характеристик
            string characteristicsStr = string.Join(", ", characteristics);
            // 4. Промпт для ШІ
            string prompt = $@"
Ти — експертна система для аналізу нелінійного тестування на базі ШІ.
ОЦІНЮВАНІ ХАРАКТЕРИСТИКИ:
{characteristicsStr}
ЗАПИТАННЯ:
""{question.Text}""
УСІ ДОСТУПНІ ВАРІАНТИ ВІДПОВІДЕЙ:
{allOptions}
КОРИСТУВАЧ ОБРАВ:
{selectedOptions}
ЗАВДАННЯ:
1. Проаналізуй, чому саме цей вибір серед запропонованих альтернатив є показовим.
2. Оціни кожну з характеристик: чи підсилює її цей вибір, послаблює, чи є нейтральним.
3. Зроби короткий висновок (2-3 речення) про психологічний/професійний профіль респондента.";
            return await GenerateTextAsync(prompt);
        }


    }
}


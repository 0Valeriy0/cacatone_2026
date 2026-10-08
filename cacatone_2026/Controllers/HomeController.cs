using System;
using cacatone_2026.AiClients;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cacatone_2026.Controllers
{
    [Route("")]
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly GeminiClient _geminiClient;
        public HomeController(GeminiClient geminiClient)
        {
            _geminiClient = geminiClient;
        }


        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("question")]
        public IActionResult Question()
        {
            return View();
        }

        [HttpGet("result")]
        public IActionResult Result()
        {
            return View();
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("drafts")]
        public IActionResult Drafts()
        {
            return View();
        }

        [HttpGet("published")]
        public IActionResult Published()
        {
            return View();
        }

        [HttpGet("all")]
        public IActionResult All()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }
    


        // Тестовий метод для перевірки ШІ
        [HttpGet("test-ai")]
        public async Task<IActionResult> TestAi()
        {
            var characteristics = new List<string> { "Лідерство", "Стресостійкість", "Вміння делегувати" };
            var question = new Question
            {
                Text = "Проєкт відстає від графіка на тиждень через помилку колеги. Що ви зробите?",
                AllAnswers = new List<Answer>
                {
                    new Answer { Text = "Буду працювати ночами сам, щоб врятувати реліз" },
                    new Answer { Text = "Розподілю залишок задач між командою і допоможу колезі розібратися" },
                    new Answer { Text = "Скажу тімліду, що це не моя провина" }
                },
                SelectedAnswers = new List<Answer>
                {
                    new Answer { Text = "Розподілю залишок задач між командою і допоможу колезі розібратися" }
                }
            };
            string result = await _geminiClient.AnalyzeSelectedAnswerAsync(question, characteristics);
            return Content(result, "text/plain; charset=utf-8");
        }

        // Головний API-ендпоінт: аналіз виборів тесту через Gemini
        [HttpPost("api/analyze-test")]
        public async Task<IActionResult> AnalyzeTest([FromBody] AnalyzeRequestDto request)
        {
            if (request == null || request.Answers == null || !request.Answers.Any())
            {
                return BadRequest(new { error = "Немає відповідей для аналізу" });
            }

            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < request.Answers.Count; i++)
            {
                var a = request.Answers[i];
                sb.AppendLine($"Кейс {i + 1} [{a.Category}]: \"{a.Question}\"");
                sb.AppendLine($"-> Обраний вибір: \"{a.ChosenAnswer}\"");
                sb.AppendLine();
            }

            string prompt = $@"
Ти — експертна AI-система багатовимірного профілювання SmartTest.
Користувач щойно завершив ситуаційний тест із дилемами:

{sb}

ЗАВДАННЯ:
1. Проаналізуй обрані поведінкові стратегії респондента та скомпонуй глибокий, індивідуалізований AI-вердикт.
2. ОБЧИСЛИ ДИНАМІЧНІ ПОКАЗНИКИ НА ОСНОВІ ВИБОРІВ:
   - ""accuracy"": розрахуй статистичну точність вердикту та індекс узгодженості виборів як число з одним десятковим знаком у діапазоні від 92.0 до 99.4 (наприклад 94.6, 96.2, 97.4, 98.8). ЗАБОРОНЕНО ставити завжди однакове число на зразок 98.4! Воно повинно відображати реальну консистентність відповідей користувача.
   - ""score"": інтегральний рівень зрілості та лідерської мудрості в кризі (ціле число від 72 до 98).
   - ""logic"": відсоток логіки та системної архітектури (від 50 до 98) відповідно до відповідей.
   - ""resilience"": відсоток стресостійкості та витримки (від 50 до 98).
   - ""empathy"": відсоток емпатії та психологічної безпеки (від 50 до 98).
   - ""leadership"": відсоток кризового лідерства (від 50 до 98).
   - ""adaptability"": відсоток адаптивності до змін (від 50 до 98).
   Якщо користувач обирав емпатичні варіанти — емпатія має бути високою (90-98%), а якщо виключно технічні або системні — домінувати мають логіка та архітектура.

Поверни ВІДПОВІДЬ ВИКЛЮЧНО ЯК ЧИСТИЙ ВАЛІДНИЙ JSON без markdown-блоків (без ```json, строго тільки валідний JSON-об'єкт) за такою структурою:
{{
  ""archetype"": ""Індивідуальний домінантний архетип українською (наприклад, 'Стратегічний Архітектор', 'Кризовий Дипломат', 'Системний Реалізатор', 'Емпатичний Ментор', 'Трансформаційний Лідер')"",
  ""score"": 91,
  ""accuracy"": 96.7,
  ""quote"": ""Глибокий психологічний портрет (2-3 змістовні речення про стиль мислення та дій саме цього користувача)..."",
  ""logic"": 88,
  ""resilience"": 84,
  ""empathy"": 92,
  ""leadership"": 86,
  ""adaptability"": 80,
  ""radarInsight"": ""Аналітичний висновок щодо контуру радарної діаграми (1-2 речення: баланс осей, точки максимальної сили та вектор розвитку)..."",
  ""superpowers"": [
    ""Суперсила 1 (конкретно)"",
    ""Суперсила 2 (конкретно)"",
    ""Суперсила 3 (конкретно)""
  ],
  ""growthZones"": [
    ""Точка росту 1 (практична порада)"",
    ""Точка росту 2 (практична порада)""
  ],
  ""recommendedRoles"": [
    ""Роль 1"",
    ""Роль 2"",
    ""Роль 3""
  ],
  ""teamSynergy"": {{
    ""idealPartner"": ""Назва партнера — коротке пояснення синергії"",
    ""creativeUnion"": ""Назва союзу — як взаємодіють"",
    ""frictionZone"": ""Зона тертя — де виникає напруга та як її знімати""
  }}
}}";

            try
            {
                string rawJson = await _geminiClient.GenerateTextAsync(prompt);
                string cleaned = CleanJson(rawJson);
                return Content(cleaned, "application/json; charset=utf-8");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Генерація повної матриці компетенцій та кейсів через Gemini
        [HttpPost("api/generate-matrix")]
        public async Task<IActionResult> GenerateMatrix([FromBody] GenerateMatrixRequestDto request)
        {
            string topic = string.IsNullOrWhiteSpace(request?.Topic)
                ? "Оцінка лідерського потенціалу в IT-стартапах"
                : request.Topic.Trim();

            int count = request?.CaseCount > 0 ? System.Math.Clamp(request.CaseCount, 3, 6) : 4;

            string prompt = $@"
Ти — провідний психометрист, організаційний психолог та творець матриць нелінійного тестування платформи SmartTest.
ТВОЄ ЗАВДАННЯ: Створити комплексну, глибоку та практичну авторську матрицю оцінювання на тему: ""{topic}"".
{(string.IsNullOrWhiteSpace(request?.Context) ? "" : $"Контекст/Побажання: {request.Context}")}

ВИМОГИ ДО СТРУКТУРИ:
1. Осі матриці (axes): рівно 5 професійних осей / компетенцій, специфічних для цієї теми (наприклад: 'Емпатія & Довіра', 'Системна архітектура', 'Кризове лідерство', 'Адаптивність', 'Стресостійкість').
2. Ситуаційні кейси (cases): рівно {count} глибоких життєвих дилем.
   У дилемах не повинно бути банальних, очевидно неправильних чи 'токсичних' варіантів! Всі варіанти повинні бути зрілими альтернативами дій у стані невизначеності.
3. Кожен кейс містить:
   - id: порядковий номер (1..{count})
   - title: гостре формулювання дилеми (питання: 'Що ви зробите?')
   - context: реалістичний опис напруги ситуації (2-3 речення)
   - category: назва категорії / компетенції
   - options: рівно 4 альтернативи (A, B, C, D)
     Кожна альтернатива містить:
       - letter: 'A', 'B', 'C', або 'D'
       - text: змістовна, конкретна стратегія поведінки (1-2 речення)
       - archetype: мікро-архетип респондента при такому виборі
       - vectors: масив з 2 осей із балами впливу (points від 35 до 55), наприклад: [{{ ""axis"": ""Кризове лідерство"", ""points"": 48 }}, {{ ""axis"": ""Адаптивність"", ""points"": 40 }}]
       - stats: об'єкт із відсотками {{ ""leadership"": ""82%"", ""logic"": ""75%"", ""adaptability"": ""88%"", ""empathy"": ""70%"" }}

Поверни ВІДПОВІДЬ ВИКЛЮЧНО ЯК ЧИСТИЙ ВАЛІДНИЙ JSON без markdown-блоків (без ```json, строго один JSON об'єкт):
{{
  ""title"": ""{topic}"",
  ""description"": ""Короткий влучний опис мети цієї матриці профілювання..."",
  ""axes"": [""Вісь 1"", ""Вісь 2"", ""Вісь 3"", ""Вісь 4"", ""Вісь 5""],
  ""cases"": [
    {{
      ""id"": 1,
      ""title"": ""Формулювання дилеми?"",
      ""context"": ""Опис контексту дилеми..."",
      ""category"": ""Категорія"",
      ""options"": [
        {{
          ""letter"": ""A"",
          ""text"": ""Опис стратегії дії A..."",
          ""archetype"": ""Назва архетипу"",
          ""vectors"": [
            {{ ""axis"": ""Вісь 1"", ""points"": 45 }},
            {{ ""axis"": ""Вісь 2"", ""points"": 38 }}
          ],
          ""stats"": {{
            ""leadership"": ""75%"",
            ""logic"": ""80%"",
            ""adaptability"": ""70%"",
            ""empathy"": ""90%""
          }}
        }},
        {{
          ""letter"": ""B"",
          ""text"": ""Опис стратегії дії B..."",
          ""archetype"": ""Назва архетипу"",
          ""vectors"": [
            {{ ""axis"": ""Вісь 3"", ""points"": 50 }},
            {{ ""axis"": ""Вісь 4"", ""points"": 42 }}
          ],
          ""stats"": {{
            ""leadership"": ""88%"",
            ""logic"": ""85%"",
            ""adaptability"": ""75%"",
            ""empathy"": ""65%""
          }}
        }},
        {{
          ""letter"": ""C"",
          ""text"": ""Опис стратегії дії C..."",
          ""archetype"": ""Назва архетипу"",
          ""vectors"": [
            {{ ""axis"": ""Вісь 1"", ""points"": 48 }},
            {{ ""axis"": ""Вісь 3"", ""points"": 40 }}
          ],
          ""stats"": {{
            ""leadership"": ""70%"",
            ""logic"": ""92%"",
            ""adaptability"": ""80%"",
            ""empathy"": ""60%""
          }}
        }},
        {{
          ""letter"": ""D"",
          ""text"": ""Опис стратегії дії D..."",
          ""archetype"": ""Назва архетипу"",
          ""vectors"": [
            {{ ""axis"": ""Вісь 2"", ""points"": 44 }},
            {{ ""axis"": ""Вісь 5"", ""points"": 46 }}
          ],
          ""stats"": {{
            ""leadership"": ""82%"",
            ""logic"": ""70%"",
            ""adaptability"": ""90%"",
            ""empathy"": ""78%""
          }}
        }}
      ]
    }}
  ]
}}";

            try
            {
                string raw = await _geminiClient.GenerateTextAsync(prompt);
                string cleaned = CleanJson(raw);
                return Content(cleaned, "application/json; charset=utf-8");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Генерація одного додаткового кейсу до поточної матриці
        [HttpPost("api/generate-single-case")]
        public async Task<IActionResult> GenerateSingleCase([FromBody] GenerateCaseRequestDto request)
        {
            string topic = string.IsNullOrWhiteSpace(request?.Topic)
                ? "Оцінка стилю прийняття рішень"
                : request.Topic.Trim();

            var axesList = request?.Axes != null && request.Axes.Any()
                ? request.Axes
                : new List<string> { "Кризове лідерство", "Логіка", "Емпатія", "Адаптивність", "Стресостійкість" };

            int caseNumber = request?.CaseNumber > 0 ? request.CaseNumber : 2;

            string prompt = $@"
Ти — провідний психометрист платформи SmartTest.
Створи ОДИН новий унікальний ситуаційний кейс (дилему) №{caseNumber} для матриці ""{topic}"".
Осі вимірювання: {string.Join(", ", axesList)}.

Вимоги:
- Без очевидно правильних чи неправильних відповідей.
- Чітка виробнича чи професійна напруга.
- 4 реалістичні зрілі стратегії дій (A, B, C, D).
- Кожна стратегія має letter, text, archetype, vectors (2 осі з вказаних вище та points від 35 до 55), stats (leadership, logic, adaptability, empathy у відсотках).

Поверни ВІДПОВІДЬ ВИКЛЮЧНО ЯК ЧИСТИЙ ВАЛІДНИЙ JSON без markdown-блоків (строго один JSON об'єкт кейсу):
{{
  ""id"": {caseNumber},
  ""title"": ""Формулювання дилеми?"",
  ""context"": ""Опис контексту дилеми (2-3 речення)..."",
  ""category"": ""Ситуаційний вибір"",
  ""options"": [
    {{
      ""letter"": ""A"",
      ""text"": ""Варіант дії A..."",
      ""archetype"": ""Архетип"",
      ""vectors"": [
        {{ ""axis"": ""{axesList[0]}"", ""points"": 45 }},
        {{ ""axis"": ""{axesList[1 % axesList.Count]}"", ""points"": 38 }}
      ],
      ""stats"": {{ ""leadership"": ""75%"", ""logic"": ""80%"", ""adaptability"": ""70%"", ""empathy"": ""90%"" }}
    }},
    {{
      ""letter"": ""B"",
      ""text"": ""Варіант дії B..."",
      ""archetype"": ""Архетип"",
      ""vectors"": [
        {{ ""axis"": ""{axesList[2 % axesList.Count]}"", ""points"": 50 }},
        {{ ""axis"": ""{axesList[3 % axesList.Count]}"", ""points"": 42 }}
      ],
      ""stats"": {{ ""leadership"": ""88%"", ""logic"": ""85%"", ""adaptability"": ""75%"", ""empathy"": ""65%"" }}
    }},
    {{
      ""letter"": ""C"",
      ""text"": ""Варіант дії C..."",
      ""archetype"": ""Архетип"",
      ""vectors"": [
        {{ ""axis"": ""{axesList[0]}"", ""points"": 48 }},
        {{ ""axis"": ""{axesList[2 % axesList.Count]}"", ""points"": 40 }}
      ],
      ""stats"": {{ ""leadership"": ""70%"", ""logic"": ""92%"", ""adaptability"": ""80%"", ""empathy"": ""60%"" }}
    }},
    {{
      ""letter"": ""D"",
      ""text"": ""Варіант дії D..."",
      ""archetype"": ""Архетип"",
      ""vectors"": [
        {{ ""axis"": ""{axesList[1 % axesList.Count]}"", ""points"": 44 }},
        {{ ""axis"": ""{axesList[Math.Min(4, axesList.Count - 1)]}"", ""points"": 46 }}
      ],
      ""stats"": {{ ""leadership"": ""82%"", ""logic"": ""70%"", ""adaptability"": ""90%"", ""empathy"": ""78%"" }}
    }}
  ]
}}";

            try
            {
                string raw = await _geminiClient.GenerateTextAsync(prompt);
                string cleaned = CleanJson(raw);
                return Content(cleaned, "application/json; charset=utf-8");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Допоміжний метод очищення JSON від маркдауну та зайвого тексту
        private static string CleanJson(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "{}";
            text = text.Trim();

            int jsonStart = text.IndexOf("```json", System.StringComparison.OrdinalIgnoreCase);
            if (jsonStart != -1)
            {
                int contentStart = jsonStart + 7;
                int jsonEnd = text.IndexOf("```", contentStart, System.StringComparison.OrdinalIgnoreCase);
                if (jsonEnd != -1)
                {
                    return text.Substring(contentStart, jsonEnd - contentStart).Trim();
                }
            }
            else if (text.StartsWith("```"))
            {
                int jsonEnd = text.LastIndexOf("```");
                if (jsonEnd > 3)
                {
                    return text.Substring(3, jsonEnd - 3).Trim();
                }
            }

            int firstBrace = text.IndexOf('{');
            int lastBrace = text.LastIndexOf('}');
            if (firstBrace != -1 && lastBrace != -1 && lastBrace > firstBrace)
            {
                return text.Substring(firstBrace, lastBrace - firstBrace + 1).Trim();
            }

            return text.Trim();
        }
    }

    public class UserDecisionDto
    {
        public string Category { get; set; } = string.Empty;
        public string Question { get; set; } = string.Empty;
        public string ChosenAnswer { get; set; } = string.Empty;
    }

    public class AnalyzeRequestDto
    {
        public List<UserDecisionDto> Answers { get; set; } = new();
    }

    public class GenerateMatrixRequestDto
    {
        public string Topic { get; set; } = string.Empty;
        public string Context { get; set; } = string.Empty;
        public int CaseCount { get; set; } = 4;
    }

    public class GenerateCaseRequestDto
    {
        public string Topic { get; set; } = string.Empty;
        public List<string> Axes { get; set; } = new();
        public int CaseNumber { get; set; } = 2;
    }
}
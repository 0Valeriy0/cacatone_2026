using cacatone_2026.AiClients;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
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
    


     // Тестовий метод для перевірки ШІ
        [HttpGet("test-ai")]
        public async Task<IActionResult> TestAi()
        {
    
                        // 1. Характеристики, які перевіряємо
                        var characteristics = new List<string>
                         {
                    "Лідерство",
                    "Стресостійкість",
                    "Вміння делегувати"
                         };
                        // 2. Тестове запитання та варіанти
                        var question = new Question
                        {
                            Text = "Проєкт відстає від графіка на тиждень через помилку колеги. Що ви зробите?",
                            AllAnswers = new List<Answer>
                    {
                        new Answer { Text = "Буду працювати ночами сам, щоб врятувати реліз" },
                        new Answer { Text = "Розподілю залишок задач між командою і допоможу колезі розібратися" },
                        new Answer { Text = "Скажу тімліду, що це не моя провина" }
                    },
                            // Користувач обирає другий варіант:
                            SelectedAnswers = new List<Answer>
                    {
                        new Answer { Text = "Розподілю залишок задач між командою і допоможу колезі розібратися" }
                    }
                        };
                        // 3. Викликаємо наш новий метод
                        string result = await _geminiClient.AnalyzeSelectedAnswerAsync(question, characteristics);
                        return Content(result, "text/plain; charset=utf-8");
        }
    }
}
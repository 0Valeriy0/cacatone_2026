using System.Collections.Generic;
using System.Threading.Tasks;
using DLL.Repositories;
using Domain.Models;

namespace BLL.Services
{
    public class Service
    {
        Repository _context;

        public Service(Repository repository)
        {
            _context = repository;
        }
        public async Task CreateTestAsync(Test test)
        {
            await _context.AddTestAsync(test);
        }

        public async Task AddQuestionToTestAsync(Question question, Test test)
        {
            await _context.AddQuestionAsync(question, test);
        }

        public async Task AddAnswerToQuestionAsync(Answer answer, Question question)
        {
            await _context.AddAnswerAsync(answer, question);
        }

        public async Task SubmitTestResultAsync(TestResult testResult, Test test)
        {
            await _context.AddTestResultAsync(testResult, test);
        }

        public async Task SelectAnswersAsync(List<Answer> answers, Question question)
        {
            await _context.AddSelectedAnswerAsync(answers, question);
        }
        public async Task<Test> GetTestByIdAsync(int id)
        {
            return await _context.GetTestAsync(id);
        }

        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            return await _context.GetQuestionAsync(id);
        }

        public async Task<Answer> GetAnswerByIdAsync(int id)
        {
            return await _context.GetAnswerAsync(id);
        }

        public async Task<List<Test>> GetAllTestsAsync()
        {
            return await _context.GetTestsAsync();
        }

        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            return await _context.GetQuestionsAsync();
        }

        public async Task<List<Answer>> GetAllAnswersAsync()
        {
            return await _context.GetAnswersAsync();
        }
    }
}


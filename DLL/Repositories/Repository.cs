using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DLL.Context;
using Domain.Models;
namespace DLL.Repositories
{
    public class Repository
    {
        TestContext _context;
        public Repository(TestContext context)
        {
            _context = context;
        }
        public async Task AddQuestionAsync(Question question, Test test)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }
        public async Task AddTestAsync(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
        }
        public async Task AddAnswerAsync(Answer answer, Question question)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }
        public async Task AddTestResultAsync(TestResult testResult, Test test)
        {
            _context.TestResults.Add(testResult);
            await _context.SaveChangesAsync();
        }
    }
}

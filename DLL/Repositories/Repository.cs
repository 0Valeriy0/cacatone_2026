using System;
using System.Collections.Generic;
using System.Linq;
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
            test.Questions.Add(question);
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
            question.AllAnswers.Add(answer);
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }
        public async Task AddTestResultAsync(TestResult testResult, Test test)
        {
            test.TestResult = testResult;
            _context.TestResults.Add(testResult);
            await _context.SaveChangesAsync();
        }
    }
}

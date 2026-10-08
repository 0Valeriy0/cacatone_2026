using System;
using System.Collections.Generic;
using System.Text;
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
        public Task AddQuestionAsync(Question question, Test test)
        {
            _context.Questions.Add(question);
            return _context.SaveChangesAsync();
        }
        public Task AddTestAsync(Test test)
        {
            _context.Tests.Add(test);
            return _context.SaveChangesAsync();
        }
        public Task AddAnswerAsync(Answer answer, Question question)
        {
            _context.Answers.Add(answer);
            return _context.SaveChangesAsync();
        }
        public Task AddTestResultAsync(TestResult testResult, Test test)
        {
            _context.TestResults.Add(testResult);
            return _context.SaveChangesAsync();
        }
    }
}

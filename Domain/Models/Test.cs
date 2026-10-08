using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Question> Questions { get; set; }

        public TestResult? TestResult { get; set; }
    }
}

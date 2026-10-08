using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public Test Test { get; set; }
        public string OverResult { get; set; }
        public string DetailDescription { get; set; }
    }
}

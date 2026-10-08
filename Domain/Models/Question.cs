using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public bool IsMultipleChoice { get; set; }
        public bool IsHaveOpenAnswer { get; set; }
        public string? OpenAnswer { get; set; }
        public string Text { get; set; }
        public List<Answer> AllAnswers { get; set; }
        public List<Answer> SelectedAnswers { get; set; }

    }
}

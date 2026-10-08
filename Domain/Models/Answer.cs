using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Answer

    {
        public int Id { get; set; }
        public Question Question { get; set; }
        public string Text { get; set; }
    }
}

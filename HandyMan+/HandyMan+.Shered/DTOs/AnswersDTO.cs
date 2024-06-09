using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan_.Shered.DTOs
{
    public class AnswersDTO
    {
        public int Id { get; set; }
        public string QuestionType { get; set; } = null!;
        public List<string> SelectedOptions { get; set; } = new();
        public int StarRating { get; set; }
        public string Comment { get; set; } = null!;
        public bool BooleanAnswer { get; set; } = false;
        public string UserType { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

    }
}

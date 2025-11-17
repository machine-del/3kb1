using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TestingPlatform.Domain.Enums;
using TestingPlatform.Domain.Models;

namespace Lesson.Responses.Questions
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public AnswerType AnswerType { get; set; }
        public bool IsScoring { get; set; }

        public int? Maxscore { get; set; }
    }
}

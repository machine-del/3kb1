using TestingPlatform.Application.DTOS;
using TestingPlatform.Domain.Enums;

namespace Lesson.Requests.Questions
{
    public class CreateQuestionRequest
    {
        public string Text { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public AnswerType AnswerType { get; set; }
        public bool IsScoring { get; set; }
        public int? Maxscore { get; set; }
        public int TestId { get; set; }
    }
}

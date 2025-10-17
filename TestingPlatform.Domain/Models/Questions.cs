using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TestingPlatform.Domain.Models;
using TestingPlatform.Domain.Enums;

namespace TestingPlatform.Domain.Models
{
    public class Questions
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int Number { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public AnswerType AnswerType { get; set; }

        [DefaultValue(true)]
        public bool IsScoring { get; set; }

        public int? Maxscore { get; set; }

        [Required]
        public int TestId { get; set; }

        public Test Test { get; set; }

        public List<UserAttemptsAnswer> UserAttemptAnswers { get; set; }

        public List<Answer> Answers { get; set; }

    }
}
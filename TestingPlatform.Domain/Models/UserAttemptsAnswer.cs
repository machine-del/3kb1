using TestingPlatform.Domain.Models;

namespace TestingPlatform.Domain.Models
{
    public class UserAttemptsAnswer
    {
        public int Id { get; set; }
        public bool IsCorrect { get; set; }
        public int ScoreAwarded { get; set; }
        public int AttemptId { get; set; }
        public int QuestionId { get; set; }
        public Attempt Attempt { get; set; }
        public Questions Questions { get; set; }
        public List<UserSelectedOption>? UserSelectedOptions { get; set; }
        public UserTextAnswer? UserTextAnswer { get; set; }
    }
}
using TestingPlatform.Domain.Enums;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Domain.Models
{
    public class UserSelectedOption
    {
        public int Id { get; set; }
        public int UserAttemptAnswerId { get; set; }
        public int AnswerId { get; set; }
        public UserAttemptsAnswer UserAttemptAnswer { get; set; }
        public Answer Answer { get; set; }
    }
}

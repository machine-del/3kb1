using System.ComponentModel.DataAnnotations;

namespace TestingPlatform.Domain.Models
{
    public class UserTextAnswer
    {
        public int Id {  get; set; }
        public string TextAnswer { get; set; }
        [Required]
        public int UserAttemptAnswerId { get; set; }
        public UserAttemptsAnswer UserAttemptsAnswer { get; set; }
    }
}

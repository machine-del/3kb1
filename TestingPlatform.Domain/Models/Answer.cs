using TestingPlatform.Domain.Enums;
using TestingPlatform.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace TestingPlatform.Domain.Enums
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public bool IsCorrect { get; set; }

        [Required]
        public int QuestionId  {  get; set; } 

        public Questions Question {  get; set; }
        public List<UserSelectedOption> UserSelectedOptions {  get; set; }


    }
}

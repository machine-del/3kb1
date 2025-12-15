using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Application.DTOS
{
    public class UserAttemptAnswerDTO
    {
            public int Id { get; set; }
            public bool IsCorrect { get; set; }
            public int ScoreAwarded { get; set; }
            public int AttemptId { get; set; }
            public int QuestionId { get; set; }
            public List<int>? UserSelectedOptions { get; set; }
            public UserTextAnswer? UserTextAnswer { get; set; }
    }
}

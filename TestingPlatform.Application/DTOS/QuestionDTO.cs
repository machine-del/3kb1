using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TestingPlatform.Domain.Enums;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Application.DTOS
{
    public class QuestionDTO
    { public int Id { get; set; }
        public string Text { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public AnswerType AnswerType { get; set; }
        public bool IsScoring { get; set; }
        public int? Maxscore { get; set; }
        public int TestId { get; set; }
        public List<AnswerDTO> Answers { get; set; }
       
    }
}
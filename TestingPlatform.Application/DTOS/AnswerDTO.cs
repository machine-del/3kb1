using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Application.DTOS
{
    public class AnswerDTO
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
    }
}

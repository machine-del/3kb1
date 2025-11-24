using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Application.DTOS
{
    public class AttemptDto
    {
        public int Id { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime SubmittedAt { get; set; }
        public int? Score { get; set; }
        public int TestId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}

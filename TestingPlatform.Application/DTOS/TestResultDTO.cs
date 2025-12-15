using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPlatform.Application.DTOS
{
    public class TestResultDTO
    {
        public int Id { get; set; }
        public bool Passed { get; set; }
        public int TestId { get; set; }
        public int AttemptId { get; set; }
        public int StudentId { get; set; }
        public int BestScore { get; set; }
    }
}

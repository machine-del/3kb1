using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Domain.Enums;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Application.DTOS
{
    public class TestDTO
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [DefaultValue(false)]
        public bool IsRepeatable { get; set; }
        public AnswerType AnswerType { get; set; }
        public TestType Type { get; set; }

        [DefaultValue("CURRENT_TIMESTAMP")]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime PublishedAt { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        public int? DurationMinutes { get; set; }

        [DefaultValue(false)]
        public bool IsPublic { get; set; }

        public int? PassingScore { get; set; }

        public int? MaxAttempts { get; set; }
        public List<Student> Students { get; set; }
        public List<Project> Projects { get; set; }
        public List<Course> Courses { get; set; }
        public List<Group> Groups { get; set; }
        public List<Direction> Directions { get; set; }
    }
}

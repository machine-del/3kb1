using System;
using System.ComponentModel.DataAnnotations;
using TestingPlatform.Domain.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TestingPlatform.Domain.Models
{
    public class Student
    {
        public int Id {get; set; }
        [Required]
        [MaxLength(30)]
        public string Phone { get; set; }
        public string VKLink { get; set; }
        [Required]
        public string VKProfileLink { get; set; }        
        [Required]
        public int UserId {  get; set; }
        public User User { get; set; }
        public List<Group> Groups { get; set; }
        public List<Test> Tests { get; set; }
        public List<Attempt> Attempts { get; set; }
        public List<TestResult> TestResults { get; set; }
    }
}

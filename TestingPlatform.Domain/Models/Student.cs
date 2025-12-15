using System;
using System.ComponentModel.DataAnnotations;
using TestingPlatform.Domain.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TestingPlatform.Domain.Models
{
    public class Student
    {
        public int Id {get; set; }
        public string Phone { get; set; }
        public string VKLink { get; set; }
        [Required]
        public string VKProfileLink { get; set; }  = string.Empty;
        public string? AvatarPath { get; set; }
        [Required]
        public int UserId {  get; set; }
        public User User { get; set; } = null;
        public List<Group> Groups { get; set; } = new List<Group>();
        public List<Test> Tests { get; set; } = new List<Test>(); 
        public List<Attempt> Attempts { get; set; } = new List<Attempt>(); 
        public List<TestResult> TestResults { get; set; } = new List<TestResult>();
    }
}

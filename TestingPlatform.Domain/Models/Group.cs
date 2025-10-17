using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Domain.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        [JsonIgnore]
        public int DirectionId {  get; set; }
        public Direction Direction { get; set; }
        [JsonIgnore]  
        public Course Course { get; set; }
       
        public int ProjectId {  get; set; }
        public Project Project { get; set; }
        public List<Student> Students { get; set; }
        public List<Test> Tests { get; set; }
    }
}

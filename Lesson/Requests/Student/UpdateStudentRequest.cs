using System.ComponentModel.DataAnnotations;

namespace Lesson.Requests.Student
{
    public class UpdateStudentRequest
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string VKLink { get; set; }
    }
}

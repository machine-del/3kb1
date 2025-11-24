using System.ComponentModel.DataAnnotations;

namespace Lesson.Requests.Auth
{
    public class AuthRequest
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

using TestingPlatform.Domain.Enums;

namespace TestingPlatform.Application.DTOS
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string VKLink { get; set; }
        public string Phone { get; set; }
        public UserRole Role { get; set; }
    }
}
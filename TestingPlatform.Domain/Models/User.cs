using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TestingPlatform.Domain.Enums;

namespace TestingPlatform.Domain.Models
{
    [Index(nameof(Login), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

        [Required]
        public string PasswordHash {  get; set; }

        [Required]
        [EmailAddress]
      
        public string Email {  get; set; }

        [Required]
        public string FirtsName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public UserRole Role {  get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        [JsonIgnore]
        [Required]
        public Student? Student { get; set; }

    }
}

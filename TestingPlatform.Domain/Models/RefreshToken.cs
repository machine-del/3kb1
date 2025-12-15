using TestingPlatform.Domain.Models;

namespace TestingPlatform.Infrastructure.Data
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string TokenHash { get; set; } = null;
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
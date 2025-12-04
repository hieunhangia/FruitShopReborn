using Core.Enums;

namespace Core.Entities.Users;

public abstract class User
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string Role => GetType().Name;
    public UserStatus Status { get; set; } = UserStatus.Active;
    public DateTime? LastLogin { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
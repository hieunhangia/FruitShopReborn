using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Users;

public class User : IdentityUser
{
    public string Role => GetType().Name;
    public UserStatus Status { get; set; } = UserStatus.Active;
    public DateTime? LastLogin { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
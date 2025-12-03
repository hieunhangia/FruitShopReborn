using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core;
using Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Repository.Entities.Users;

[Index(nameof(Email), IsUnique = true)]
public abstract class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(BussinessRuleConstant.EmailMaxLength)]
    [Unicode(false)]
    public string? Email { get; set; }
    
    [MaxLength(BussinessRuleConstant.PasswordHashMaxLength)]
    [Unicode(false)]
    public string? PasswordHash { get; set; }
    
    [Required]
    [MaxLength(BussinessRuleConstant.UserStatusMaxLength)]
    [Unicode(false)]
    public UserStatus Status { get; set; }
    
    public DateTime? LastLogin { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
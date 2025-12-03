using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core;
using Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Repository.Entities.Ai;

public class AiChatMessage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(BussinessRuleConstant.AiChatMessageRoleMaxLength)]
    [Unicode(false)]
    public AiMessageRole Role { get; set; }
    
    [Required]
    [MaxLength(BussinessRuleConstant.AiChatMessageContentMaxLength)]
    public string? Content { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public int ChatHistoryId { get; set; }
    [ForeignKey(nameof(ChatHistoryId))]
    public AiConversation? ChatHistory { get; set; }
}
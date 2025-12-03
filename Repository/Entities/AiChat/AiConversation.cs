using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Entities.Users;

namespace Repository.Entities.AiChat;

public class AiConversation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public int CustomerId { get; set; }
    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; }
    
    [InverseProperty(nameof(AiChatMessage.ChatHistory))]
    public ICollection<AiChatMessage>? Messages { get; set; }
}
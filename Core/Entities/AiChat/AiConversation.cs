using Core.Entities.Users;

namespace Core.Entities.AiChat;

public class AiConversation
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<AiChatMessage>? Messages { get; set; }
}
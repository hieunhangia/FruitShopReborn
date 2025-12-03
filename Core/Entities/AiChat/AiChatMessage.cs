using Core.Enums;

namespace Core.Entities.AiChat;

public class AiChatMessage
{
    public int Id { get; set; }
    public AiMessageRole Role { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int ChatHistoryId { get; set; }
    public AiConversation? ChatHistory { get; set; }
}
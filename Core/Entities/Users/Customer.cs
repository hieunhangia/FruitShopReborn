using Core.Entities.AiChat;

namespace Core.Entities.Users;

public class Customer : User
{
    public int? DefaultShippingInformationId { get; set; }
    public ShippingInformation? DefaultShippingInformation { get; set; }
    public ICollection<ShippingInformation>? ShippingInformations { get; set; }
    public ICollection<AiConversation>? Conversations { get; set; }
}
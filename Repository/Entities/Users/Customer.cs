using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Repository.Entities.AiChat;

namespace Repository.Entities.Users;

public class Customer : User
{
    public int? DefaultShippingInformationId { get; set; }
    [ForeignKey(nameof(DefaultShippingInformationId))]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public ShippingInformation? DefaultShippingInformation { get; set; }
    
    [InverseProperty(nameof(ShippingInformation.Customer))]
    public ICollection<ShippingInformation>? ShippingInformations { get; set; }
    
    [InverseProperty(nameof(AiConversation.Customer))]
    public ICollection<AiConversation>? Conversations { get; set; }
}
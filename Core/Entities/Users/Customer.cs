namespace Core.Entities.Users;

public class Customer : User
{
    public int DefaultShippingInformationId { get; set; }
    public ICollection<ShippingInformation>? ShippingInformations { get; set; }
}
namespace Core.Entities.Users;

public class ShippingInformation
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CommuneWardCode { get; set; }
    public string? DetailAddress { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
}
namespace Core.Entities.Users.Staffs;

public abstract class Staff : User
{
    public string? FullName { get; set; }
    public string? IdentityNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CommuneWardCode { get; set; }
    public string? DetailAddress { get; set; }
    public DateTime HireDate { get; set; }
}
namespace Core.Entities.Users.Staffs;

public abstract class Staff : User
{
    public DateTime HireDate { get; set; }
    public int StaffInformationId { get; set; }
    public StaffInformation? StaffInformation { get; set; }
}
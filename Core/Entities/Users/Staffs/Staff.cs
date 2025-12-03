using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities.Users.Staffs;

[Index(nameof(IdentityNumber), IsUnique = true)]
[Index(nameof(PhoneNumber), IsUnique = true)]
public abstract class Staff : User
{
    [Required]
    [MaxLength(BussinessRuleConstant.FullNameMaxLength)]
    public string? FullName { get; set; }
    
    [Required]
    [MaxLength(BussinessRuleConstant.IdentityNumberMaxLength)]
    [Unicode(false)]
    public string? IdentityNumber { get; set; }
    
    [Required]
    [MaxLength(BussinessRuleConstant.PhoneNumberLength)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }
    
    [Required]
    [MaxLength(BussinessRuleConstant.CommuneWardCodeLength)]
    [Unicode(false)]
    public string? CommuneWardCode { get; set; }
    
    [Required]
    [MaxLength(BussinessRuleConstant.DetailAddressMaxLength)]
    public string? DetailAddress { get; set; }
    
    [Required]
    public DateTime HireDate { get; set; }
}
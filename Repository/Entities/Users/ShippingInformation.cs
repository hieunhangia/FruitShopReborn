using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core;
using Microsoft.EntityFrameworkCore;

namespace Repository.Entities.Users;

public class ShippingInformation
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(BussinessRuleConstant.FullNameMaxLength)]
    public string? FullName { get; set; }
    
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
    
    public int CustomerId { get; set; }
    [ForeignKey(nameof(CustomerId))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Customer? Customer { get; set; }
}
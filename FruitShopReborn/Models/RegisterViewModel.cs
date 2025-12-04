using System.ComponentModel.DataAnnotations;
using Core;

namespace FruitShopReborn.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
    [DataType(DataType.EmailAddress)]
    [StringLength(BussinessRuleConstant.EmailMaxLength, ErrorMessage = "Email không được vượt quá {1} ký tự.")]
    [Display(Name = "Email")]
    public string? Email { get; init; }
    
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    [DataType(DataType.Password)]
    [StringLength(BussinessRuleConstant.PasswordMaxLength,
        MinimumLength = BussinessRuleConstant.PasswordMinLength,
        ErrorMessage = "Mật khẩu phải có từ {2} đến {1} ký tự.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$",
        ErrorMessage = "Mật khẩu phải chứa ít nhất một chữ hoa, một chữ thường, một chữ số và một ký tự đặc biệt.")]
    [Display(Name = "Mật khẩu")]
    public string? Password { get; init; }
    
    [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
    [Display(Name = "Xác nhận mật khẩu")]
    public string? ConfirmPassword { get; init; }
}
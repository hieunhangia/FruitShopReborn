using System.ComponentModel.DataAnnotations;

namespace FruitShopReborn.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string? Email { get; init; }
    
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string? Password { get; init; }
    
    [Display(Name = "Ghi nhớ đăng nhập")]
    public bool RememberMe { get; init; }

    public string? ReturnUrl { get; init; }
}
using System.ComponentModel.DataAnnotations;

namespace FruitShopReborn.Models.Ai;

public class AiViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập câu hỏi.")]
    [StringLength(255, ErrorMessage = "Câu hỏi không được vượt quá 255 ký tự.")]
    [Display(Name = "Câu hỏi")]
    public string? Prompt { get; init; }
}
using System.ComponentModel.DataAnnotations;
using homework_project.Models;

namespace homework_project.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public string UserFullName { get; set; } = string.Empty;

    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateOrderDto
{
    [Required(ErrorMessage = "ID користувача є обов'язковим")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Назва товару є обов'язковою")]
    [MaxLength(100)]
    public string ProductName { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Кількість має бути більше 0")]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Ціна має бути більше 0")]
    public decimal Price { get; set; }
}

public class UpdateOrderDto
{
    [Required(ErrorMessage = "Назва товару є обов'язковою")]
    [MaxLength(100)]
    public string ProductName { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Кількість має бути більше 0")]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Ціна має бути більше 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Статус є обов'язковим")]
    public OrderStatus Status { get; set; }
}

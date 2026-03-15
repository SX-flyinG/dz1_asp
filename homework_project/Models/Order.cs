using System.ComponentModel.DataAnnotations;

namespace homework_project.Models;

public class Order
{
    public int Id { get; set; }

    [Required(ErrorMessage = "ID користувача є обов'язковим")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Назва товару є обов'язковою")]
    [MaxLength(100)]
    public string ProductName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Кількість є обов'язковою")]
    [Range(1, int.MaxValue, ErrorMessage = "Кількість має бути більше 0")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Ціна є обов'язковою")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Ціна має бути більше 0")]
    public decimal Price { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User? User { get; set; }
}

public enum OrderStatus
{
    Pending,
    Processing,
    Completed,
    Cancelled
}

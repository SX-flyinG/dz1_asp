using System.ComponentModel.DataAnnotations;

namespace homework_project.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва продукту обов'язкова")]
        [Display(Name = "Назва продукту")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Категорія обов'язкова")]
        [Display(Name = "Категорія")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Ціна обов'язкова")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Ціна повинна бути більше 0")]
        [Display(Name = "Ціна (грн)")]
        public decimal Price { get; set; }

        [Display(Name = "Кількість на складі")]
        [Range(0, int.MaxValue, ErrorMessage = "Кількість не може бути від'ємною")]
        public int Stock { get; set; }

        [Display(Name = "Опис")]
        public string Description { get; set; }

        [Display(Name = "Дата додавання")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

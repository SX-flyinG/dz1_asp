namespace homework_project.Models
{
    public static class ProductRepository
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Ноутбук Dell XPS 15", Category = "Електроніка", Price = 45000, Stock = 12, Description = "Потужний ноутбук для роботи та розваг", CreatedAt = DateTime.Now.AddDays(-30) },
            new Product { Id = 2, Name = "Смартфон Samsung Galaxy S24", Category = "Електроніка", Price = 28000, Stock = 35, Description = "Флагманський смартфон з AMOLED дисплеєм", CreatedAt = DateTime.Now.AddDays(-25) },
            new Product { Id = 3, Name = "Кавоварка Delonghi", Category = "Побутова техніка", Price = 8500, Stock = 8, Description = "Автоматична еспресо-машина", CreatedAt = DateTime.Now.AddDays(-20) },
            new Product { Id = 4, Name = "Навушники Sony WH-1000XM5", Category = "Аудіо", Price = 12000, Stock = 20, Description = "Преміальні навушники з ANC", CreatedAt = DateTime.Now.AddDays(-15) },
            new Product { Id = 5, Name = "Ноутбук Dell XPS 15", Category = "Електроніка", Price = 47000, Stock = 5, Description = "Оновлена версія з кращим процесором", CreatedAt = DateTime.Now.AddDays(-5) },
        };

        private static int _nextId = 6;

        public static List<Product> GetAll() => _products.ToList();

        public static Product GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public static List<Product> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return new List<Product>();
            return _products
                .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public static void Add(Product product)
        {
            product.Id = _nextId++;
            product.CreatedAt = DateTime.Now;
            _products.Add(product);
        }

        public static bool Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return false;
            _products.Remove(product);
            return true;
        }

        public static bool Update(Product updated)
        {
            var existing = _products.FirstOrDefault(p => p.Id == updated.Id);
            if (existing == null) return false;

            existing.Name = updated.Name;
            existing.Description = updated.Description;
            existing.Price = updated.Price;
            existing.Category = updated.Category;
            existing.CreatedAt = updated.CreatedAt;
            existing.Stock = updated.Stock;
            return true;
        }
    }
}

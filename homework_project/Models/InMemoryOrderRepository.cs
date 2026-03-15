
namespace homework_project.Models;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new()
    {
        new Order
        {
            Id          = 1,
            UserId      = 1,
            ProductName = "Ноутбук Dell XPS",
            Quantity    = 1,
            Price       = 45000.00m,
            Status      = OrderStatus.Completed,
            CreatedAt   = DateTime.UtcNow.AddDays(-5)
        },
        new Order
        {
            Id          = 2,
            UserId      = 1,
            ProductName = "Механічна клавіатура",
            Quantity    = 2,
            Price       = 3500.00m,
            Status      = OrderStatus.Processing,
            CreatedAt   = DateTime.UtcNow.AddDays(-2)
        },
        new Order
        {
            Id          = 3,
            UserId      = 2,
            ProductName = "Навушники Sony WH-1000XM5",
            Quantity    = 1,
            Price       = 12000.00m,
            Status      = OrderStatus.Pending,
            CreatedAt   = DateTime.UtcNow.AddDays(-1)
        }
    };

    private int _nextId = 4;

    // Зберігаємо посилання на репозиторій юзерів щоб підтягувати навігаційну властивість
    private readonly IUserRepository _userRepo;

    public InMemoryOrderRepository(IUserRepository userRepo)
    {
        _userRepo = userRepo;
        AttachUsers();
    }

    // Підв'язуємо User до кожного Order
    private void AttachUsers()
    {
        foreach (var order in _orders)
            order.User = _userRepo.GetById(order.UserId);
    }

    public IEnumerable<Order> GetAll() => _orders.ToList();

    public IEnumerable<Order> GetByUserId(int userId) =>
        _orders.Where(o => o.UserId == userId).ToList();

    public Order? GetById(int id) =>
        _orders.FirstOrDefault(o => o.Id == id);

    public Order Add(Order order)
    {
        order.Id        = _nextId++;
        order.CreatedAt = DateTime.UtcNow;
        order.User      = _userRepo.GetById(order.UserId);
        _orders.Add(order);
        return order;
    }

    public Order? Update(int id, Order updated)
    {
        var existing = _orders.FirstOrDefault(o => o.Id == id);
        if (existing is null) return null;

        existing.ProductName = updated.ProductName;
        existing.Quantity    = updated.Quantity;
        existing.Price       = updated.Price;
        existing.Status      = updated.Status;

        return existing;
    }

    public bool Delete(int id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order is null) return false;

        _orders.Remove(order);
        return true;
    }
}

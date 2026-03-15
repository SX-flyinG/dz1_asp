
namespace homework_project.Models;

public interface IOrderRepository
{
    IEnumerable<Order> GetAll();
    IEnumerable<Order> GetByUserId(int userId);
    Order? GetById(int id);
    Order Add(Order order);
    Order? Update(int id, Order updated);
    bool Delete(int id);
}

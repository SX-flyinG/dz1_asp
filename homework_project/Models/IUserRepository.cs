
namespace homework_project.Models;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User? GetById(int id);
    User? GetByEmail(string email);
    User Add(User user);
    User? Update(int id, User updated);
    bool Delete(int id);
    bool EmailExists(string email, int? excludeId = null);
}

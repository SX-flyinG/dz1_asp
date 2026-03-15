
namespace homework_project.Models;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new()
    {
        new User
        {
            Id          = 1,
            Name        = "Іван",
            LastName    = "Петренко",
            Email       = "ivan@example.com",
            PhoneNumber = "+380991234567",
            DateOfBirth = new DateOnly(1995, 6, 15),
            CreatedAt   = DateTime.UtcNow
        },
        new User
        {
            Id          = 2,
            Name        = "Олена",
            LastName    = "Коваленко",
            Email       = "olena@example.com",
            PhoneNumber = "+380507654321",
            DateOfBirth = new DateOnly(1990, 3, 22),
            CreatedAt   = DateTime.UtcNow
        }
    };

    private int _nextId = 3;

    public IEnumerable<User> GetAll() => _users.ToList();

    public User? GetById(int id) =>
        _users.FirstOrDefault(u => u.Id == id);

    public User? GetByEmail(string email) =>
        _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

    public User Add(User user)
    {
        user.Id        = _nextId++;
        user.CreatedAt = DateTime.UtcNow;
        _users.Add(user);
        return user;
    }

    public User? Update(int id, User updated)
    {
        var existing = _users.FirstOrDefault(u => u.Id == id);
        if (existing is null) return null;

        existing.Name        = updated.Name;
        existing.LastName    = updated.LastName;
        existing.Email       = updated.Email;
        existing.PhoneNumber = updated.PhoneNumber;
        existing.DateOfBirth = updated.DateOfBirth;

        return existing;
    }

    public bool Delete(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user is null) return false;

        _users.Remove(user);
        return true;
    }

    public bool EmailExists(string email, int? excludeId = null) =>
        _users.Any(u =>
            u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
            (excludeId == null || u.Id != excludeId));
}

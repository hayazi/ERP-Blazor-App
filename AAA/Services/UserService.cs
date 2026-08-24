using ERPBlazorApp.AAA.Models;

namespace ERPBlazorApp.AAA.Services;

public class UserService
{
    private List<User> _users;
    private List<UserRole> _userRoles;

    public UserService()
    {
        _users = AAASampleData.GetUsers();
        _userRoles = new List<UserRole>();
    }

    public List<User> GetAll() => _users;
    public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);
    public User? GetByUsername(string username) => _users.FirstOrDefault(u => u.Username == username);

    public void Add(User user)
    {
        user.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
        user.CreatedAt = DateTime.Now;
        _users.Add(user);
    }

    public void Update(int id, User user)
    {
        var existing = GetById(id);
        if (existing == null) return;
        existing.Username = user.Username;
        existing.Email = user.Email;
        existing.FirstName = user.FirstName;
        existing.LastName = user.LastName;
        existing.IsActive = user.IsActive;
        if (!string.IsNullOrEmpty(user.PasswordHash))
        {
            existing.PasswordHash = user.PasswordHash;
        }
    }

    public void Delete(int id)
    {
        var user = GetById(id);
        if (user != null)
        {
            _users.Remove(user);
            _userRoles.RemoveAll(ur => ur.UserId == id);
        }
    }

    public List<Role> GetUserRoles(int userId)
    {
        return _userRoles.Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role!)
            .ToList();
    }

    public void AssignRole(int userId, int roleId)
    {
        if (_userRoles.Any(ur => ur.UserId == userId && ur.RoleId == roleId)) return;
        _userRoles.Add(new UserRole
        {
            Id = _userRoles.Any() ? _userRoles.Max(ur => ur.Id) + 1 : 1,
            UserId = userId,
            RoleId = roleId,
            AssignedAt = DateTime.Now
        });
    }

    public void RemoveRole(int userId, int roleId)
    {
        _userRoles.RemoveAll(ur => ur.UserId == userId && ur.RoleId == roleId);
    }
}

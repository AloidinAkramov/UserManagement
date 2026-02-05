using UserManagement.Models;

namespace UserManagement.Services;
public interface IUserService
{
    Task RegisterAsync(string name, string email, string password);
    Task<User?> LoginAsync(string email, string password);
    Task BlockAsync(List<Guid> userId);
    Task UnblockAsync(List<Guid> userId);
    Task<List<User>> GetAllAsync();
    Task DeleteAsync(List<Guid> userId);
    Task DeleteUnverifiedAsync();
    Task ConfirmAsync(Guid id);
}
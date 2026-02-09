using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Services;
public class UserService : IUserService
{
    private readonly AppDbContext context;

    public UserService(AppDbContext appcontext)
    {
        context = appcontext;
    }

    public async Task RegisterAsync(string name, string email, string password)
    {
        // IMPORTANT:
        // User is always created as Unverified.
        // Email confirmation is OPTIONAL according to task requirements.
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            PasswordHash = Hash(password),
            Status = UserStatus.Unverified,
            CreatedAt = DateTime.UtcNow,
        };

        context.Users.Add(user);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            // NOTE:
            // Email uniqueness is enforced on database level (unique index).
            // Exception here means duplicate email.
            throw new Exception("Email already exists");
        }
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var hash = Hash(password);

        var user = await context.Users
            .FirstOrDefaultAsync(x => x.Email == email && x.PasswordHash == hash);

        if (user == null)
            return null;

        // IMPORTANT:
        // Blocked users are NOT allowed to login.
        // Unverified users ARE allowed to login.
        if (user.Status == UserStatus.Blocked)
            return null;

        user.LastLoginAt = DateTime.UtcNow;
        await context.SaveChangesAsync();

        return user;
    }

    public async Task BlockAsync(List<Guid> userIds)
    {
        // NOTE:
        // Bulk operation: multiple users can be blocked at once.
        if (!userIds.Any()) return;

        var users = await context.Users
       .Where(x => userIds.Contains(x.Id))
       .ToListAsync();

        foreach (var user in users)
            user.Status = UserStatus.Blocked;

        await context.SaveChangesAsync();
    }

    public async Task UnblockAsync(List<Guid> userIds)
    {
        // NOTE:
        // Bulk operation: multiple users can be unblocked at once.
        if (!userIds.Any()) return;

        var users = await context.Users
       .Where(x => userIds.Contains(x.Id))
       .ToListAsync();

        foreach (var user in users)
            user.Status = UserStatus.Active;

        await context.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        // NOTE:
        // Users are ordered by last activity (LastLoginAt).
        return await context.Users
            .OrderByDescending(x => x.LastLoginAt ?? DateTime.MinValue)
            .ToListAsync();
    }

    public async Task DeleteAsync(List<Guid> userIds)
    {
        // IMPORTANT:
        // Physical delete (not soft delete), as required by task.
        if (!userIds.Any()) return;

        var users = await context.Users
        .Where(x => userIds.Contains(x.Id))
        .ToListAsync();

        context.Users.RemoveRange(users);
        await context.SaveChangesAsync();
    }

    public async Task DeleteUnverifiedAsync()
    {
        // IMPORTANT:
        // Deletes ONLY users with Unverified status.
        // Used by toolbar action.
        var users = await context.Users
            .Where(x => x.Status == UserStatus.Unverified)
            .ToListAsync();

        if (!users.Any()) return;

        context.Users.RemoveRange(users);
        await context.SaveChangesAsync();
    }

    public async Task ConfirmAsync(Guid id)
    {
        // NOTE:
        // Fake email confirmation (button-based),
        // allowed by task description.
        var user = await context.Users.FindAsync(id);
        if (user == null) return;

        if (user.Status == UserStatus.Unverified)
            user.Status = UserStatus.Active;

        await context.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    private static string Hash(string value)
    {
        // NOTE:
        // Simple SHA256 hashing is used for demo purposes.
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
        return Convert.ToBase64String(bytes);
    }
}
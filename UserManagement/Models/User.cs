namespace UserManagement.Models;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public UserStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}

// NOTE: Represents current user account status
public enum UserStatus
{
    Unverified,
    Active,
    Blocked
}
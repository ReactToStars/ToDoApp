using System.ComponentModel.DataAnnotations;

namespace ToDo.API.Models;

public class User
{
    public Guid Id { get; set; }

    [Required, MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required, MaxLength(256), EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required] // MaxLength is optional here, but hashes are usually fixed size (e.g., 60-100 chars)
    public string PasswordHash { get; set; } = string.Empty;

    // Use UtcNow to avoid cross-timezone server bugs
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }

    // Navigation properties
    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}

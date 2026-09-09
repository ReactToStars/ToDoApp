namespace ToDo.API.Models;

public enum Priority
{
    Low = 0,
    Medium = 1,
    High = 2
}

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = false;
    public Priority Priority { get; set; } = Priority.Medium;
    public DateTime? DueDate { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }

    // Foreign Keys
    public Guid UserId { get; set; }
    public int? CategoryId { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public Category? Category { get; set; }
}
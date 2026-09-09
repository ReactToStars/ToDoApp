namespace ToDo.API.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ColorHex { get; set; } = "#6c757d"; // Default color hex for UI badges

    // Foreign Key
    public Guid UserId { get; set; }
    
    // Navigation properties
    public User User { get; set; } = null!;
    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}
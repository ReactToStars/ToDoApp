using ToDo.API.Models;

namespace ToDo.API.DTOs;

public class TodoItemDTO
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
    public Priority? Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Guid? UserId { get; set; }
    public int? CategoryId { get; set; }
}
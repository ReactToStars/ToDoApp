namespace ToDo.API.DTOs;

public class CategoryDTO
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? ColorHex { get; set; }
    public Guid? UserId { get; set; }
}
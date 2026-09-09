using System.ComponentModel.DataAnnotations;

namespace ToDo.API.DTOs;

public class UserDTO
{
    public Guid? Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

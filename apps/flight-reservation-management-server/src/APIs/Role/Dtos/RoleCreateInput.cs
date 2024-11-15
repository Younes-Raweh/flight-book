namespace FlightReservationManagement.APIs.Dtos;

public class RoleCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Description { get; set; }

    public string? DisplayName { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? PermissionId { get; set; }

    public Role? Role { get; set; }

    public List<Role>? Roles { get; set; }

    public DateTime UpdatedAt { get; set; }
}
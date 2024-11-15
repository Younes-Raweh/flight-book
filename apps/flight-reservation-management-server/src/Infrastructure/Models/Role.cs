using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightReservationManagement.Infrastructure.Models;

[Table("Roles")]
public class RoleDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(1000)]
    public string? DisplayName { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [StringLength(1000)]
    public string? PermissionId { get; set; }

    public string? RoleId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public RoleDbModel? Role { get; set; } = null;

    public List<RoleDbModel>? Roles { get; set; } = new List<RoleDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}

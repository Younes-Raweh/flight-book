using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightReservationManagement.Infrastructure.Models;

[Table("Profiles")]
public class ProfileDbModel
{
    [StringLength(1000)]
    public string? Address { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? FirstName { get; set; }

    [Range(-999999999, 999999999)]
    public int? GenderId { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? OtherName { get; set; }

    [StringLength(1000)]
    public string? PhoneNumber { get; set; }

    [StringLength(1000)]
    public string? Photo { get; set; }

    [StringLength(1000)]
    public string? SurName { get; set; }

    [Range(-999999999, 999999999)]
    public int? TitleId { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? UserId { get; set; }
}

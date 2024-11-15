using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightReservationManagement.Infrastructure.Models;

[Table("Vats")]
public class VatDbModel
{
    [Range(-999999999, 999999999)]
    public int? CarVatType { get; set; }

    [Range(-999999999, 999999999)]
    public int? CarVatValue { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Range(-999999999, 999999999)]
    public int? FlightVatType { get; set; }

    [Range(-999999999, 999999999)]
    public int? FlightVatValue { get; set; }

    [Range(-999999999, 999999999)]
    public int? HotelVatType { get; set; }

    [Range(-999999999, 999999999)]
    public int? HotelVatValue { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Range(-999999999, 999999999)]
    public int? PackageVatType { get; set; }

    [Range(-999999999, 999999999)]
    public int? PackageVatValue { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}

namespace FlightReservationManagement.APIs.Dtos;

public class VatCreateInput
{
    public int? CarVatType { get; set; }

    public int? CarVatValue { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? FlightVatType { get; set; }

    public int? FlightVatValue { get; set; }

    public int? HotelVatType { get; set; }

    public int? HotelVatValue { get; set; }

    public string? Id { get; set; }

    public int? PackageVatType { get; set; }

    public int? PackageVatValue { get; set; }

    public DateTime UpdatedAt { get; set; }
}

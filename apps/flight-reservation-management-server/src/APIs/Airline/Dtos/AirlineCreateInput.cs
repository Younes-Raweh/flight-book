namespace FlightReservationManagement.APIs.Dtos;

public class AirlineCreateInput
{
    public string? Code { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? IcaoCode { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public DateTime UpdatedAt { get; set; }
}

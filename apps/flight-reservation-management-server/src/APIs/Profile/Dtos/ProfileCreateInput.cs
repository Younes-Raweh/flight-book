namespace FlightReservationManagement.APIs.Dtos;

public class ProfileCreateInput
{
    public string? Address { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? FirstName { get; set; }

    public int? GenderId { get; set; }

    public string? Id { get; set; }

    public string? OtherName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Photo { get; set; }

    public string? SurName { get; set; }

    public int? TitleId { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UserId { get; set; }
}

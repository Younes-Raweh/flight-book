using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class ProfilesExtensions
{
    public static Profile ToDto(this ProfileDbModel model)
    {
        return new Profile
        {
            Address = model.Address,
            CreatedAt = model.CreatedAt,
            FirstName = model.FirstName,
            GenderId = model.GenderId,
            Id = model.Id,
            OtherName = model.OtherName,
            PhoneNumber = model.PhoneNumber,
            Photo = model.Photo,
            SurName = model.SurName,
            TitleId = model.TitleId,
            UpdatedAt = model.UpdatedAt,
            UserId = model.UserId,
        };
    }

    public static ProfileDbModel ToModel(
        this ProfileUpdateInput updateDto,
        ProfileWhereUniqueInput uniqueId
    )
    {
        var profile = new ProfileDbModel
        {
            Id = uniqueId.Id,
            Address = updateDto.Address,
            FirstName = updateDto.FirstName,
            GenderId = updateDto.GenderId,
            OtherName = updateDto.OtherName,
            PhoneNumber = updateDto.PhoneNumber,
            Photo = updateDto.Photo,
            SurName = updateDto.SurName,
            TitleId = updateDto.TitleId,
            UserId = updateDto.UserId
        };

        if (updateDto.CreatedAt != null)
        {
            profile.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            profile.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return profile;
    }
}

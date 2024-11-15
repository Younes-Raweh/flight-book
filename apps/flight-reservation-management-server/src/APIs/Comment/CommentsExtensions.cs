using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.Infrastructure.Models;

namespace FlightReservationManagement.APIs.Extensions;

public static class CommentsExtensions
{
    public static Comment ToDto(this CommentDbModel model)
    {
        return new Comment
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static CommentDbModel ToModel(
        this CommentUpdateInput updateDto,
        CommentWhereUniqueInput uniqueId
    )
    {
        var comment = new CommentDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            comment.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            comment.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return comment;
    }
}

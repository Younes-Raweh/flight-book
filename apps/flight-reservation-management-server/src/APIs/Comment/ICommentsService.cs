using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface ICommentsService
{
    /// <summary>
    /// Create one Comment
    /// </summary>
    public Task<Comment> CreateComment(CommentCreateInput comment);

    /// <summary>
    /// Delete one Comment
    /// </summary>
    public Task DeleteComment(CommentWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Comments
    /// </summary>
    public Task<List<Comment>> Comments(CommentFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Comment records
    /// </summary>
    public Task<MetadataDto> CommentsMeta(CommentFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Comment
    /// </summary>
    public Task<Comment> Comment(CommentWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Comment
    /// </summary>
    public Task UpdateComment(CommentWhereUniqueInput uniqueId, CommentUpdateInput updateDto);
}

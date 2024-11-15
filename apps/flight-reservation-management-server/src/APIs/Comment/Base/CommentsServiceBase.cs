using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class CommentsServiceBase : ICommentsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public CommentsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Comment
    /// </summary>
    public async Task<Comment> CreateComment(CommentCreateInput createDto)
    {
        var comment = new CommentDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            comment.Id = createDto.Id;
        }

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CommentDbModel>(comment.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Comment
    /// </summary>
    public async Task DeleteComment(CommentWhereUniqueInput uniqueId)
    {
        var comment = await _context.Comments.FindAsync(uniqueId.Id);
        if (comment == null)
        {
            throw new NotFoundException();
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Comments
    /// </summary>
    public async Task<List<Comment>> Comments(CommentFindManyArgs findManyArgs)
    {
        var comments = await _context
            .Comments.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return comments.ConvertAll(comment => comment.ToDto());
    }

    /// <summary>
    /// Meta data about Comment records
    /// </summary>
    public async Task<MetadataDto> CommentsMeta(CommentFindManyArgs findManyArgs)
    {
        var count = await _context.Comments.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Comment
    /// </summary>
    public async Task<Comment> Comment(CommentWhereUniqueInput uniqueId)
    {
        var comments = await this.Comments(
            new CommentFindManyArgs { Where = new CommentWhereInput { Id = uniqueId.Id } }
        );
        var comment = comments.FirstOrDefault();
        if (comment == null)
        {
            throw new NotFoundException();
        }

        return comment;
    }

    /// <summary>
    /// Update one Comment
    /// </summary>
    public async Task UpdateComment(CommentWhereUniqueInput uniqueId, CommentUpdateInput updateDto)
    {
        var comment = updateDto.ToModel(uniqueId);

        _context.Entry(comment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Comments.Any(e => e.Id == comment.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}

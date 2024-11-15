using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class VisaApplicationsServiceBase : IVisaApplicationsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public VisaApplicationsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one VisaApplication
    /// </summary>
    public async Task<VisaApplication> CreateVisaApplication(VisaApplicationCreateInput createDto)
    {
        var visaApplication = new VisaApplicationDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            visaApplication.Id = createDto.Id;
        }

        _context.VisaApplications.Add(visaApplication);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<VisaApplicationDbModel>(visaApplication.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one VisaApplication
    /// </summary>
    public async Task DeleteVisaApplication(VisaApplicationWhereUniqueInput uniqueId)
    {
        var visaApplication = await _context.VisaApplications.FindAsync(uniqueId.Id);
        if (visaApplication == null)
        {
            throw new NotFoundException();
        }

        _context.VisaApplications.Remove(visaApplication);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many VisaApplications
    /// </summary>
    public async Task<List<VisaApplication>> VisaApplications(
        VisaApplicationFindManyArgs findManyArgs
    )
    {
        var visaApplications = await _context
            .VisaApplications.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return visaApplications.ConvertAll(visaApplication => visaApplication.ToDto());
    }

    /// <summary>
    /// Meta data about VisaApplication records
    /// </summary>
    public async Task<MetadataDto> VisaApplicationsMeta(VisaApplicationFindManyArgs findManyArgs)
    {
        var count = await _context.VisaApplications.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one VisaApplication
    /// </summary>
    public async Task<VisaApplication> VisaApplication(VisaApplicationWhereUniqueInput uniqueId)
    {
        var visaApplications = await this.VisaApplications(
            new VisaApplicationFindManyArgs
            {
                Where = new VisaApplicationWhereInput { Id = uniqueId.Id }
            }
        );
        var visaApplication = visaApplications.FirstOrDefault();
        if (visaApplication == null)
        {
            throw new NotFoundException();
        }

        return visaApplication;
    }

    /// <summary>
    /// Update one VisaApplication
    /// </summary>
    public async Task UpdateVisaApplication(
        VisaApplicationWhereUniqueInput uniqueId,
        VisaApplicationUpdateInput updateDto
    )
    {
        var visaApplication = updateDto.ToModel(uniqueId);

        _context.Entry(visaApplication).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.VisaApplications.Any(e => e.Id == visaApplication.Id))
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

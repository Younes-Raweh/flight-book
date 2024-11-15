using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using FlightReservationManagement.APIs.Extensions;
using FlightReservationManagement.Infrastructure;
using FlightReservationManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightReservationManagement.APIs;

public abstract class ReservationsServiceBase : IReservationsService
{
    protected readonly FlightReservationManagementDbContext _context;

    public ReservationsServiceBase(FlightReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Reservation
    /// </summary>
    public async Task<Reservation> CreateReservation(ReservationCreateInput createDto)
    {
        var reservation = new ReservationDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            reservation.Id = createDto.Id;
        }

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ReservationDbModel>(reservation.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Reservation
    /// </summary>
    public async Task DeleteReservation(ReservationWhereUniqueInput uniqueId)
    {
        var reservation = await _context.Reservations.FindAsync(uniqueId.Id);
        if (reservation == null)
        {
            throw new NotFoundException();
        }

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Reservations
    /// </summary>
    public async Task<List<Reservation>> Reservations(ReservationFindManyArgs findManyArgs)
    {
        var reservations = await _context
            .Reservations.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return reservations.ConvertAll(reservation => reservation.ToDto());
    }

    /// <summary>
    /// Meta data about Reservation records
    /// </summary>
    public async Task<MetadataDto> ReservationsMeta(ReservationFindManyArgs findManyArgs)
    {
        var count = await _context.Reservations.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Reservation
    /// </summary>
    public async Task<Reservation> Reservation(ReservationWhereUniqueInput uniqueId)
    {
        var reservations = await this.Reservations(
            new ReservationFindManyArgs { Where = new ReservationWhereInput { Id = uniqueId.Id } }
        );
        var reservation = reservations.FirstOrDefault();
        if (reservation == null)
        {
            throw new NotFoundException();
        }

        return reservation;
    }

    /// <summary>
    /// Update one Reservation
    /// </summary>
    public async Task UpdateReservation(
        ReservationWhereUniqueInput uniqueId,
        ReservationUpdateInput updateDto
    )
    {
        var reservation = updateDto.ToModel(uniqueId);

        _context.Entry(reservation).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Reservations.Any(e => e.Id == reservation.Id))
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

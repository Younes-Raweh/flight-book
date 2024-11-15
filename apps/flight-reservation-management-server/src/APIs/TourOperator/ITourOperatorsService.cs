using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;

namespace FlightReservationManagement.APIs;

public interface ITourOperatorsService
{
    /// <summary>
    /// Create one Tour Operator
    /// </summary>
    public Task<TourOperator> CreateTourOperator(TourOperatorCreateInput touroperator);

    /// <summary>
    /// Delete one Tour Operator
    /// </summary>
    public Task DeleteTourOperator(TourOperatorWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Tour Operators
    /// </summary>
    public Task<List<TourOperator>> TourOperators(TourOperatorFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Tour Operator records
    /// </summary>
    public Task<MetadataDto> TourOperatorsMeta(TourOperatorFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Tour Operator
    /// </summary>
    public Task<TourOperator> TourOperator(TourOperatorWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Tour Operator
    /// </summary>
    public Task UpdateTourOperator(
        TourOperatorWhereUniqueInput uniqueId,
        TourOperatorUpdateInput updateDto
    );
}

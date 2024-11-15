using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class VisaApplicationsControllerBase : ControllerBase
{
    protected readonly IVisaApplicationsService _service;

    public VisaApplicationsControllerBase(IVisaApplicationsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one VisaApplication
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<VisaApplication>> CreateVisaApplication(
        VisaApplicationCreateInput input
    )
    {
        var visaApplication = await _service.CreateVisaApplication(input);

        return CreatedAtAction(
            nameof(VisaApplication),
            new { id = visaApplication.Id },
            visaApplication
        );
    }

    /// <summary>
    /// Delete one VisaApplication
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteVisaApplication(
        [FromRoute()] VisaApplicationWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteVisaApplication(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many VisaApplications
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<VisaApplication>>> VisaApplications(
        [FromQuery()] VisaApplicationFindManyArgs filter
    )
    {
        return Ok(await _service.VisaApplications(filter));
    }

    /// <summary>
    /// Meta data about VisaApplication records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> VisaApplicationsMeta(
        [FromQuery()] VisaApplicationFindManyArgs filter
    )
    {
        return Ok(await _service.VisaApplicationsMeta(filter));
    }

    /// <summary>
    /// Get one VisaApplication
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<VisaApplication>> VisaApplication(
        [FromRoute()] VisaApplicationWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.VisaApplication(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one VisaApplication
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateVisaApplication(
        [FromRoute()] VisaApplicationWhereUniqueInput uniqueId,
        [FromQuery()] VisaApplicationUpdateInput visaApplicationUpdateDto
    )
    {
        try
        {
            await _service.UpdateVisaApplication(uniqueId, visaApplicationUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}

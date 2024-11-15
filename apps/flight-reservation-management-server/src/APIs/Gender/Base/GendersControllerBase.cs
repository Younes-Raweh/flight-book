using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class GendersControllerBase : ControllerBase
{
    protected readonly IGendersService _service;

    public GendersControllerBase(IGendersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Gender
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Gender>> CreateGender(GenderCreateInput input)
    {
        var gender = await _service.CreateGender(input);

        return CreatedAtAction(nameof(Gender), new { id = gender.Id }, gender);
    }

    /// <summary>
    /// Delete one Gender
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteGender([FromRoute()] GenderWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteGender(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Genders
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Gender>>> Genders([FromQuery()] GenderFindManyArgs filter)
    {
        return Ok(await _service.Genders(filter));
    }

    /// <summary>
    /// Meta data about Gender records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> GendersMeta(
        [FromQuery()] GenderFindManyArgs filter
    )
    {
        return Ok(await _service.GendersMeta(filter));
    }

    /// <summary>
    /// Get one Gender
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Gender>> Gender([FromRoute()] GenderWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Gender(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Gender
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateGender(
        [FromRoute()] GenderWhereUniqueInput uniqueId,
        [FromQuery()] GenderUpdateInput genderUpdateDto
    )
    {
        try
        {
            await _service.UpdateGender(uniqueId, genderUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}

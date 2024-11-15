using FlightReservationManagement.APIs;
using FlightReservationManagement.APIs.Common;
using FlightReservationManagement.APIs.Dtos;
using FlightReservationManagement.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class GalleriesControllerBase : ControllerBase
{
    protected readonly IGalleriesService _service;

    public GalleriesControllerBase(IGalleriesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Gallery
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Gallery>> CreateGallery(GalleryCreateInput input)
    {
        var gallery = await _service.CreateGallery(input);

        return CreatedAtAction(nameof(Gallery), new { id = gallery.Id }, gallery);
    }

    /// <summary>
    /// Delete one Gallery
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteGallery([FromRoute()] GalleryWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteGallery(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Galleries
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<Gallery>>> Galleries(
        [FromQuery()] GalleryFindManyArgs filter
    )
    {
        return Ok(await _service.Galleries(filter));
    }

    /// <summary>
    /// Meta data about Gallery records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> GalleriesMeta(
        [FromQuery()] GalleryFindManyArgs filter
    )
    {
        return Ok(await _service.GalleriesMeta(filter));
    }

    /// <summary>
    /// Get one Gallery
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<Gallery>> Gallery([FromRoute()] GalleryWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Gallery(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Gallery
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateGallery(
        [FromRoute()] GalleryWhereUniqueInput uniqueId,
        [FromQuery()] GalleryUpdateInput galleryUpdateDto
    )
    {
        try
        {
            await _service.UpdateGallery(uniqueId, galleryUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}

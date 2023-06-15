using CocCanService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CocCanService.DTOs.Location;

namespace CocCanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LocationDTO>))]
        //public async Task<IActionResult> GetAll()
        //{
        //    var locations = await _locationService.GetAllLocationsAsync();
        //    return Ok(locations);
        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LocationDTO))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult<LocationDTO>> CreateLocation([FromBody] CreateLocationDTO createLocationDTO)
        //{
        //    if (createLocationDTO == null)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!ModelState.IsValid) { return BadRequest(ModelState); }

        //    var _newLocation = await _locationService.CreateLocationAsync(createLocationDTO);

        //    if (_newLocation.Success == false && _newLocation.Message == "Exist")
        //    {
        //        return Ok(_newLocation);
        //    }


        //    if (_newLocation.Success == false && _newLocation.Message == "RepoError")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding location {createLocationDTO}");
        //        return StatusCode(500, ModelState);
        //    }

        //    if (_newLocation.Success == false && _newLocation.Message == "Error")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in service layer when adding location {createLocationDTO}");
        //        return StatusCode(500, ModelState);
        //    }
        //    return Ok(_newLocation);
        //}

        //[HttpPatch("{id:Guid}", Name = "UpdateLocation")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> UpdateLocation(Guid id, [FromBody] LocationDTO locationDTO)
        //{
        //    if (locationDTO == null || locationDTO.Id != id)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    var _updateLocation = await _locationService.UpdateLocationAsync(locationDTO);

        //    if (_updateLocation.Success == false && _updateLocation.Message == "NotFound")
        //    {
        //        return Ok(_updateLocation);
        //    }

        //    if (_updateLocation.Success == false && _updateLocation.Message == "RepoError")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating location {locationDTO}");
        //        return StatusCode(500, ModelState);
        //    }

        //    if (_updateLocation.Success == false && _updateLocation.Message == "Error")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating location  {locationDTO}");
        //        return StatusCode(500, ModelState);
        //    }

        //    return Ok(_updateLocation);
        //}

        //[HttpDelete("{id:Guid}", Name = "DeleteLocation")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        //[ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> DeleteLocation(Guid id)
        //{

        //    var _deleteLocation = await _locationService.SoftDeleteLocationAsync(id);


        //    if (_deleteLocation.Success == false && _deleteLocation.Data == "NotFound")
        //    {
        //        ModelState.AddModelError("", "Location Not found");
        //        return StatusCode(404, ModelState);
        //    }

        //    if (_deleteLocation.Success == false && _deleteLocation.Data == "RepoError")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in Repository when deleting Location");
        //        return StatusCode(500, ModelState);
        //    }

        //    if (_deleteLocation.Success == false && _deleteLocation.Data == "Error")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in service layer when deleting Location");
        //        return StatusCode(500, ModelState);
        //    }

        //    return NoContent();

        //}
    }
}

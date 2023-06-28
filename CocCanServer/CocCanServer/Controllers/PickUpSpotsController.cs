using CocCanService.DTOs.PickUpSpot;
using CocCanService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocCanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickUpSpotsController : ControllerBase
    {
        private readonly IPickUpSpotService _pickUpSpotService;

        public PickUpSpotsController(IPickUpSpotService pickUpSpotService)
        {
            _pickUpSpotService = pickUpSpotService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PickUpSpotDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var pickUpSpot = await _pickUpSpotService.GetAllPickUpSpotsAsync();
            return Ok(pickUpSpot);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PickUpSpotDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PickUpSpotDTO>> CreatePickUpSpot([FromBody] CreatePickUpSpotDTO createPickUpSpotDTO)
        {
            if (createPickUpSpotDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _newPickUpSpot = await _pickUpSpotService.CreatePickUpSpotAsync(createPickUpSpotDTO);
            if (_newPickUpSpot.Status == false && _newPickUpSpot.Title == "RepoError")
            {
                foreach (string error in _newPickUpSpot.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_newPickUpSpot.Status == false && _newPickUpSpot.Title == "Error")
            {
                foreach (string error in _newPickUpSpot.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }
            return Ok(_newPickUpSpot);
        }

        [HttpPatch("{id:Guid}", Name = "UpdatePickUpSpot")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePickUpSpot(Guid id, [FromBody] PickUpSpotDTO pickUpSpotDTO)
        {
            if (pickUpSpotDTO == null || pickUpSpotDTO.Id != id)
            {
                return BadRequest(ModelState);
            }


            var _updatePickUpSpot = await _pickUpSpotService.UpdatePickUpSpotAsync(pickUpSpotDTO);
            if (_updatePickUpSpot.Status == false && _updatePickUpSpot.Title == "RepoError")
            {
                foreach (string error in _updatePickUpSpot.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_updatePickUpSpot.Status == false && _updatePickUpSpot.Title == "Error")
            {
                foreach (string error in _updatePickUpSpot.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }
            return Ok(_updatePickUpSpot);
        }

        [HttpDelete("{id:Guid}", Name = "DeletePickUpSpot")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePickUpSpot(Guid id)
        {

            var _deletePickUpSpot = await _pickUpSpotService.SoftDeletePickUpSpotAsync(id);


            if (_deletePickUpSpot.Status == false && _deletePickUpSpot.Title == "RepoError")
            {
                foreach (string error in _deletePickUpSpot.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_deletePickUpSpot.Status == false && _deletePickUpSpot.Title == "Error")
            {
                foreach (string error in _deletePickUpSpot.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}

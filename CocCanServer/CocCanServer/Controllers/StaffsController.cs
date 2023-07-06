using CocCanService.DTOs.Staff;
using CocCanService.Services;
using CocCanService.Services.Imp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocCanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffsController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StaffDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var staffs = await _staffService.GetAllStaffsAsync();
            return Ok(staffs);
        }

        //[HttpPost("Authen")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StaffDTO))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult<StaffDTO>> CheckStaffLogin(string Email, string Password)
        //{
        //    if (Email == "" || Password == "" || Email == null || Password == null)
        //    {
        //        return BadRequest();
        //    }
        //    var StaffFound = await _staffService.CheckStaffLoginsAsync(Email, Password);

        //    if (StaffFound.Data == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(StaffFound);

        //}

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StaffDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<StaffDTO>> CreateStaff([FromBody] CreateStaffDTO createStaffDTO)
        {
            if (createStaffDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _newStaff = await _staffService.CreateStaffAsync(createStaffDTO);

            if (_newStaff.Status == false && _newStaff.Title == "Exist")
            {
                return Ok(_newStaff);
            }


            if (_newStaff.Status == false && _newStaff.Title == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding staff {createStaffDTO}");
                return StatusCode(500, ModelState);
            }

            if (_newStaff.Status == false && _newStaff.Title == "Error")
            {   
                ModelState.AddModelError("", $"Some thing went wrong in service layer when adding staff {createStaffDTO}");
                return StatusCode(500, ModelState);
            }

            //Return new Staff created
            return Ok(_newStaff);
        }

        [HttpPatch("{id:Guid}", Name = "UpdateStaff")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStaff(Guid id, [FromBody] StaffDTO staffDTO)
        {
            if (staffDTO == null || staffDTO.Id != id)
            {
                return BadRequest(ModelState);
            }


            var _updateStaff = await _staffService.UpdateStaffAsync(staffDTO);

            if (_updateStaff.Status == false && _updateStaff.Title == "NotFound")
            {
                return Ok(_updateStaff);
            }

            if (_updateStaff.Status == false && _updateStaff.Title == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating Staff {staffDTO}");
                return StatusCode(500, ModelState);
            }

            if (_updateStaff.Status == false && _updateStaff.Title == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when updating Staff {staffDTO}");
                return StatusCode(500, ModelState);
            }

            return Ok(_updateStaff);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteStaff")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteStaff(Guid id)
        {

            var _deleteStaff = await _staffService.SoftDeleteStaffAsync(id);

            if (_deleteStaff.Status == false && _deleteStaff.Title == "RepoError")
            {
                foreach (string error in _deleteStaff.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_deleteStaff.Status == false && _deleteStaff.Title == "Error")
            {
                foreach (string error in _deleteStaff.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
    }
}

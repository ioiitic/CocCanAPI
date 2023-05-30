using CocCanService.DTOs.Staff;
using CocCanService.Services;
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
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StaffDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _staffService.GetAllStaffsAsync();
            return Ok(companies);
        }

        [HttpPost("Authen")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StaffDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<StaffDTO>> CheckStaffLogin(string Email, string Password)
        {
            if (Email == "" || Password == "" || Email == null || Password == null)
            {
                return BadRequest();
            }
            var StaffFound = await _staffService.CheckStaffLoginsAsync(Email, Password);

            if (StaffFound.Data == null)
            {
                return NotFound();
            }

            return Ok(StaffFound);

        }

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

            if (_newStaff.Success == false && _newStaff.Message == "Exist")
            {
                return Ok(_newStaff);
            }


            if (_newStaff.Success == false && _newStaff.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding staff {createStaffDTO}");
                return StatusCode(500, ModelState);
            }

            if (_newStaff.Success == false && _newStaff.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when adding staff {createStaffDTO}");
                return StatusCode(500, ModelState);
            }

            //Return new company created
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


            var _updateCompany = await _staffService.UpdateStaffAsync(staffDTO);

            if (_updateCompany.Success == false && _updateCompany.Message == "NotFound")
            {
                return Ok(_updateCompany);
            }

            if (_updateCompany.Success == false && _updateCompany.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating company {staffDTO}");
                return StatusCode(500, ModelState);
            }

            if (_updateCompany.Success == false && _updateCompany.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when updating company {staffDTO}");
                return StatusCode(500, ModelState);
            } 

            return Ok(_updateCompany);
        }
    }
}

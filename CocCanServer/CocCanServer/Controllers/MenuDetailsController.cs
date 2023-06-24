using CocCanService.DTOs.Menu;
using CocCanService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CocCanService.DTOs.MenuDetail;

namespace CocCanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuDetailsController : ControllerBase
    {
        private readonly IMenuDetailService _menuDetailService;

        public MenuDetailsController(IMenuDetailService menuDetailService)
        {
            _menuDetailService = menuDetailService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MenuDetailDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var menuDetail = await _menuDetailService.GetAllMenuDetailsAsync();
            return Ok(menuDetail);
        }

        [HttpPost("{Guid:id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MenuDetailDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<MenuDetailDTO>> CreateMenuDetail(Guid id, [FromBody] List<CreateMenuDetailDTO> createMenuDetailDTO)
        {
            if (createMenuDetailDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _newMenuDetail = await _menuDetailService.CreateMenuDetailAsync(id, createMenuDetailDTO);

            if (_newMenuDetail.Status == false && _newMenuDetail.Title == "RepoError")
            {
                foreach (string error in _newMenuDetail.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_newMenuDetail.Status == false && _newMenuDetail.Title == "Error")
            {
                foreach (string error in _newMenuDetail.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }
            return Ok(_newMenuDetail);
        }

        [HttpPatch("{id:Guid}", Name = "UpdateMenuDetail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMenuDetail(Guid id, [FromBody] MenuDetailDTO menuDetailDTO)
        {
            if (menuDetailDTO == null || menuDetailDTO.Id != id)
            {
                return BadRequest(ModelState);
            }


            var _updateMenuDetail = await _menuDetailService.UpdateMenuDetailAsync(menuDetailDTO);

            if (_updateMenuDetail.Status == false && _updateMenuDetail.Title == "RepoError")
            {
                foreach (string error in _updateMenuDetail.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_updateMenuDetail.Status == false && _updateMenuDetail.Title == "Error")
            {
                foreach (string error in _updateMenuDetail.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            return Ok(_updateMenuDetail);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteMenuDetail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMenuDetail(Guid id)
        {

            var _deleteMenuDetail = await _menuDetailService.HardDeleteMenuDetailAsync(id);

            if (_deleteMenuDetail.Status == false && _deleteMenuDetail.Title == "RepoError")
            {
                foreach (string error in _deleteMenuDetail.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_deleteMenuDetail.Status == false && _deleteMenuDetail.Title == "Error")
            {
                foreach (string error in _deleteMenuDetail.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}

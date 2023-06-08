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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MenuDetailDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<MenuDetailDTO>> CreateMenuDetail([FromBody] CreateMenuDetailDTO createMenuDetailDTO)
        {
            if (createMenuDetailDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _newMenuDetail = await _menuDetailService.CreateMenuDetailAsync(createMenuDetailDTO);

            if (_newMenuDetail.Success == false && _newMenuDetail.Message == "Exist")
            {
                return Ok(_newMenuDetail);
            }


            if (_newMenuDetail.Success == false && _newMenuDetail.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding menu detail {createMenuDetailDTO}");
                return StatusCode(500, ModelState);
            }

            if (_newMenuDetail.Success == false && _newMenuDetail.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding menu detail  {createMenuDetailDTO}");
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

            if (_updateMenuDetail.Success == false && _updateMenuDetail.Message == "NotFound")
            {
                return Ok(_updateMenuDetail);
            }

            if (_updateMenuDetail.Success == false && _updateMenuDetail.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating menu detail {menuDetailDTO}");
                return StatusCode(500, ModelState);
            }

            if (_updateMenuDetail.Success == false && _updateMenuDetail.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating menu detail {menuDetailDTO}");
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


            if (_deleteMenuDetail.Success == false && _deleteMenuDetail.Data == "NotFound")
            {
                ModelState.AddModelError("", "Menu Not found");
                return StatusCode(404, ModelState);
            }

            if (_deleteMenuDetail.Success == false && _deleteMenuDetail.Data == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in Repository when deleting menu detail");
                return StatusCode(500, ModelState);
            }

            if (_deleteMenuDetail.Success == false && _deleteMenuDetail.Data == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when deleting menu detail");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}

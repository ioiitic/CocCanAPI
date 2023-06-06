using CocCanService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CocCanService.DTOs.Menu;

namespace CocCanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class menusController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public menusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MenuDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var menu = await _menuService.GetAllMenusAsync();
            return Ok(menu);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MenuDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<MenuDTO>> CreateMenu([FromBody] CreateMenuDTO createMenuDTO)
        {
            if (createMenuDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _newMenu = await _menuService.CreateMenuAsync(createMenuDTO);

            if (_newMenu.Success == false && _newMenu.Message == "Exist")
            {
                return Ok(_newMenu);
            }


            if (_newMenu.Success == false && _newMenu.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding menu {createMenuDTO}");
                return StatusCode(500, ModelState);
            }

            if (_newMenu.Success == false && _newMenu.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding menu  {createMenuDTO}");
                return StatusCode(500, ModelState);
            }
            return Ok(_newMenu);
        }

        [HttpPatch("{id:Guid}", Name = "UpdateMenu")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMenu(Guid id, [FromBody] MenuDTO menuDTO)
        {
            if (menuDTO == null || menuDTO.Id != id)
            {
                return BadRequest(ModelState);
            }


            var _updateMenu = await _menuService.UpdateMenuAsync(menuDTO);

            if (_updateMenu.Success == false && _updateMenu.Message == "NotFound")
            {
                return Ok(_updateMenu);
            }

            if (_updateMenu.Success == false && _updateMenu.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating menu {menuDTO}");
                return StatusCode(500, ModelState);
            }

            if (_updateMenu.Success == false && _updateMenu.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating menu  {menuDTO}");
                return StatusCode(500, ModelState);
            }

            return Ok(_updateMenu);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteMenu")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMenu(Guid id)
        {

            var _deleteMenu = await _menuService.SoftDeleteMenuAsync(id);


            if (_deleteMenu.Success == false && _deleteMenu.Data == "NotFound")
            {
                ModelState.AddModelError("", "Menu Not found");
                return StatusCode(404, ModelState);
            }

            if (_deleteMenu.Success == false && _deleteMenu.Data == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in Repository when deleting menu");
                return StatusCode(500, ModelState);
            }

            if (_deleteMenu.Success == false && _deleteMenu.Data == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when deleting menu");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}

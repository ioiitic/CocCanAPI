﻿using CocCanService.Services;
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
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenusController(IMenuService menuService)
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

            if (_newMenu.Status == false && _newMenu.Title == "RepoError")
            {
                foreach (string error in _newMenu.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_newMenu.Status == false && _newMenu.Title == "Error")
            {
                foreach (string error in _newMenu.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
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

            if (_updateMenu.Status == false && _updateMenu.Title == "RepoError")
            {
                foreach (string error in _updateMenu.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_updateMenu.Status == false && _updateMenu.Title == "Error")
            {
                foreach (string error in _updateMenu.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
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

            if (_deleteMenu.Status == false && _deleteMenu.Title == "RepoError")
            {
                foreach (string error in _deleteMenu.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_deleteMenu.Status == false && _deleteMenu.Title == "Error")
            {
                foreach (string error in _deleteMenu.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}

using CocCanService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Repository.Entities;
using CocCanService.DTOs.Category;

namespace CocCanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryDTO>))]
        //public async Task<IActionResult> GetAll()
        //{
        //    var categories = await _categoryService.GetAllCategoriesAsync();
        //    return Ok(categories);
        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] CreateCategoryDTO createCategoryDTO)
        //{
        //    if (createCategoryDTO == null)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!ModelState.IsValid) { return BadRequest(ModelState); }

        //    var _newCategory = await _categoryService.CreateCategoryAsync(createCategoryDTO);

        //    if (_newCategory.Success == false && _newCategory.Message == "Exist")
        //    {
        //        return Ok(_newCategory);
        //    }


        //    if (_newCategory.Success == false && _newCategory.Message == "RepoError")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding category {createCategoryDTO}");
        //        return StatusCode(500, ModelState);
        //    }

        //    if (_newCategory.Success == false && _newCategory.Message == "Error")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in service layer when adding category {createCategoryDTO}");
        //        return StatusCode(500, ModelState);
        //    }
        //    return Ok(_newCategory);
        //}

        //[HttpPatch("{id:Guid}", Name = "UpdateCategory")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryDTO categoryDTO)
        //{
        //    if (categoryDTO == null || categoryDTO.Id != id)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    var _updateCategory = await _categoryService.UpdateCategoryAsync(categoryDTO);

        //    if (_updateCategory.Success == false && _updateCategory.Message == "NotFound")
        //    {
        //        return Ok(_updateCategory);
        //    }

        //    if (_updateCategory.Success == false && _updateCategory.Message == "RepoError")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating category {categoryDTO}");
        //        return StatusCode(500, ModelState);
        //    }

        //    if (_updateCategory.Success == false && _updateCategory.Message == "Error")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating category {categoryDTO}");
        //        return StatusCode(500, ModelState);
        //    }

        //    return Ok(_updateCategory);
        //}

        //[HttpDelete("{id:Guid}", Name = "DeleteCategory")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        //[ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> DeleteCategory(Guid id)
        //{

        //    var _deleteCategory = await _categoryService.SoftDeleteCategoryAsync(id);


        //    if (_deleteCategory.Success == false && _deleteCategory.Data == "NotFound")
        //    {
        //        ModelState.AddModelError("", "Category Not found");
        //        return StatusCode(404, ModelState);
        //    }

        //    if (_deleteCategory.Success == false && _deleteCategory.Data == "RepoError")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in Repository when deleting category");
        //        return StatusCode(500, ModelState);
        //    }

        //    if (_deleteCategory.Success == false && _deleteCategory.Data == "Error")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in service layer when deleting category");
        //        return StatusCode(500, ModelState);
        //    }

        //    return NoContent();

        //}
    }
}

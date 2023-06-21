using CocCanService.DTOs.Product;
using CocCanService.DTOs.Staff;
using CocCanService.Services;
using CocCanService.Services.Imp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CocCanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SessionDTO>))]
        public async Task<IActionResult> GetAll(string filter, string range, string sort)
        {
            var companies = await _productService.GetAllProductsAsync();
            return Ok(companies);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SessionDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<SessionDTO>> CreateProduct([FromBody] CreateSessionDTO createProductDTO)
        {
            if (createProductDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _newProduct = await _productService.CreateProductAsync(createProductDTO);

            if (_newProduct.Status == false && _newProduct.Title == "RepoError")
            {
                foreach (string error in _newProduct.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_newProduct.Status == false && _newProduct.Title == "Error")
            {
                foreach (string error in _newProduct.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }
            return Ok(_newProduct.Data);
        }

        [HttpPatch("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] SessionDTO productDTO)
        {
            if (productDTO == null || productDTO.Id != id)
            {
                return BadRequest(ModelState);
            }


            var _updateProduct = await _productService.UpdateProductAsync(productDTO);

            if (_updateProduct.Status == false && _updateProduct.Title == "RepoError")
            {
                foreach (string error in _updateProduct.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_updateProduct.Status == false && _updateProduct.Title == "Error")
            {
                foreach (string error in _updateProduct.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }
            return Ok(_updateProduct);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SessionDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesDefaultResponseType]
        public async Task<ActionResult<SessionDTO>> GetByGUID(Guid id)
        {

            if (id == Guid.Empty)
            {
                return BadRequest(id);
            }

            var product = await _productService.GetProductByGUIDAsync(id);

            if (product.Data == null)
            {

                return NotFound();
            }

            return Ok(product);
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {

            var _deleteProduct = await _productService.SoftDeleteProductAsync(id);

            if (_deleteProduct.Status == false && _deleteProduct.Title == "RepoError")
            {
                foreach (string error in _deleteProduct.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_deleteProduct.Status == false && _deleteProduct.Title == "Error")
            {
                foreach (string error in _deleteProduct.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

    }
}

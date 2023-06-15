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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _productService.GetAllProductsAsync();
            return Ok(companies);
        }
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] CreateProductDTO createProductDTO)
        //{
        //    if (createProductDTO == null)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!ModelState.IsValid) { return BadRequest(ModelState); }

        //    var _newProduct = await _productService.CreateProductAsync(createProductDTO);

        //    if (_newProduct.Success == false && _newProduct.Message == "Exist")
        //    {
        //        return Ok(_newProduct);
        //    }


        //    if (_newProduct.Success == false && _newProduct.Message == "RepoError")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding product {createProductDTO}");
        //        return StatusCode(500, ModelState);
        //    }

        //    if (_newProduct.Success == false && _newProduct.Message == "Error")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in service layer when adding product {createProductDTO}");
        //        return StatusCode(500, ModelState);
        //    }

        //    //Return new company created
        //    return Ok(_newProduct);
        //}

        //[HttpPatch("{id:Guid}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductDTO productDTO)
        //{
        //    if (productDTO == null || productDTO.Id != id)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    var _updateProduct = await _productService.UpdateProductAsync(productDTO);

        //    if (_updateProduct.Success == false && _updateProduct.Message == "NotFound")
        //    {
        //        return Ok(_updateProduct);
        //    }

        //    if (_updateProduct.Success == false && _updateProduct.Message == "RepoError")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating product {productDTO}");
        //        return StatusCode(500, ModelState);
        //    }

        //    if (_updateProduct.Success == false && _updateProduct.Message == "Error")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in service layer when updating product {productDTO}");
        //        return StatusCode(500, ModelState);
        //    }

        //    return Ok(_updateProduct);
        //}

        //[HttpGet("{id:Guid}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult<ProductDTO>> GetByGUID(Guid id)
        //{

        //    if (id == Guid.Empty)
        //    {
        //        return BadRequest(id);
        //    }

        //    var product = await _productService.GetProductByGUIDAsync(id);

        //    if (product.Data == null)
        //    {

        //        return NotFound();
        //    }

        //    return Ok(product);
        //}

        //[HttpDelete("{id:Guid}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        //[ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> DeleteCompany(Guid id)
        //{

        //    var _deleteProduct = await _productService.SoftDeleteProductAsync(id);

        //    if (_deleteProduct.Success == false && _deleteProduct.Message == "NotFound")
        //    {
        //        ModelState.AddModelError("", "Product Not found");
        //        return StatusCode(404, ModelState);
        //    }

        //    if (_deleteProduct.Success == false && _deleteProduct.Message == "RepoError")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in Repository when deleting product");
        //        return StatusCode(500, ModelState);
        //    }

        //    if (_deleteProduct.Success == false && _deleteProduct.Message == "Error")
        //    {
        //        ModelState.AddModelError("", $"Some thing went wrong in service layer when deleting product");
        //        return StatusCode(500, ModelState);
        //    }

        //    return NoContent();

        //}

    }
}

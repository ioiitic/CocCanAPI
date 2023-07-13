using CocCanService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CocCanService.DTOs.Batch;

namespace CocCanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchesController : ControllerBase
    {
        private readonly IBatchService _BatchService;

        public BatchesController(IBatchService batchService)
        {
            _BatchService = batchService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BatchDTO>))]
        public async Task<IActionResult> GetAll(string filter, string range, string sort)
        {
            var batch = await _BatchService.GetAllBatchesAsync();
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            HttpContext.Response.Headers.Add("Content-Range", "batchs 0-1/2");
            return Ok(batch.Data);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BatchDTO>))]
        public async Task<IActionResult> GetBatchByIdAll(Guid id)
        {
            var orderDetail = await _BatchService.GetBatchByIdAsync(id);
            return Ok(orderDetail.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BatchDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BatchDTO>> CreateBatch([FromBody] CreateBatchDTO createBatchDTO)
        {
            if (createBatchDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _newBatch = await _BatchService.CreateBatchAsync(createBatchDTO);

            if (_newBatch.Status == false && _newBatch.Title == "RepoError")
            {
                foreach (string error in _newBatch.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_newBatch.Status == false && _newBatch.Title == "Error")
            {
                foreach (string error in _newBatch.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            return Ok(_newBatch);
        }

        [HttpPut("{id:Guid}", Name = "UpdateBatch")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBatch(Guid id, [FromBody] UpdateBatchDTO updateBatchDTO)
        {
            var _updateBatch = await _BatchService.UpdateBatchAsync(id, updateBatchDTO);

            if (_updateBatch.Status == false && _updateBatch.Title == "RepoError")
            {
                foreach (string error in _updateBatch.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_updateBatch.Status == false && _updateBatch.Title == "Error")
            {
                foreach (string error in _updateBatch.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            return Ok(_updateBatch);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteBatch")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBatch(Guid id)
        {

            var _deleteBatch = await _BatchService.SoftDeleteBatchAsync(id);

            if (_deleteBatch.Status == false && _deleteBatch.Title == "RepoError")
            {
                foreach (string error in _deleteBatch.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            if (_deleteBatch.Status == false && _deleteBatch.Title == "Error")
            {
                foreach (string error in _deleteBatch.ErrorMessages)
                {
                    ModelState.AddModelError("", error);
                }
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}

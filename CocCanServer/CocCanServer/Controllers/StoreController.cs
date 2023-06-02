using CocCanService.DTOs.Store;
using CocCanService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace CocCanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StoreDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var stores = await _storeService.GetAllStoresAsync();
            return Ok(stores);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StoreDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<StoreDTO>> CreateStore([FromBody] CreateStoreDTO createStoreDTO)
        {
            if (createStoreDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var _newStore = await _storeService.CreateStoreAsync(createStoreDTO);

            if (_newStore.Success == false && _newStore.Message == "Exist")
            {
                return Ok(_newStore);
            }


            if (_newStore.Success == false && _newStore.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when adding store {createStoreDTO}");
                return StatusCode(500, ModelState);
            }

            if (_newStore.Success == false && _newStore.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when adding store {createStoreDTO}");
                return StatusCode(500, ModelState);
            }
            return Ok(_newStore);
        }

        [HttpPatch("{id:Guid}", Name = "UpdateStore")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStore(Guid id, [FromBody] StoreDTO storeDTO)
        {
            if (storeDTO == null || storeDTO.Id != id)
            {
                return BadRequest(ModelState);
            }


            var _updateStore = await _storeService.UpdateStoreAsync(storeDTO);

            if (_updateStore.Success == false && _updateStore.Message == "NotFound")
            {
                return Ok(_updateStore);
            }

            if (_updateStore.Success == false && _updateStore.Message == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating Store {storeDTO}");
                return StatusCode(500, ModelState);
            }

            if (_updateStore.Success == false && _updateStore.Message == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in respository layer when updating Store  {storeDTO}");
                return StatusCode(500, ModelState);
            }

            return Ok(_updateStore);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteStore")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Can not be removed 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteStore(Guid id)
        {

            var _deleteStore = await _storeService.SoftDeleteStoreAsync(id);


            if (_deleteStore.Success == false && _deleteStore.Data == "NotFound")
            {
                ModelState.AddModelError("", "Store Not found");
                return StatusCode(404, ModelState);
            }

            if (_deleteStore.Success == false && _deleteStore.Data == "RepoError")
            {
                ModelState.AddModelError("", $"Some thing went wrong in Repository when deleting Store");
                return StatusCode(500, ModelState);
            }

            if (_deleteStore.Success == false && _deleteStore.Data == "Error")
            {
                ModelState.AddModelError("", $"Some thing went wrong in service layer when deleting store");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StoreDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesDefaultResponseType]
        public async Task<ActionResult<StoreDTO>> GetByGUID(Guid id)
        {

            if (id == Guid.Empty)
            {
                return BadRequest(id);
            }

            var company = await _storeService.GetStoreByIdAsync(id);

            if (company.Data == null)
            {

                return NotFound();
            }

            return Ok(company);
        }
    }
}

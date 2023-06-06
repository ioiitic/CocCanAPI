using AutoMapper;
using CocCanService.DTOs.Category;
using Repository.Entities;
using Repository.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services.Imp
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepo, IMapper mapper)
        {
            this._categoryRepo = categoryRepo;
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<CategoryDTO>> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            ServiceResponse<CategoryDTO> _response = new();
            try
            {
                Repository.Entities.Category _newCategory = new()
                {
                    Id = Guid.NewGuid(),
                    Name = createCategoryDTO.Name,
                    Status = createCategoryDTO.Status
                };

                if (!await _categoryRepo.CreateCategoryAsync(_newCategory))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }

                _response.Success = true;
                _response.Message = "Created";
                _response.Data = _mapper.Map<CategoryDTO>(_newCategory);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return _response;
        }

        public async Task<ServiceResponse<List<CategoryDTO>>> GetAllCategoriesAsync()
        {
            ServiceResponse<List<CategoryDTO>> _response = new();
            try
            {
                var _CategoryList = await _categoryRepo.GetAllCategoriesAsync();

                var _CategoryListDTO = new List<CategoryDTO>();

                foreach (var item in _CategoryList)
                {
                    _CategoryListDTO.Add(_mapper.Map<CategoryDTO>(item));
                }

                _response.Success = true;
                _response.Data = _CategoryListDTO;
                _response.Message = "OK";
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return _response;
        }

        public async Task<ServiceResponse<CategoryDTO>> GetCategoryByIdAsync(Guid id)
        {
            ServiceResponse<CategoryDTO> _response = new();
            try
            {
                var _CategoryList = await _categoryRepo.GetCategoryByGUIDAsync(id);

                if (_CategoryList == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }

                var _CategoryDto = _mapper.Map<CategoryDTO>(_CategoryList);

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _CategoryDto;

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return _response;
        }

        public async Task<ServiceResponse<string>> SoftDeleteCategoryAsync(Guid id)
        {
            ServiceResponse<string> _response = new();
            try
            {
                var _existingCategory = await _categoryRepo.GetCategoryByGUIDAsync(id);

                if (_existingCategory == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;
                }

                if (!await _categoryRepo.SoftDeleteCategoryAsync(id))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    return _response;
                }


                _response.Success = true;
                _response.Message = "SoftDeleted";
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return _response;
        }

        public async Task<ServiceResponse<CategoryDTO>> UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            ServiceResponse<CategoryDTO> _response = new();
            try
            {
                var _existingCategory = await _categoryRepo.GetCategoryByGUIDAsync(categoryDTO.Id);

                if (_existingCategory == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;
                }

                _existingCategory.Name = categoryDTO.Name;
                _existingCategory.Status = categoryDTO.Status;

                if (!await _categoryRepo.UpdateCategoryAsync(_existingCategory))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }


                _response.Success = true;
                _response.Message = "Updated";
                _response.Data = _mapper.Map<CategoryDTO>(_existingCategory);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return _response;
        }
    }
}

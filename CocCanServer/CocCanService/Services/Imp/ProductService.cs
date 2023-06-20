using AutoMapper;
using CocCanService.DTOs.Category;
using CocCanService.DTOs.Product;
using CocCanService.DTOs.Staff;
using Repository.Entities;
using Repository.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services.Imp
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ProductDTO>> CreateProductAsync(CreateProductDTO createProductDTO)
        {
            ServiceResponse<ProductDTO> _response = new();
            try
            {
                var _newProduct = _mapper.Map<Product>(createProductDTO);
                if (!await _productRepo.CreateProductAsync(_newProduct))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Category Repository when trying to create store!");
                    _response.Data = null;
                    return _response;
                }

                _response.Status = true;
                _response.Title = "Created";
                _response.Data = _mapper.Map<ProductDTO>(_newProduct);
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Title = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Data = null;
            }
            return _response;
        }

        public async Task<ServiceResponse<List<ProductDTO>>> GetAllProductsAsync()
        {
            ServiceResponse<List<ProductDTO>> _response = new();
            try
            {
                var _SProductList = await _productRepo.GetAllProductsAsync();

                var _SProductListDTO = new List<ProductDTO>();

                foreach (var item in _SProductList)
                {
                    _SProductListDTO.Add(_mapper.Map<ProductDTO>(item));
                }

                _response.Status = true;
                _response.Title = "Got all products";
                _response.Data = _SProductListDTO;
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Title = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Data = null;
            }
            return _response;
        }

        public async Task<ServiceResponse<ProductDTO>> GetProductByGUIDAsync(Guid id)
        {
            ServiceResponse<ProductDTO> _response = new();

            try
            {

                var _product = await _productRepo.GetProductByGUIDAsync(id);

                if (_product == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                var _ProductDto = _mapper.Map<ProductDTO>(_product);

                _response.Status = true;
                _response.Title = "Got Product";
                _response.Data = _ProductDto;


            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Title = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Data = null;
            }

            return _response;
        }

        public Task<ServiceResponse<bool>> HardDeleteProductAsync(ProductDTO productDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<string>> SoftDeleteProductAsync(Guid id)
        {
            ServiceResponse<string> _response = new();
            try
            {
                var _existingProduct = await _productRepo.GetProductByGUIDAsync(id);
                if (_existingProduct == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                if (!await _productRepo.SoftDeleteProductAsync(id))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Store Repository when trying to delete category!");
                    _response.Data = null;
                    return _response;
                }


                _response.Status = true;
                _response.Title = "Deleted product";
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Title = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Data = null;
            }
            return _response;
        }

        public async Task<ServiceResponse<ProductDTO>> UpdateProductAsync(ProductDTO productDTO)
        {
            ServiceResponse<ProductDTO> _response = new();
            try
            {
                var _existingProduct = await _productRepo.GetProductByGUIDAsync(productDTO.Id)
                    ;
                if (_existingProduct == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                _existingProduct.Name = productDTO.Name;
                _existingProduct.Image = productDTO.Image;

                if (!await _productRepo.UpdateProductAsync(_existingProduct))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Store Repository when trying to update category!");
                    _response.Data = null;
                    return _response;
                }

                _response.Status = true;
                _response.Title = "Updated category";
                _response.Data = _mapper.Map<ProductDTO>(_existingProduct);
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Title = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Data = null;
            }
            return _response;
        }
    }
}

using AutoMapper;
using CocCanService.DTOs.OrderDetail;
using Repository.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services.Imp
{
    public class OrderDetailService:IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepo;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository orderDetailRepo, IMapper mapper)
        {
            this._orderDetailRepo = orderDetailRepo;
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<OrderDetailDTO>> CreateOrderDetailAsync(CreateOrderDetailDTO createOrderDetailDTO)
        {
            ServiceResponse<OrderDetailDTO> _response = new();
            try
            {
                Repository.Entities.OrderDetail _newOrderDetail = new()
                {
                    Id = Guid.NewGuid(),
                    ProductId = createOrderDetailDTO.ProductId,
                    OrderId = createOrderDetailDTO.OrderId,
                    Status = createOrderDetailDTO.Status
                };

                if (!await _orderDetailRepo.CreateOrderDetailAsync(_newOrderDetail))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }

                _response.Success = true;
                _response.Message = "Created";
                _response.Data = _mapper.Map<OrderDetailDTO>(_newOrderDetail);
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

        public async Task<ServiceResponse<List<OrderDetailDTO>>> GetAllOrderDetailsAsync()
        {
            ServiceResponse<List<OrderDetailDTO>> _response = new();
            try
            {
                var _OrderDetailList = await _orderDetailRepo.GetAllOrderDetailsAsync();

                var _OderDetailListDTO = new List<OrderDetailDTO>();

                foreach (var item in _OrderDetailList)
                {
                    _OderDetailListDTO.Add(_mapper.Map<OrderDetailDTO>(item));
                }

                _response.Success = true;
                _response.Data = _OderDetailListDTO;
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
        public async Task<ServiceResponse<OrderDetailDTO>> GetOrderDetailByIdAsync(Guid id)
        {
            ServiceResponse<OrderDetailDTO> _response = new();
            try
            {
                var _orderDetailList = await _orderDetailRepo.GetOrderDetailByGUIDAsync(id);

                if (_orderDetailList == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }

                var _orderDetailDto = _mapper.Map<OrderDetailDTO>(_orderDetailList);

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _orderDetailDto;

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

        public async Task<ServiceResponse<string>> SoftDeleteOrderDetailAsync(Guid id)
        {
            ServiceResponse<string> _response = new();
            try
            {
                var _existingOrderDetail = await _orderDetailRepo.GetOrderDetailByGUIDAsync(id);

                if (_existingOrderDetail == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;
                }

                if (!await _orderDetailRepo.SoftDeleteOrderDetailAsync(id))
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

        public async Task<ServiceResponse<OrderDetailDTO>> UpdateOrderDetailAsync(OrderDetailDTO orderDetailDTO)
        {
            ServiceResponse<OrderDetailDTO> _response = new();
            try
            {
                var _existingOrderDetail = await _orderDetailRepo.GetOrderDetailByGUIDAsync(orderDetailDTO.Id);

                if (_existingOrderDetail == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;
                }
                _existingOrderDetail.ProductId = orderDetailDTO.ProductId;
                _existingOrderDetail.OrderId = orderDetailDTO.OrderId;
                _existingOrderDetail.Status = orderDetailDTO.Status;

                if (!await _orderDetailRepo.UpdateOrderDetailAsync(_existingOrderDetail))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }


                _response.Success = true;
                _response.Message = "Updated";
                _response.Data = _mapper.Map<OrderDetailDTO>(_existingOrderDetail);
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

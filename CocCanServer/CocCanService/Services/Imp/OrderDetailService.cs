using AutoMapper;
using CocCanService.DTOs.OrderDetail;
using Repository.Entities;
using Repository.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services.Imp
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepo;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository orderDetailRepo, IMapper mapper)
        {
            this._orderDetailRepo = orderDetailRepo;
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<List<CreateOrderDetailDTO>>> CreateOrderDetailAsync(Guid orderID, List<CreateOrderDetailDTO> createOrderDetailDTOList)
        {
            ServiceResponse<List<CreateOrderDetailDTO>> _response = new();
            try
            {
                List<CreateOrderDetailDTO> list = new List<CreateOrderDetailDTO>();
                foreach (var orderDetailDTO in createOrderDetailDTOList)
                {
                    var _newOrderDetail = _mapper.Map<OrderDetail>(orderDetailDTO);
                    if (!await _orderDetailRepo.CreateOrderDetailAsync(_newOrderDetail))
                    {
                        _response.Status = false;
                        _response.Title = "Error";
                        _response.ErrorMessages.Add("Some error occur in Category Repository when trying to create store!");
                        _response.Data = null;
                        return _response;
                    }

                    list.Add(orderDetailDTO);
                }

                _response.Status = true;
                _response.Title = "Created";
                _response.Data = list;

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

                _response.Status = true;
                _response.Title = "Got all categories";
                _response.Data = _OderDetailListDTO;
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
        public async Task<ServiceResponse<OrderDetailDTO>> GetOrderDetailByIdAsync(Guid id)
        {
            ServiceResponse<OrderDetailDTO> _response = new();
            try
            {
                var _orderDetailList = await _orderDetailRepo.GetOrderDetailByGUIDAsync(id);

                if (_orderDetailList == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                var _orderDetailDto = _mapper.Map<OrderDetailDTO>(_orderDetailList);

                _response.Status = true;
                _response.Title = "Got store";
                _response.Data = _orderDetailDto;

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

        public async Task<ServiceResponse<string>> SoftDeleteOrderDetailAsync(Guid id)
        {
            ServiceResponse<string> _response = new();
            try
            {
                var _existingOrderDetail = await _orderDetailRepo.GetOrderDetailByGUIDAsync(id);

                if (_existingOrderDetail == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                if (!await _orderDetailRepo.SoftDeleteOrderDetailAsync(id))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Store Repository when trying to delete category!");
                    _response.Data = null;
                    return _response;
                }

                _response.Status = true;
                _response.Title = "Deleted category";
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

        public async Task<ServiceResponse<OrderDetailDTO>> UpdateOrderDetailAsync(Guid id, UpdateOrderDetailDTO updateorderDetailDTO)
        {
            ServiceResponse<OrderDetailDTO> _response = new();
            try
            {
                var _existingOrderDetail = await _orderDetailRepo.GetOrderDetailByGUIDAsync(id);

                if (_existingOrderDetail == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                _existingOrderDetail = _mapper.Map<OrderDetail>(updateorderDetailDTO);

                if (!await _orderDetailRepo.UpdateOrderDetailAsync(_existingOrderDetail))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Store Repository when trying to update category!");
                    _response.Data = null;
                    return _response;
                }

                _response.Status = true;
                _response.Title = "Updated category";
                _response.Data = _mapper.Map<OrderDetailDTO>(_existingOrderDetail);
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

using AutoMapper;
using CocCanService.DTOs.Order;
using Repository.Entities;
using Repository.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services.Imp
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;      
        private readonly IMapper _mapper;
        
        public OrderService(IOrderRepository orderRepo, IMapper mapper)
        {
            this._orderRepo = orderRepo;
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<OrderDTO>> CreateOrderAsync(CreateOrderDTO createOrderDTO)
        {            
            ServiceResponse<OrderDTO> _response = new();
            try
            {
                Repository.Entities.Order _newOrder = new()
                {
                    Id = Guid.NewGuid(),
                    OrderTime = createOrderDTO.OrderTime,
                    ServiceFee = createOrderDTO.ServiceFee,
                    TotalPrice = createOrderDTO.TotalPrice,
                    CustomerId = createOrderDTO.CustomerId,
                    SessionId = createOrderDTO.SessionId,
                    PickUpSpotId = createOrderDTO.PickUpSpotId,
                    Status = createOrderDTO.Status
                };

                if (!await _orderRepo.CreateOrderAsync(_newOrder))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Category Repository when trying to create store!");
                    _response.Data = null;
                    return _response;
                }

                _response.Status = true;
                _response.Title = "Created";
                _response.Data = _mapper.Map<OrderDTO>(_newOrder);
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

        public async Task<ServiceResponse<List<OrderDTO>>> GetAllOrdersAsync()
        {
            ServiceResponse<List<OrderDTO>> _response = new();
            try
            {
                var _OrderList = await _orderRepo.GetAllOrdersAsync();

                var _OderListDTO = new List<OrderDTO>();

                foreach (var item in _OrderList)
                {
                    _OderListDTO.Add(_mapper.Map<OrderDTO>(item));
                }

                _response.Status = true;
                _response.Title = "Got all categories";
                _response.Data = _OderListDTO;
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
        public async Task<ServiceResponse<OrderDTO>> GetOrderByIdAsync(Guid id)
        {
            ServiceResponse<OrderDTO> _response = new();
            try
            {
                var _orderList = await _orderRepo.GetOrderByGUIDAsync(id);

                if (_orderList == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                var _orderDto = _mapper.Map<OrderDTO>(_orderList);

                _response.Status = true;
                _response.Title = "Got store";
                _response.Data = _orderDto;

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

        public async Task<ServiceResponse<string>> SoftDeleteOrderAsync(Guid id)
        {
            ServiceResponse<string> _response = new();
            try
            {
                var _existingOrder = await _orderRepo.GetOrderByGUIDAsync(id);

                if (_existingOrder == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                if (!await _orderRepo.SoftDeleteOrderAsync(id))
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

        public async Task<ServiceResponse<OrderDTO>> UpdateOrderAsync(OrderDTO orderDTO)
        {
            ServiceResponse<OrderDTO> _response = new();
            try
            {
                var _existingOrder = await _orderRepo.GetOrderByGUIDAsync(orderDTO.Id);

                if (_existingOrder == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }
                _existingOrder.OrderTime = orderDTO.OrderTime;
                _existingOrder.ServiceFee = orderDTO.ServiceFee;
                _existingOrder.TotalPrice = orderDTO.TotalPrice;
                _existingOrder.CustomerId = orderDTO.CustomerId;
                _existingOrder.SessionId = orderDTO.SessionId;
                _existingOrder.PickUpSpotId = orderDTO.PickUpSpotId;
                _existingOrder.Status = orderDTO.Status;

                if (!await _orderRepo.UpdateOrderAsync(_existingOrder))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Store Repository when trying to update category!");
                    _response.Data = null;
                    return _response;
                }

                _response.Status = true;
                _response.Title = "Updated category";
                _response.Data = _mapper.Map<OrderDTO>(_existingOrder);
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

using CocCanService.DTOs.OrderDetail;
using CocCanService.Services.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services
{
    public interface IOrderDetailService
    {
        Task<ServiceResponse<List<DTOs.OrderDetail.OrderDetailDTO>>> GetAllOrderDetailsAsync();
        Task<ServiceResponse<DTOs.OrderDetail.OrderDetailDTO>> CreateOrderDetailAsync(Guid oderID, List<CreateOrderDetailDTO> createOrderDetailDTOList);
        Task<ServiceResponse<DTOs.OrderDetail.OrderDetailDTO>> UpdateOrderDetailAsync(OrderDetailDTO orderDetailDTO);
        Task<ServiceResponse<DTOs.OrderDetail.OrderDetailDTO>> GetOrderDetailByIdAsync(Guid id);
        Task<ServiceResponse<string>> SoftDeleteOrderDetailAsync(Guid id);
    }
}

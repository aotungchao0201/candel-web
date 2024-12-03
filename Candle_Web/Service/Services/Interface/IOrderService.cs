using Model.Models;
using Service.Modals;
using Service.Modals.Request;
using Service.Modals.Respond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interface
{
    public interface IOrderService
    {
        Task<OrderDTO> createOrder(OrderDTO order);
        Task<OrderDTO> UpdateOrderQuantityAsync(int orderId, OrderDTO order);
        Task<bool> UpdateStatus_Success_OrderService(int orderId, OrderStatusDTO order);
        Task<bool> UpdateStatus_Canceled_OrderService(int orderId, OrderStatusDTO order);
        Task<List<OrderRequestDTO>> GetAllOrderOfUser();
        Task<OrderItem> GetOrderItemByIdOrder(int id);
        Task<List<OrderRequestDTO>> GetOrderByUserId(int id);

        Task<OrderRespondDTO> GetOrderByIdOrder(int id);

        Task<bool> delete(int id);

    }
}

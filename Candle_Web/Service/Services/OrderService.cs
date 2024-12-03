using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repo.Repository;
using Repo.Repository.Interface;
using Service.Enum;
using Service.Modals;
using Service.Modals.Request;
using Service.Modals.Respond;
using Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Service.Modals.OrderDTO;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;
        private readonly candleContext _context;

        public OrderService(IOrderRepo orderRepo, IMapper mapper, candleContext context)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
            _context = context;
        }
        public async Task<OrderDTO> UpdateOrderQuantityAsync(int orderId, OrderDTO orderDto)
        {
            // Find the existing order by OrderId and UserId with Status = "false"
            var existingOrder = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId
                && o.Status == OrderStatusEnum.Pending.ToString()
                && o.UserId == orderDto.UserId);

            if (existingOrder != null)
            {
                // Update quantities of order items
                foreach (var orderItemDto in orderDto.OrderItems)
                {
                    var existingOrderItem = existingOrder.OrderItems
                        .FirstOrDefault(oi => oi.CandleId == orderItemDto.CandleId);

                    if (existingOrderItem != null)
                    {
                        // Update the quantity if it exists
                        existingOrderItem.Quantity += orderItemDto.Quantity; // Set to the new quantity
                    }
                    else
                    {
                        // Optionally add new item if it doesn't exist
                        var newItem = _mapper.Map<OrderItem>(orderItemDto);
                        existingOrder.OrderItems.Add(newItem);
                    }
                }
                existingOrder.TotalPrice = existingOrder.OrderItems.Sum(o => o.Quantity * o.Price);
                // Save changes to the existing order
                await _context.SaveChangesAsync();

                // Map and return the updated order as OrderDTO
                return _mapper.Map<OrderDTO>(existingOrder);
            }
            else
            {
                // If no existing order found, throw an exception or handle as needed
                throw new InvalidOperationException("Order not found or already completed.");
            }
        }

        public async Task<OrderDTO> createOrder(OrderDTO order)
        {


            try
            {
                order.Status = OrderStatusEnum.Pending.ToString();
                order.IsPay = OrderStatusEnum.NotPay.ToString();

                var map = _mapper.Map<Order>(order);
                    var createCandle = await _orderRepo.Create(map);
                    var resutl = _mapper.Map<OrderDTO>(createCandle);
                    return resutl;
                

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        
        public async Task<bool> delete(int id)
        {
            try
            {
                var candle = await _orderRepo.GetOrderItemById(id);
                if (candle == null)
                {
                    throw new Exception($"Order item {id} does not exist");
                }

                await _orderRepo.Delete(candle);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateStatus_Success_OrderService(int orderId, OrderStatusDTO order)
        {
            try
            {
                var data = await _orderRepo.GetByOrderId(orderId);
                if (data != null)
                {
                    order.Status = OrderStatusEnum.Successful.ToString();
                    order.IsPay = OrderStatusEnum.Paid.ToString();

                    _mapper.Map(order, data);
                    await _orderRepo.Update(data);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Fail to update Order {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateStatus_Canceled_OrderService(int orderId, OrderStatusDTO order)
        {
            try
            {
                var data = await _orderRepo.GetByOrderId(orderId);
                if (data != null)
                {
                    order.Status = OrderStatusEnum.Canceled.ToString();
                    order.IsPay = OrderStatusEnum.NotPay.ToString();

                    _mapper.Map(order, data);
                    await _orderRepo.Update(data);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Fail to update Order {ex.Message}");
                return false;
            }
        }

        public async Task<List<OrderRequestDTO>> GetAllOrderOfUser()
        {
            try
            {

                var data = await _orderRepo.GetALL();

                if (!data.Any())
                {
                    return null;
                }

                var map = _mapper.Map<List<OrderRequestDTO>>(data);

                return map;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<OrderRequestDTO>> GetOrderByUserId(int id)
        {
            try
            {

                var data = await _orderRepo.GetByUserId(id);

                if (!data.Any())
                {
                    return null;
                }

                var map = _mapper.Map<List<OrderRequestDTO>>(data);

                return map;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Task<OrderItem> GetOrderItemByIdOrder(int id)
        {
            try
            {
                var data = _orderRepo.GetOrderItemById(id);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderRespondDTO> GetOrderByIdOrder(int id)
        {
            try
            {

                var data = await _orderRepo.GetByOrderId(id);


                var map = _mapper.Map<OrderRespondDTO>(data);

                return map;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

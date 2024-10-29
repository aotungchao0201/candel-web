using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repo.Repository;
using Repo.Repository.Interface;
using Service.Enum;
using Service.Modals;
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
            var existingOrder = await _context.Orders
        .Include(o => o.OrderItems)
        .FirstOrDefaultAsync(o => o.UserId == order.UserId && o.Status == OrderStatusEnum.Pending.ToString());

            if (existingOrder != null)
            {

                existingOrder.TotalPrice += order.TotalPrice; // Update total price based on the provided DTO

                // Update order items
                foreach (var orderItemDto in order.OrderItems)
                {
                    var existingOrderItem = existingOrder.OrderItems
                        .FirstOrDefault(oi => oi.CandleId == orderItemDto.CandleId);

                    if (existingOrderItem != null)
                    {
                        // Increment the quantity if the item already exists
                        existingOrderItem.Quantity += orderItemDto.Quantity;

                        //existingOrderItem.Price = orderItemDto.Price; // Update price ???
                    }
                    else
                    {
                        // Add new order item
                        var newItem = _mapper.Map<OrderItem>(orderItemDto);
                        newItem.OrderId = existingOrder.OrderId; // Set the OrderId for the new item
                        existingOrder.OrderItems.Add(newItem);
                    }
                }
                existingOrder.TotalPrice = existingOrder.OrderItems.Sum(o => o.Quantity * o.Price);

                // Save changes to the existing order
                await _context.SaveChangesAsync();
                return _mapper.Map<OrderDTO>(existingOrder);
            }
            else
            {
                // If no existing order found, create a new order
                var newOrder = _mapper.Map<Order>(order);
                newOrder.CreatedAt = DateTime.Now; // Set created date
                newOrder.Status = OrderStatusEnum.Pending.ToString(); // Set the status for the new order

                newOrder.TotalPrice = order.OrderItems.Sum(item => item.Quantity * item.Price);

                foreach (var orderItemDto in order.OrderItems)
                {
                    var newItem = _mapper.Map<OrderItem>(orderItemDto);
                    newItem.OrderId = newOrder.OrderId; // Set the OrderId for the new item
                    newOrder.OrderItems.Add(newItem);
                }

                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();

                // Map and return the newly created order as OrderDTO
                return _mapper.Map<OrderDTO>(newOrder);
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

        public async Task<bool> UpdateStatus_Success_OrderService(int orderId, OrderDTO order)
        {
            try
            {
                var data = await _orderRepo.GetByOrderId(orderId);
                if (data != null)
                {
                    data.Status = OrderStatusEnum.Successful.ToString();
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

        public async Task<bool> UpdateStatus_Canceled_OrderService(int orderId, OrderDTO order)
        {
            try
            {
                var data = await _orderRepo.GetByOrderId(orderId);
                if (data != null)
                {
                    data.Status = OrderStatusEnum.Canceled.ToString();
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

        public Task<List<Order>> GetAllOrderOfUser()
        {
            try
            {
                var data = _orderRepo.GetALL();
                return data;
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
    }
}

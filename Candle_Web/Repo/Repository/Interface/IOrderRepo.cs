using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repository.Interface
{
    public interface IOrderRepo
    {
        public Task<List<Order>> GetALL();
        public Task<Order> Create(Order order);
        public Task<Order> Update(Order order);
        public Task<bool> Delete(OrderItem order);

        public Task<OrderItem> GetOrderItemById(int id);
        public Task<Order> GetByOrderId(int id);
        public Task<List<Order>> GetByUserId(int id);

        public Task<Order> GetByName(string? order);

    }
}

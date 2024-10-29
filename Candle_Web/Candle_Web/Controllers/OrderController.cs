using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Modals;
using Service.Services.Interface;

namespace Candle_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPut("update-order-quantity/{orderId}")]
        public async Task<IActionResult> UpdateOrderQuantity(int orderId, [FromBody] OrderDTO orderDto)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateOrderQuantityAsync(orderId, orderDto);
                return Ok(updatedOrder); // Return 200 OK with the updated order
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message); // Return 404 Not Found if the order is not found
            }
        }
        [HttpPut("update-order-success/{orderId}")]
        public async Task<IActionResult> UpdateOrderSuccessStatus(int orderId, [FromBody] OrderDTO orderDto)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateStatus_Success_OrderService(orderId, orderDto);
                return Ok(updatedOrder); // Return 200 OK with the updated order
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message); // Return 404 Not Found if the order is not found
            }
        }
        [HttpPut("update-order-canceled/{orderId}")]
        public async Task<IActionResult> UpdateOrderCanceledStatus(int orderId, [FromBody] OrderDTO orderDto)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateStatus_Canceled_OrderService (orderId, orderDto);
                return Ok(updatedOrder); // Return 200 OK with the updated order
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message); // Return 404 Not Found if the order is not found
            }
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDTO orderDto)
        {
            var createdOrder = await _orderService.createOrder(orderDto);
            if (createdOrder == null)
            {
                return BadRequest("Order could not be created.");
            }

            return Ok(createdOrder);

        }

        [HttpGet("get-all-order")]
        public async Task<IActionResult> GetAllOrder()
        {
            var Order = await _orderService.GetAllOrderOfUser();
            if (Order == null)
            {
                return BadRequest("Nothing");
            }

            return Ok(Order);

        }
    }
}

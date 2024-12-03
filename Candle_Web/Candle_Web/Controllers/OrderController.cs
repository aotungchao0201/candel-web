using Azure;
using Candle_Web.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;
using Service.Modals;
using Service.Modals.Respond;
using Service.Services;
using Service.Services.Interface;
using Response = Candle_Web.Types.Response;

namespace Candle_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly PayOS _payOS;

        public OrderController(IOrderService orderService, PayOS payOS)
        {
            _orderService = orderService;
            _payOS = payOS;
        }

        [HttpPut("update-order-success/{orderId}")]
        public async Task<IActionResult> UpdateOrderSuccessStatus(int orderId, OrderStatusDTO orderDto)
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
        public async Task<IActionResult> UpdateOrderCanceledStatus(int orderId, OrderStatusDTO orderDto)
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
        public async Task<IActionResult> CreateOrder(OrderDTO orderDto)
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
        [HttpGet("get-order-id/{id}")]
        public async Task<IActionResult> GetOrderByOrderId(int id)
        {
            try
            {
                var result = await _orderService.GetOrderByIdOrder(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetOrderByUserId(int id)
        {
            try
            {
                var result = await _orderService.GetOrderByUserId(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreatePaymentLink(CreatePaymentLinkRequest body)
        {
            try
            {
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));

                //Tao item list
                List<ItemData> items = new List<ItemData>();

                //set total bang 0
                int totalPrice = 0;

                // Gan gia tri tu chuoi json
                foreach (var orderItem in body.OrderItems)
                {
                    
                    ItemData item = new ItemData(orderItem.productName, orderItem.quantity, orderItem.priceItem);
                    items.Add(item);

                    totalPrice += orderItem.priceItem * orderItem.quantity;
                }

                PaymentData paymentData = new PaymentData(
                    orderCode,
                    totalPrice, // Use the calculated totalPrice here
                    body.description,
                    items,
                    body.cancelUrl,
                    body.returnUrl
                );

                CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

                return Ok(new Response(0, "success", createPayment));
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception);
                return Ok(new Response(-1, "fail", null));
            }
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder([FromRoute] int orderId)
        {
            try
            {
                PaymentLinkInformation paymentLinkInformation = await _payOS.getPaymentLinkInformation(orderId);
                return Ok(new Response(0, "Ok", paymentLinkInformation));
            }
            catch (System.Exception exception)
            {

                Console.WriteLine(exception);
                return Ok(new Response(-1, "fail", null));
            }

        }
        [HttpPut("{orderId}")]
        public async Task<IActionResult> CancelOrder([FromRoute] int orderId)
        {
            try
            {
                PaymentLinkInformation paymentLinkInformation = await _payOS.cancelPaymentLink(orderId);
                return Ok(new Response(0, "Ok", paymentLinkInformation));
            }
            catch (System.Exception exception)
            {

                Console.WriteLine(exception);
                return Ok(new Response(-1, "fail", null));
            }

        }
        [HttpPost("confirm-webhook")]
        public async Task<IActionResult> ConfirmWebhook(ConfirmWebhook body)
        {
            try
            {
                await _payOS.confirmWebhook(body.webhook_url);
                return Ok(new Response(0, "Ok", null));
            }
            catch (System.Exception exception)
            {

                Console.WriteLine(exception);
                return Ok(new Response(-1, "fail", null));
            }

        }
    }

}

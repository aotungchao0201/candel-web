
namespace Candle_Web.Types;

// Record for creating a payment link request
public record CreatePaymentLinkRequest(
    string description,
    int price,
    string returnUrl,
    string cancelUrl,
    List<CreatePaymentLinkRequestProductDTO> OrderItems // List of order items
);

// Record for individual products in the payment link request
public record CreatePaymentLinkRequestProductDTO(

    string productName,
    int quantity,
    int priceItem
);
namespace Licenta.Messaging
{
    public class RabbitMqConstants
    {
        public const string RabbitMqUri = "rabbitmq://localhost/licenta/";
        public const string Username = "licenta";
        public const string Password = "licenta";

        //Services
        public const string   ProductServiceQueue =    "product.service";
        public const string InventoryServiceQueue =  "inventory.service";
        public const string    ReviewServiceQueue =     "review.service";
        public const string InvoicingServiceQueue =  "invoicing.service";
        public const string   LoggingServiceQueue =    "logging.service";
        public const string     OrderServiceQueue =      "order.service";
        public const string  DeliveryServiceQueue =   "delivery.service";

        //Views
        public const string              ProductViewQueue =               "product.view";
        public const string CustomerShoppingCartViewQueue = "customer.shoppingcart.view";
        public const string        CustomerOrderViewQueue =        "customer.order.view";
    }
}

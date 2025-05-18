using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;
using PaypalServerSdk.Standard.Authentication;
using PaypalServerSdk.Standard.Controllers;
using PaypalServerSdk.Standard.Http.Response;
using PaypalServerSdk.Standard.Models;
using PaypalServerSdk.Standard;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Microsoft.Extensions.Configuration;

namespace AlbumsGalore.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly OrdersController _ordersController;
        private readonly PaymentsController _paymentsController;
        private readonly Dictionary<string, CheckoutPaymentIntent> _paymentIntentMap;
        private readonly IConfiguration _configuration;

        private string _paypalClientId =>
            _configuration["PayPal:ClientID"]!;
        private string _paypalClientSecret =>
            _configuration["PayPal:Secret"]!;

        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(IConfiguration configuration, ILogger<CheckoutController> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _paymentIntentMap = new Dictionary<string, CheckoutPaymentIntent> {
              {
                "CAPTURE",
                CheckoutPaymentIntent.Capture
              },
              {
                "AUTHORIZE",
                CheckoutPaymentIntent.Authorize
              }
            };

            _logger.LogInformation("_paypalClientId " + _paypalClientId);
            // Initialize the PayPal SDK client
            PaypalServerSdkClient client = new PaypalServerSdkClient.Builder()
              .Environment(PaypalServerSdk.Standard.Environment.Sandbox)
              .ClientCredentialsAuth(
                new ClientCredentialsAuthModel.Builder(_paypalClientId, _paypalClientSecret).Build()
              )
              .LoggingConfig(config =>
                config
                .LogLevel(LogLevel.Information)
                .RequestConfig(reqConfig => reqConfig.Body(true))
                .ResponseConfig(respConfig => respConfig.Headers(true))
              )
              .Build();

            _ordersController = client.OrdersController;
            _paymentsController = client.PaymentsController;
        }


        [HttpPost("orders")]
        public async Task<IActionResult> CreateOrder([FromBody] dynamic cart)
        {
            try
            {
                Console.WriteLine("Cart Contents: " + cart);
                var result = await _CreateOrder(cart);
                return StatusCode((int)result.StatusCode, result.Data);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to create order:", ex);
                return StatusCode(500, new
                {
                    error = "Failed to create order."
                });
            }
        }

        private async Task<dynamic> _CreateOrder(dynamic cart)
        {
            
            Console.WriteLine("Cart Contents async: " + cart);

            //Tax, shipping

            CreateOrderInput createOrderInput = new CreateOrderInput
                {
                    Body = new OrderRequest
                    {
                        Intent = _paymentIntentMap["CAPTURE"],
                        PurchaseUnits = new List<PurchaseUnitRequest> {
            new PurchaseUnitRequest {
              Amount = new AmountWithBreakdown {
                  CurrencyCode = cart.currency,
                    MValue = cart.totalCost,
                    Breakdown = new AmountBreakdown {
                      ItemTotal = new Money {
                        CurrencyCode = cart.currency,
                          MValue = cart.totalCost,
                      },
                    },
                },
            // lookup item details in `cart` from database
            Items = new List < Item > {
                  new Item {
                    Name = "Shirt",
                      UnitAmount = new Money {
                        CurrencyCode = "USD",
                          MValue = cart.totalCost,
                      },
                      //Tax = new Money {
                      //  CurrencyCode = "USD",
                      //    MValue = "100",
                      //},
                      Quantity = "1",
                      Description = "Super Fresh Shirt",
                      Category = ItemCategory.PhysicalGoods,
                  },
                },

            },

          },

                    },
                };

            ApiResponse<Order> result = await _ordersController.CreateOrderAsync(createOrderInput);
            //_logger.LogInformation("result " + result);
            return result;
        }


        [HttpPost("orders/{orderID}/capture")]
        public async Task<IActionResult> CaptureOrder(string orderID)
        {
            try
            {
                var result = await _CaptureOrder(orderID);
                return StatusCode((int)result.StatusCode, result.Data);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to capture order:", ex);
                return StatusCode(500, new
                {
                    error = "Failed to capture order."
                });
            }
        }

        private async Task<dynamic> _CaptureOrder(string orderID)
        {
            CaptureOrderInput captureOrderInput = new CaptureOrderInput
            {
                Id = orderID,
            };

            ApiResponse<Order> result = await _ordersController.CaptureOrderAsync(captureOrderInput);

            return result;
        }
    }
}

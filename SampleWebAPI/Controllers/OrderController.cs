using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SampleWebAPI.Controllers
{
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        [Route("~/api/orders/")]//To override or 'IGNORE' the RoutePrefix, use ~
        [HttpGet]
        public HttpResponseMessage GetOrders()
        {

            return Request.CreateResponse(HttpStatusCode.OK,
                new List<Order>
                {
                    new Order { Id = 1,ItemName = "Mobile", CustomerId = 1 },
                    new Order { Id = 2, ItemName = "TV", CustomerId = 2 }
                });
        }

        [Route("{customerId:int}")]//When you have two methods with same name, then using 'CONSTRAINTS' we can specify what type it should be
        [HttpGet]
        public IHttpActionResult Get(int customerId)
        {
            return Ok(new List<Order>
                {
                    new Order { Id = 1,ItemName = "Mobile", CustomerId = customerId },
                    new Order { Id = 2, ItemName = "TV", CustomerId = customerId }
                });
        }

        [Route("{customerName:alpha}")]//When you have two methods with same name, then using 'CONSTRAINTS' we can specify what type it should be
        [HttpGet]
        public IHttpActionResult Get(string customerName)
        {
            return Ok(new List<Order>
                {
                    new Order { Id = 1,ItemName = "Mobile", CustomerId = 1 },
                    new Order { Id = 2, ItemName = "TV", CustomerId = 1 }
                });
        }

        [Route("", Name = "GetOrderById")]//NAMING the routes, which can be used to create URL Links to be sent back to the GUI.
        [HttpGet]
        //URL: http://localhost:59540/api/order?customerId=<customerId>&orderId=<orderId>
        public HttpResponseMessage GetOrder(int customerId, int orderId)
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                new Order { Id = orderId, ItemName = "HardDisk", CustomerId = customerId });
        }

        [Route("CreateOrder")]
        [HttpPost]
        [ActionName("CreateOrder")]
        //URL: http://localhost:59540/api/order/CreateOrder
        public HttpResponseMessage Post([FromBody]Order order)
        {
            try
            {
                var response = Request.CreateResponse(HttpStatusCode.OK);
                // Generate a link to the new book and set the Location header in the response.
                string uri = Url.Link("GetOrderById", new { customerId = order.CustomerId, orderId = order.Id, xx = order.Id });
                response.Headers.Location = new Uri(uri);
                JsonMediaTypeFormatter j = new JsonMediaTypeFormatter();
                response.Content = new ObjectContent(typeof(Order), new Order { Id = order.Id, ItemName = order.ItemName, CustomerId = order.CustomerId }, j);
                return response;
                //return Request.CreateResponse(HttpStatusCode.Created, new Order { Id = order.Id, ItemName = order.ItemName, CustomerId = order.CustomerId });
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc);
            }
        }

        [Route("CreateOrders")]
        [HttpPost]
        [ActionName("CreateOrder1")]
        //URL: http://localhost:59540/api/order/CreateOrders
        public HttpResponseMessage Post([FromBody]List<Order> orders)
        {
            try
            {
                var order = orders != null && orders.Count > 0 ? orders[0] : new Order { Id = -1, ItemName = string.Empty, CustomerId = -1 };
                return Request.CreateResponse(HttpStatusCode.Created, new Order { Id = order.Id, ItemName = order.ItemName, CustomerId = order.CustomerId });

            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc);
            }
        }

        [Route("")]
        [HttpDelete]
        //URL: http://localhost:59540/api/order?orderId=<orderId>
        public HttpResponseMessage Delete(int orderId)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc);
            }
        }

        [Route("")]
        [HttpGet]
        //CUSTOM RESULT TYPE
        public OrderResult GetCustomResult(string orderName)
        {
            return new OrderResult(orderName, Request);
        }
    }

    /// <summary>
    /// To return a CUSTOM Result Type
    /// </summary>
    public class OrderResult : IHttpActionResult
    {
        string orderName;
        List<Order> _orders;
        HttpRequestMessage _request;
        JsonMediaTypeFormatter jsonMediaType = new JsonMediaTypeFormatter()
        {
            SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        };


        public OrderResult(string orderName, HttpRequestMessage request)
        {
            //_orders = orders;
            this.orderName = orderName;
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            _orders = new List<Order>
            {
                new Order { Id = 1, ItemName = orderName, CustomerId = 1 },
                new Order { Id = 10, ItemName = orderName, CustomerId = 12 }
            };
            var response = new HttpResponseMessage()
            {
                Content = new ObjectContent(typeof(List<Order>), _orders, jsonMediaType),
                RequestMessage = _request
            };
            return Task.FromResult(response);
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int CustomerId { get; set; }
    }
}

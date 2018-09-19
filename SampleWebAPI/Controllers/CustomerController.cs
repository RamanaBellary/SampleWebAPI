using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SampleWebAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Web.Http;

namespace SampleWebAPI.Controllers
{
    //[MyAuthorization]
    [Log]
    public class CustomerController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeOnlyActiveCustomers">This will passed as part of Query String.</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage RetrieveCustomers(bool includeOnlyActiveCustomers = true)
        {
            string name=null;
            name.Substring(1, 10);
            if (includeOnlyActiveCustomers)
                return Request.CreateResponse(HttpStatusCode.OK, new List<Customer> { new Customer { Id = 1, Name = "Customer1" } });

            return Request.CreateResponse(HttpStatusCode.OK, new List<Customer> { new Customer { Id = 1, Name = "Customer1" }, new Customer { Id = 2, Name = "Customer2" } });
        }

        [HttpGet]
        public HttpResponseMessage Customer(int customerId)
        {
            Thread.Sleep(5000);
            return Request.CreateResponse(HttpStatusCode.OK, new Customer { Id = customerId, Name = "Customer" + customerId });
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

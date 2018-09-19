using Newtonsoft.Json.Serialization;
using SampleWebAPI.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

namespace SampleWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //Note: Below two lines are needed if we have support only JSON format
            //config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Customer",
                routeTemplate: "api/customer/{customerId}",
                defaults: new { controller = "Customer", customerId = RouteParameter.Optional }
                //,constraints:""
            );

            //Implemented the below route using Attribute Routing
            //config.Routes.MapHttpRoute(
            //    name: "Order",
            //    routeTemplate: "api/customer/{customerId}/order/{orderId}",
            //    defaults: new { controller = "Order", orderId = RouteParameter.Optional }
            //    //,constraints:""
            //);

            //Below two lines are required so that the output will be seralized to Camel Case format.
            var jsonFormatter = config.Formatters.JsonFormatter;//.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();


            config.Filters.Add(new LogAttribute());
            //Force Https on entire API
#if !DEBUG
            config.Filters.Add(new RequireHttpsAttribute());
#endif
            //To allow CORS(Cross Origin Resource Sharing)
            var cors = new EnableCorsAttribute("www.example.com", "*", "*");
            config.EnableCors(cors);

            config.Services.Add(typeof(IExceptionLogger), 
                new TraceSourceExceptionLogger(new TraceSource("MyTraceSource", SourceLevels.All)));

            //config.Routes.MapHttpRoute(
            //    name: "ControllerWithAction",
            //    routeTemplate:"api/{controller}/{action}"
            //    );

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}

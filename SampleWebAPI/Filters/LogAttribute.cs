using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SampleWebAPI.Filters
{
    public class LogAttribute : Attribute, IActionFilter
    {
        public bool AllowMultiple
        {
            get
            {
                return true;
            }
        }

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            Trace.WriteLine($"Method {actionContext.ActionDescriptor.ActionName} is executing at {DateTime.Now.ToString("DD-MM-YY HH:mm:ss")}");
            var result = continuation();
            result.Wait();
            Trace.WriteLine($"Method {actionContext.ActionDescriptor.ActionName} is executed at {DateTime.Now.ToString("DD-MM-YY HH:mm:ss")}");
            return result;
        }
    }
}
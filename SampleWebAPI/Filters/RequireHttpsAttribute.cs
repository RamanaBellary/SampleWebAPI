using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SampleWebAPI.Filters
{
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var request = actionContext.Request;

            if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                var res = "Https is required";
                if(request.Method == HttpMethod.Get)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Found);
                    actionContext.Response.Content = new StringContent(res);

                    var uriBuilder = new UriBuilder(request.RequestUri);
                    uriBuilder.Scheme = Uri.UriSchemeHttps;
                    uriBuilder.Port = 44329;
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                    actionContext.Response.Content = new StringContent(res);
                }
            }
        }
    }
}
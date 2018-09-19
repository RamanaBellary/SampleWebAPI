using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SampleWebAPI.Filters
{
    public class MyAuthorizationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                    !string.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    var rawCredentials = authHeader.Parameter;
                    var encoding = Encoding.GetEncoding("ISO-8859-1");
                    var credentials = encoding.GetString(Convert.FromBase64String(rawCredentials));
                    var split = credentials.Split(':');
                    var userName = split[0];
                    var password = split[1];

                    //TODO: Validate the user credentials and set the current context with the user details
                    if (true)
                    {
                        actionContext.RequestContext.Principal = new GenericPrincipal(new GenericIdentity(userName), null);
                        return;
                    }
                }
            }
            HandleUnAuthorized(actionContext);
        }

        private void HandleUnAuthorized(HttpActionContext actionContext)
        {
            actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            //By setting the response Header with 'WWW-Authenticate', the user will know how to get authenticated..
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='domain name' location='http://servicename:port/login'");
        }
    }
}
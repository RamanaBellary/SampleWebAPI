using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace SampleWebAPI
{
    public class WebApiApplication1 : HttpApplication
    {
        #region Events that don't get fired on each request 
        /// <summary>
        /// Gets called once when the Application is started
        /// </summary>
        protected void Application_Start(object sender, EventArgs e)
        {
            //Prior to WebAPI2
            //WebApiConfig.Register(GlobalConfiguration.Configuration);

            //In WebAPI2
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //SwaggerConfig.Register();
        }

        /// <summary>
        /// Gets called once when the application is ended/stoped
        /// </summary>
        protected void Application_End(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Gets called when a new Session is started
        /// </summary>
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Gets called once when a Session is ended
        /// </summary>
        protected void Session_End(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Gets called when an Unhandled exception occurs
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Get called once when the Application is disposed(Basically when garbage collected..)
        /// </summary>
        protected void Application_Disposed(object sender, EventArgs e)
        {

        }
        #endregion

        #region Events that get called on every request
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthorizeRequest(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            
        } 
        #endregion


    }
}

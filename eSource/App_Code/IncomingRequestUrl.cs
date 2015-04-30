using System;
using System.Web;
using System.Web;

namespace EBid.IncomingRequestUrl
{
    public class UrlMungeModule : IHttpModule
    {
        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
        }

        public void Dispose()
        {
            //nop
        }

        #endregion

        private static void BeginRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            string path = app.Context.Request.Url.PathAndQuery;
            int pos = path.IndexOf("%20%3C");
            if (pos > -1)
            {
                path = path.Substring(0, pos);
                app.Context.RewritePath(path);
            }

        }
    }
}
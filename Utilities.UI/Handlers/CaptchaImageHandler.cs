#region Using Statements
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;
using Utilities.UI;
#endregion

namespace System.Web
{
    public class CaptchaImageHandler : IHttpHandler, IRequiresSessionState
    {
        #region IHttpHandler Members

        public void ProcessRequest(HttpContext context)
        {
            CaptchaImage ci;
            HttpApplication app = context.ApplicationInstance;

            if (app.Request.QueryString["guid"] == null)
            {
                app.Response.StatusCode = 404;
                app.CompleteRequest();
            }
            string guid = app.Request.QueryString["guid"];

            string s = String.Empty;
            if (app.Request.QueryString["s"] != null)
            {
                s = app.Request.QueryString["s"];
            }


            if (String.IsNullOrEmpty(s))
            {
                ci = (CaptchaImage) context.Cache[guid];
            }
            else
            {
                ci = (CaptchaImage) context.Session[guid];
            }


            if (ci == null)
            {
                app.Response.StatusCode = 404;
                app.CompleteRequest();
            }
            else
            {
                app.Context.Response.Clear();
                Bitmap b = ci.RenderImage();
                b.Save(app.Context.Response.OutputStream, ImageFormat.Jpeg);
                b.Dispose();
                app.Response.ContentType = "image/jpg";
                app.Response.StatusCode = 200;
                app.CompleteRequest();
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }

        #endregion
    }
}
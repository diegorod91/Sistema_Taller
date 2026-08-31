using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web
{
    public static class WebExtensions
    {
        public static string CreateAbsoluteUrl(this System.Web.UI.Page page, string relativeUrl)
        {
            var request = page.Request;
            return string.Format("{0}://{1}{2}", (request.IsSecureConnection) ? "https" : "http", request.Headers["Host"], System.Web.VirtualPathUtility.ToAbsolute(relativeUrl));
        }
        public static string CreateAbsoluteUrl(this HttpRequest request, string relativeUrl)
        {
            return string.Format("{0}://{1}{2}", (request.IsSecureConnection) ? "https" : "http", request.Headers["Host"], System.Web.VirtualPathUtility.ToAbsolute(relativeUrl));
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utilities
{
    public class Contexto
    {
        static IDictionary standAloneContext;
        public static IDictionary Items
        {
            get
            {
                IDictionary result = null;
                if (HttpContext.Current == null)
                {
                    if (standAloneContext == null)
                        standAloneContext = new Dictionary<string, object>();
                    result = standAloneContext;
                }
                else
                { result = HttpContext.Current.Items; }
                return result;
            }
            private set { }
        }
        public static NameValueCollection ServerVariables 
        { 
            get 
            {
                return HttpContext.Current == null ? null : HttpContext.Current.Request.ServerVariables; 
            } 
            private set { } 
        }
    }
}

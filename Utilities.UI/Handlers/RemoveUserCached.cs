using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace System.Web
{
    public class RemoveUserCached: IHttpHandler
    {
        #region Miembros de IHttpHandler

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(System.Web.HttpContext context)
        {
            try
            {
                if (context.Request.Form["idUsuario"] != null)
                {
                    if (context.Request.Form["idUsuario"].ToString().Equals("Todos"))
                    {
                        IDictionaryEnumerator CacheEnum = context.Cache.GetEnumerator();
                        while (CacheEnum.MoveNext())
                        {
                            context.Cache.Remove(CacheEnum.Key.ToString());
                        }
                    }
                    else
                    {
                        string key = context.Request.Form["idUsuario"].ToString();
                        int intKey = 0;
                        if (int.TryParse(key, out intKey))
                        {
                            if (context.Cache.Get(key) != null)
                            { context.Cache.Remove(key); }
                        }
                    }
                }
                context.Response.Write(true);
            }
            catch
            {
                context.Response.Write(false);
            }
     
        }

        #endregion
    }
}

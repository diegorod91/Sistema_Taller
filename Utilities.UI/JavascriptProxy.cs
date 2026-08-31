using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace System.Web.UI
{
    public static class JavascriptProxy
    {
        private static bool useMSAjax;
        private static MethodInfo RegisterClientScriptBlockMethod;
        private static MethodInfo RegisterClientScriptIncludeMethod;
        private static MethodInfo RegisterClientScriptResourceMethod;
        private static MethodInfo RegisterHiddenFieldMethod;
        private static MethodInfo RegisterStartupScriptMethod;
        private static MethodInfo GetCurrent;
        private static Type scriptManagerType;

        public static bool IsInAsyncPostBack(Page page)
        {
            if (!JavascriptProxy.UseMSAjax)
            {
                return false;
            }
            GetCurrent = scriptManagerType.GetMethod("GetCurrent", new Type[] { typeof(Page) });
            object obj = GetCurrent.Invoke(null, new object[] { page });
            return (bool)scriptManagerType.GetProperty("IsInAsyncPostBack").GetValue(obj, null);
        }

        public static bool UseMSAjax
        {
            get
            {
                if (scriptManagerType == null)
                {
                    VerifyIfExistMsAjax();
                }
                return useMSAjax;
            }
        }
        public static bool VerifyIfExistMsAjax()
        {
            Assembly assembly = null;
            foreach (Assembly assembly2 in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly2.FullName.StartsWith("System.Web.Extensions"))
                {
                    assembly = assembly2;
                    break;
                }
            }
            if (assembly != null)
            {
                scriptManagerType = assembly.GetType("System.Web.UI.ScriptManager");
                if (scriptManagerType != null)
                {
                    useMSAjax = true;
                    return true;
                }
                useMSAjax = false;
            }
            return false;
        }

        #region RegisterClientScriptBlock
        public static void RegisterClientScriptBlock(Control control, Type type, string key, string script, bool addStartupTags)
        {
            if (control != null && type != null)
            {
                if (JavascriptProxy.UseMSAjax)
                {
                    if (RegisterClientScriptBlockMethod == null)
                    {
                        RegisterClientScriptBlockMethod = scriptManagerType.GetMethod("RegisterClientScriptBlock", new Type[] { typeof(Control), typeof(Type), typeof(string), typeof(string), typeof(bool) });
                    }
                    RegisterClientScriptBlockMethod.Invoke(null, new object[] { control, type, key, script, addStartupTags });
                }
                else
                {
                    control.Page.ClientScript.RegisterClientScriptBlock(type, key, script, addStartupTags);
                }
            }
        }
        public static void RegisterClientScriptBlock(Control control, Type type, string key, string script)
        {
            RegisterClientScriptBlock(control, type, key, script, false);
        }
        #endregion RegisterClientScriptBlock

        #region RegisterClientScriptInclude
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "3#")]
        public static void RegisterClientScriptInclude(Control control, Type type, string key, string url)
        {
            if (control != null && type != null)
            {
                if (JavascriptProxy.UseMSAjax)
                {
                    if (RegisterClientScriptIncludeMethod == null)
                    {
                        RegisterClientScriptIncludeMethod = scriptManagerType.GetMethod("RegisterClientScriptInclude", new Type[] { typeof(Control), typeof(Type), typeof(string), typeof(string) });
                    }
                    RegisterClientScriptIncludeMethod.Invoke(null, new object[] { control, type, key, url });
                }
                else
                {
                    control.Page.ClientScript.RegisterClientScriptInclude(key, url);
                }
            }
        }
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#")]
        public static void RegisterClientScriptInclude(Control control, string key, string url)
        {
            if (control != null)
            {
                RegisterClientScriptInclude(control, control.Page.GetType(), key, url);
            }
        }
        #endregion RegisterClientScriptInclude

        #region RegisterClientScriptResource
        public static void RegisterClientScriptResource(Control control, Type type, string resourceName)
        {
            if (control != null && type != null)
            {
                if (JavascriptProxy.UseMSAjax)
                {
                    if (RegisterClientScriptResourceMethod == null)
                    {
                        RegisterClientScriptResourceMethod = scriptManagerType.GetMethod("RegisterClientScriptResource", new Type[] { typeof(Control), typeof(Type), typeof(string) });
                    }
                    RegisterClientScriptResourceMethod.Invoke(null, new object[] { control, type, resourceName });
                }
                else
                {
                    control.Page.ClientScript.RegisterClientScriptResource(type, resourceName);
                }
            }
        }
        #endregion RegisterClientScriptResource

        #region RegisterHiddenField
        public static void RegisterHiddenField(Control control, string hiddenFieldName, string hiddenFieldInitialValue)
        {
            if (control != null)
            {
                if (JavascriptProxy.UseMSAjax)
                {
                    if (RegisterHiddenFieldMethod == null)
                    {
                        RegisterHiddenFieldMethod = scriptManagerType.GetMethod("RegisterHiddenField", new Type[] { typeof(Control), typeof(string), typeof(string) });
                    }
                    RegisterHiddenFieldMethod.Invoke(null, new object[] { control, hiddenFieldName, hiddenFieldInitialValue });
                }
                else
                {
                    control.Page.ClientScript.RegisterHiddenField(hiddenFieldName, hiddenFieldInitialValue);
                }
            }
        }
        #endregion RegisterHiddenField

        #region RegisterStartupScript
        public static void RegisterStartupScript(Control control, Type type, string key, string script, bool addStartupTags)
        {
            if (control != null)
            {
                if (JavascriptProxy.UseMSAjax)
                {
                    if (RegisterStartupScriptMethod == null)
                    {
                        RegisterStartupScriptMethod = scriptManagerType.GetMethod("RegisterStartupScript", new Type[] { typeof(Control), typeof(Type), typeof(string), typeof(string), typeof(bool) });
                    }
                    RegisterStartupScriptMethod.Invoke(null, new object[] { control, type, key, script, addStartupTags });
                }
                else
                {
                    control.Page.ClientScript.RegisterStartupScript(type, key, script, addStartupTags);
                }
            }
        }
        public static void RegisterStartupScript(Control control, Type type, string key, string script)
        {
            JavascriptProxy.RegisterStartupScript(control, type, key, script, false);
        }
        #endregion RegisterStartupScript

        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        public static string GetWebResourceUrl(Control control, Type type, string resourceName)
        {
            if (control != null && type != null)
            {
                return control.Page.ClientScript.GetWebResourceUrl(type, resourceName);
            }
            return "";
        }
    }
}

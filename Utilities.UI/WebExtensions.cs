using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ClosedXML.Excel;

namespace System.Web
{
    public static class WebExtensions
    {
        public static void RedirectTo(this UI.Page current, string url)
        {
            string redirectURL = current.ResolveClientUrl(url);
            string script = "window.location = '" + redirectURL + "';";
            UI.JavascriptProxy.RegisterStartupScript(current, current.GetType(), "RedirectTo", script, true);
        }
        public static string CreateAbsoluteUrl(this UI.Page page, string relativeUrl)
        {
            var request = page.Request;
            return string.Format("{0}://{1}{2}", (request.IsSecureConnection) ? "https" : "http", request.Headers["Host"], System.Web.VirtualPathUtility.ToAbsolute(relativeUrl));
        }
        public static string CreateAbsoluteUrl(this HttpRequest request, string relativeUrl)
        {
            return string.Format("{0}://{1}{2}", (request.IsSecureConnection) ? "https" : "http", request.Headers["Host"], System.Web.VirtualPathUtility.ToAbsolute(relativeUrl));
        }
        public static string ConvertBoolToString(object value)
        {
            string result = "NO";
            if ((bool)value)
            { result = "SI"; }
            return result;
        }
        public static string ConvertBoolToStyleDisplay(object value)
        {
            string result = "none";
            if ((bool)value)
            { result = ""; }
            return result;
        }
        public static string ConvertIntToStyleDisplay(object value)
        {
            string result = "none";
            string nro = value.ToString();
            if (int.Parse(nro) == 0)
            { result = ""; }
            return result;
        }
        public static string ConvertIntToString(object value)
        {
            string result = "NO";
            string nro = value.ToString();
            if (int.Parse(nro) == 1)
            { result = "SI"; }
            return result;
        }
        public static string FormatDate(object value)
        {
            string result = "";
            try
            {
                result = DateTime.Parse(value.ToString()).ToString("dd/MM/yyyy");
            }
            finally { result = ""; }
            return result;
        }
        public static string FormatDateTime(object value)
        {
            string result = "";
            try
            {
                result = DateTime.Parse(value.ToString()).ToString("dd/MM/yyyy HH:mm");
            }
            finally { result = ""; }
            return result;
        }
        public static string FormatTime(object value)
        {
            string result = "";
            try
            {
                result = DateTime.Parse(value.ToString()).ToString("HH:mm");
            }
            finally { result = ""; }
            return result;
        }
        public static string JsonSinCiclo(object objeto)
        {
            return WebExtensions.JsonSinCiclo(objeto, true);
        }
        public static string JsonSinCiclo(object objeto, bool isHTML)
        {
            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.Converters.Add(new Newtonsoft.Json.Converters.IsoDateTimeConverter() { DateTimeFormat = "dd/MM/yyyy HH:mm" });
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string json = JsonConvert.SerializeObject(objeto, Formatting.None, jsSettings);
            if (isHTML)
                json = json.Replace("\"", "&quot;").Replace("'", "&#39;");
            return json;
        }
        public static void GenerateJsControlID(this Page page)
        {
            GenerateJsControlID(page, true);
        }
        public static void GenerateJsControlID(this Page page, bool generateJQID)
        {
            System.Collections.ArrayList a = new System.Collections.ArrayList();
            a.AddRange(page.Controls);
            System.Web.UI.Control element = null;
            string jsIds = "";
            while (a.Count > 0)
            {
                element = (System.Web.UI.Control)a[0];
                a.RemoveAt(0);
                if (element is WebControl || element is HtmlControl || element is HiddenField)
                {
                    if (!string.IsNullOrEmpty(element.ID))
                        if (generateJQID)
                            jsIds += "var " + element.ID + " = '#" + element.ClientID + "';\n";
                        else
                            jsIds += "var " + element.ID + " = '" + element.ClientID + "';\n";
                }

                a.AddRange(element.Controls);
            }
            JavascriptProxy.RegisterStartupScript(page, page.GetType(), "JsIDs", jsIds, true);
        }

        public static void Notificar(this Page page, int tipo, string titulo, string mensaje)
        {
            if (page != null)
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "notificar", "notificar(" + tipo + ", '" + titulo + "', '" + mensaje + "');", true);
            }
        }

        public static void ExportDataToExcel(this Page page, object data, string xlsName)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            HtmlForm form = new HtmlForm();
            GridView gv_Excel = new GridView();
            gv_Excel.DataSource = data;
            gv_Excel.DataBind();
            string style = @"<style>.text {mso-number-format:\@;} </style>";
            Page pageToRender = new Page();
            pageToRender.EnableEventValidation = false;
            pageToRender.DesignerInitialize();
            pageToRender.Controls.Add(form);
            form.Controls.Add(gv_Excel);
            pageToRender.RenderControl(htw);
            page.Response.Clear();
            page.Response.Buffer = true;
            page.Response.ContentType = "application/vnd.ms-excel";
            page.Response.AddHeader("Content-Disposition", "attachment;filename=" + xlsName);
            page.Response.Charset = "UTF-8";
            page.Response.ContentEncoding = Encoding.Default;
            page.Response.Write(style);
            page.Response.Write(sb.ToString());
            page.Response.End();
        }
        public static void ExportDataToExcel(this Page page, Data.DataSet data, string xlsName)
        {
            MemoryStream ms = new MemoryStream();
            XLWorkbook wb = new XLWorkbook();
            Data.DataTable dt = data.Tables[0];
            wb.Worksheets.Add(dt, xlsName);
            wb.SaveAs(ms);
            page.Response.Clear();
            HttpCookie hc = new HttpCookie("fileDownloadToken", Guid.NewGuid().ToString());
            hc.Expires = DateTime.Now.AddSeconds(3);
            page.Response.AppendCookie(hc);
            page.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            page.Response.AddHeader("content-disposition", "AlwaysAttachment; filename = " + xlsName + ".xlsx");
            page.Response.BinaryWrite(ms.ToArray());
            page.Response.End();
        }
        public static void ExportDataToExcel(this Page page, object data, string title, string xlsName)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            HtmlForm form = new HtmlForm();
            GridView gv_Excel = new GridView();
            gv_Excel.DataSource = data;
            gv_Excel.Caption = title;
            gv_Excel.CaptionAlign = TableCaptionAlign.Top;
            gv_Excel.DataBind();
            string style = @"<style>.text {mso-number-format:\@;} </style>";
            Page pageToRender = new Page();
            pageToRender.EnableEventValidation = false;
            pageToRender.DesignerInitialize();
            pageToRender.Controls.Add(form);
            form.Controls.Add(gv_Excel);
            pageToRender.RenderControl(htw);
            page.Response.Clear();
            page.Response.Buffer = true;
            page.Response.ContentType = "application/vnd.ms-excel";
            page.Response.AddHeader("Content-Disposition", "attachment;filename=" + xlsName);
            page.Response.Charset = "UTF-8";
            page.Response.ContentEncoding = Encoding.Default;
            page.Response.Write(style);
            page.Response.Write(sb.ToString());
            page.Response.End();
        }

        public static void MostrarMensajeValidacion(this Page page, string mensaje)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "mostrarMsjValidacion", "mostrarMsjValidacion('" + mensaje + "');", true);
        }
    }
}

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace Utilities.UI
{

    public class Report
    {
        #region Campos
        private Dictionary<string, string> reportParameters = new Dictionary<string, string>();
        private List<ReportParameter> parametersReporte = new List<ReportParameter>();
        private LocalReport reporteLocal = new LocalReport();
        #endregion Campos

        #region Propiedades estáticas
        private LocalReport ReporteLocal
        {
            get { return reporteLocal; }
            set { reporteLocal = value; }
        }
        #endregion Propiedades estáticas

        #region Métodos no estáticos
        public void AddDataSource(string name, object dataSourceValue)
        {
            ReporteLocal.DataSources.Add(new ReportDataSource(name, dataSourceValue));
        }
        public void AddParameter(string name, object value)
        {
            // reportParameters.Add(name, value.ToString());
            parametersReporte.Add(new ReportParameter(name, value.ToString()));
        }
        public void ExportReport(string nameReport, Page pagina)
        {
            this.ExportReport(null, nameReport, Formato.None, ExportedMod.None, pagina);
        }
        public void ExportReport(string nameReport, Formato format, Page pagina)
        {
            this.ExportReport(null, nameReport, format, ExportedMod.None, pagina);
        }
        public void ExportReport(string nameReport, ExportedMod exportedMod, Page pagina)
        {
            this.ExportReport(null, nameReport, Formato.None, exportedMod, pagina);
        }
        public void ExportReport(string nameReport, Formato format, ExportedMod exportedMod, Page pagina)
        {
            this.ExportReport(null, nameReport, format, exportedMod, pagina);
        }
        public void ExportReport(string fileName, string nameReport, Page pagina)
        {
            this.ExportReport(fileName, nameReport, Formato.None, ExportedMod.None, pagina);
        }
        public void ExportReport(string fileName, string nameReport, Formato format, Page pagina)
        {
            this.ExportReport(fileName, nameReport, format, ExportedMod.None, pagina);
        }
        public void ExportReport(string fileName, string nameReport, ExportedMod exportedMod, Page pagina)
        {
            this.ExportReport(fileName, nameReport, Formato.None, exportedMod, pagina);
        }
        public void ExportReport(string fileName, string nameReport, Formato format, ExportedMod exportedMod, Page pagina)
        {
            ReporteLocal.ReportPath = pagina.Server.MapPath(nameReport + ".rdlc");
            ReporteLocal.EnableExternalImages = true;
            ReporteLocal.EnableHyperlinks = true;
            if (parametersReporte.Count > 0)
            { reporteLocal.SetParameters(parametersReporte); }
            string formatos;
            string exportara;
            string extension = "";
            switch (format)
            {
                case Formato.PDF:
                    formatos = "PDF";
                    extension = "pdf";
                    break;
                case Formato.EXCEL:
                    formatos = "Excel";
                    extension = "xls";
                    break;
                case Formato.IMG:
                    formatos = "Image";
                    extension = "png";
                    break;
                default:
                    formatos = "PDF";
                    extension = "pdf";
                    break;
            }
            switch (exportedMod)
            {
                case ExportedMod.AlwaysInline:
                    exportara = "AlwaysInline";
                    break;
                case ExportedMod.OnlyHtmlInline:
                    exportara = "OnlyHtmlInline";
                    break;
                case ExportedMod.AlwaysAttachment:
                    exportara = "AlwaysAttachment";
                    break;
                default:
                    exportara = "AlwaysAttachment";
                    break;
            }
            string reportType = formatos;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
           "<DeviceInfo>" +
           "  <OutputFormat>" + extension + "</OutputFormat>" +
           "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            //Render the report
            renderedBytes = ReporteLocal.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            pagina.Response.Clear();
            pagina.Response.AppendCookie(new HttpCookie("fileDownloadToken", Guid.NewGuid().ToString()));
            pagina.Response.ContentType = mimeType;
            if (format.Equals(Formato.IMG))
            {
                pagina.Response.ContentType = "image/png";
            }
            pagina.Response.AddHeader("content-disposition", exportara + "; filename=" + (String.IsNullOrEmpty(fileName) ? nameReport : fileName) + "." + fileNameExtension);
            pagina.Response.BinaryWrite(format.Equals(Formato.IMG) ? ConvertTiffToPng(renderedBytes) : renderedBytes);
            pagina.Response.End();
        }
        
        public MemoryStream ExportReport(string nameReport)
        {
            return this.ExportReport(nameReport, Formato.None);
        }
        public MemoryStream ExportReport(string nameReport, Formato format)
        {
            ReporteLocal.ReportPath = HttpContext.Current.Server.MapPath(nameReport + ".rdlc");
            string formatos;
            string extension = "";
            switch (format)
            {
                case Formato.PDF:
                    formatos = "PDF";
                    extension = "pdf";
                    break;
                case Formato.EXCEL:
                    formatos = "Excel";
                    extension = "xls";
                    break;
                case Formato.IMG:
                    formatos = "Image";
                    extension = "tiff";
                    break;
                default:
                    formatos = "PDF";
                    extension = "pdf";
                    break;
            }
            string reportType = formatos;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
           "<DeviceInfo>" +
           "  <OutputFormat>" + extension + "</OutputFormat>" +
           "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            //Render the report
            renderedBytes = ReporteLocal.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return new MemoryStream(format.Equals(Formato.IMG) ? ConvertTiffToPng(renderedBytes) : renderedBytes);
        }
        #endregion

        #region Métodos estáticos
        public static void ExportReport(string nameReport, string esquema, object datos, Page pagina)
        {
            Report.ExportReport(null, nameReport, esquema, datos, Formato.None, ExportedMod.None, pagina);
        }
        public static void ExportReport(string nameReport, string esquema, object datos, Formato format, Page pagina)
        {
            Report.ExportReport(null, nameReport, esquema, datos, format, ExportedMod.None, pagina);
        }
        public static void ExportReport(string nameReport, string esquema, object datos, ExportedMod exportedMod, Page pagina)
        {
            Report.ExportReport(null, nameReport, esquema, datos, Formato.None, exportedMod, pagina);
        }
        public static void ExportReport(string nameReport, string esquema, object datos, Formato format, ExportedMod exportedMod, Page pagina)
        {
            Report.ExportReport(null, nameReport, esquema, datos, format, exportedMod, pagina);
        }
        public static void ExportReport(string fileName, string nameReport, string esquema, object datos, Page pagina)
        {
            Report.ExportReport(fileName, nameReport, esquema, datos, Formato.None, ExportedMod.None, pagina);
        }
        public static void ExportReport(string fileName, string nameReport, string esquema, object datos, Formato format, Page pagina)
        {
            Report.ExportReport(fileName, nameReport, esquema, datos, format, ExportedMod.None, pagina);
        }
        public static void ExportReport(string fileName, string nameReport, string esquema, object datos, ExportedMod exportedMod, Page pagina)
        {
            Report.ExportReport(fileName, nameReport, esquema, datos, Formato.None, exportedMod, pagina);
        }
        public static void ExportReport(string fileName, string nameReport, string esquema, object datos, Formato format, ExportedMod exportedMod, Page pagina)
        {
            LocalReport localReport = new LocalReport();
            localReport.EnableExternalImages = true;
            localReport.EnableHyperlinks = true;
            localReport.ReportPath = pagina.Server.MapPath(nameReport + ".rdlc");
            ReportDataSource reportDataSource = new ReportDataSource(esquema, datos);
            localReport.DataSources.Add(reportDataSource);
            string formatos;
            string exportara;
            string extension = "";
            switch (format)
            {
                case Formato.PDF:
                    formatos = "PDF";
                    extension = "pdf";
                    break;
                case Formato.EXCEL:
                    formatos = "Excel";
                    extension = "xls";
                    break;
                case Formato.IMG:
                    formatos = "Image";
                    extension = "png";
                    break;
                default:
                    formatos = "PDF";
                    extension = "pdf";
                    break;
            }
            switch (exportedMod)
            {
                case ExportedMod.AlwaysInline:
                    exportara = "AlwaysInline";
                    break;
                case ExportedMod.OnlyHtmlInline:
                    exportara = "OnlyHtmlInline";
                    break;
                case ExportedMod.AlwaysAttachment:
                    exportara = "AlwaysAttachment";
                    break;
                default:
                    exportara = "AlwaysAttachment";
                    break;
            }
            string reportType = formatos;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
           "<DeviceInfo>" +
           "  <OutputFormat>" + extension + "</OutputFormat>" +
           "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            //Render the report
            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            pagina.Response.Clear();
            pagina.Response.AppendCookie(new HttpCookie("fileDownloadToken", Guid.NewGuid().ToString()));
            pagina.Response.ContentType = mimeType;
            if (format.Equals(Formato.IMG))
            {
                pagina.Response.ContentType = "image/png";
            }
            pagina.Response.AddHeader("content-disposition", exportara + "; filename=" + (String.IsNullOrEmpty(fileName) ? nameReport : fileName) + "." + fileNameExtension);
            pagina.Response.BinaryWrite(format.Equals(Formato.IMG) ? ConvertTiffToPng(renderedBytes) : renderedBytes);
            pagina.Response.End();
        }
        public static MemoryStream ExportReport(string nameReport, string esquema, object datos)
        {
            return Report.ExportReport(nameReport, esquema, datos, Formato.None);
        }
        public static MemoryStream ExportReport(string nameReport, string esquema, object datos, Formato format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = HttpContext.Current.Server.MapPath(nameReport + ".rdlc");
            ReportDataSource reportDataSource = new ReportDataSource(esquema, datos);
            localReport.DataSources.Add(reportDataSource);
            string formatos;
            string extension = "";
            switch (format)
            {
                case Formato.PDF:
                    formatos = "PDF";
                    extension = "pdf";
                    break;
                case Formato.EXCEL:
                    formatos = "Excel";
                    extension = "xls";
                    break;
                case Formato.IMG:
                    formatos = "Image";
                    extension = "tiff";
                    break;
                default:
                    formatos = "PDF";
                    extension = "pdf";
                    break;
            }
            string reportType = formatos;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
           "<DeviceInfo>" +
           "  <OutputFormat>" + extension + "</OutputFormat>" +
           "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            //Render the report
            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return new MemoryStream(format.Equals(Formato.IMG) ? ConvertTiffToPng(renderedBytes) : renderedBytes);
        }
        public static byte[] ConvertTiffToPng(byte[] tiff)
        {
            MemoryStream png = new MemoryStream();
            using (System.Drawing.Image imgTiff = Bitmap.FromStream(new MemoryStream(tiff)))
            {
                imgTiff.Save(png, System.Drawing.Imaging.ImageFormat.Png);
                return png.ToArray();
            }
        } 
        #endregion Métodos estáticos
    }

    public enum Formato
    {
        None = 0, PDF = 1, EXCEL = 2, IMG = 3
    }

    public enum ExportedMod
    {
        None = 0, AlwaysInline = 1, OnlyHtmlInline = 2, AlwaysAttachment = 3
    }
}
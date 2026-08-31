using System;
using System.IO;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Web;
using System.Diagnostics;

namespace WCL.ErrorModule
{
    /// <summary>
    /// Summary description for SessionHttpModule.
    /// </summary>
    public class ErrorModule : System.Web.IHttpModule, System.Web.SessionState.IRequiresSessionState
    {
        public ErrorModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static String ModuleName
        {
            get { return "ErrorModule"; }
        }

        public void Dispose()
        {

        }

        public void Init(System.Web.HttpApplication context)
        {
            context.Error += new EventHandler(context_Error);
        }

        void context_Error(object sender, EventArgs e)
        {
            System.Web.HttpApplication httpApp = sender as System.Web.HttpApplication;

            if (httpApp.Request.Url.Host.ToLower() != "localhost")
            {
                List<CustomExcepcion> listExc = new List<CustomExcepcion>();
                Exception exc = httpApp.Context.Server.GetLastError();
                while (exc != null)
                {
                    if (!exc.GetType().Equals(typeof(HttpUnhandledException)))
                    {
                        FunctException.ProcessException(ref exc);
                        if (exc is FunctException) Redireccionar(httpApp, exc.Message);
                        listExc.Add(new CustomExcepcion(exc));
                    }
                    exc = exc.InnerException;
                }
                httpApp.Context.ClearError();
                LogError log = new LogError();
                try
                {
                    log = WriteFileError(listExc, httpApp);
                    SendMail(log.NombreError, log.MensajeMail);
                }
                catch (Exception sql)
                {
                    SendMail("Error al logear el error en la base de datos", sql.Message);
                }
                finally
                {
                    Redireccionar(httpApp, log.MsjUser);
                }
            }
        }

        public void Redireccionar(System.Web.HttpApplication httpApp, string mensaje)
        {
            //string SchemeAndServer = httpApp.Context.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped);
            //string path = httpApp.Context.Request.Url.AbsolutePath.Split(new string[] { "/" }, StringSplitOptions.None)[1];
            //string uri = SchemeAndServer + "/" + path + "/";
            //httpApp.Context.Response.Redirect(uri + "error.aspx?error=" + mensaje);
            httpApp.Context.Response.Redirect(httpApp.Context.Request.CreateAbsoluteUrl("~/error.aspx?error=" + mensaje));
        }
        private string GetStringConnection()
        {
            string conexionDB = "";
            switch (System.Configuration.ConfigurationManager.AppSettings["SystemState"].ToString())
            {
                case "D": conexionDB = "Data Source=sbd.informatica.mseg.gba.gov.ar;Initial Catalog=LogErrores;Persist Security Info=True;User ID=sa;Password=sql"; break;
                case "T": conexionDB = "Data Source=test.informatica.mseg.gba.gov.ar;Initial Catalog=LogErrores;Persist Security Info=True;User ID=sa;Password=sql"; break;
                case "P": conexionDB = "Data Source=homero.mseg.gba.gov.ar;Initial Catalog=LogErrores;Persist Security Info=True;User ID=LogErrores;Password=LogErrores"; break;
            }
            return conexionDB;
        }
        public LogError WriteFileError(List<CustomExcepcion> listExc, System.Web.HttpApplication httpApp)
        {
            string IPCliente = httpApp.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string idSistema = "";
            System.Net.IPHostEntry hostEntry = new System.Net.IPHostEntry();
            DateTime fecha = DateTime.Now;
            if (!String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["IdSistema"]))
            {
                idSistema = System.Configuration.ConfigurationManager.AppSettings["IdSistema"].ToString();
            }
            if (IPCliente == null)
            {
                IPCliente = httpApp.Request.ServerVariables["REMOTE_ADDR"];
            }
            try
            {
                hostEntry = System.Net.Dns.GetHostEntry(IPCliente);
            }
            catch
            {
                hostEntry.HostName = "No se pudo resolver el nombre del host dado que no se encuentra declarado en ningun DNS";
            }
            string codigoError = fecha.ToString("yyyyMMddHHmmss");
            string fullPathFile = httpApp.Context.Request.PhysicalApplicationPath + "LogError\\Log_Aplicacion" + fecha.ToString("yyyyMMdd") + ".txt";
            string txttipoError = "Error en la aplicación";
            string applicationPath = httpApp.Context.Request.PhysicalApplicationPath;
            System.IO.StreamWriter outPutFile;
            string msj = Serializer(listExc);
            msj = string.Format("Código :{0}\r\nIP Cliente:{1}\r\n Dns Cliente:{2}\r\n URL: {3}\r\n Descripción:\r\n {4}\r\n", codigoError, IPCliente, hostEntry.HostName, httpApp.Context.Request.Url.ToString(), msj);
            string msjUser = "El error fue informado al administrador del sistema. Por favor vuelva a intentar la operación.$$";
            msjUser += "Si el problema presiste tome nota del siguiente codigo de error $$y comuniquese con la Dirección de Infomatica.$$";
            msjUser += "Codigo de error: " + codigoError + ".";
            LogError log = new LogError();
            try
            {
                if (!Directory.Exists(httpApp.Context.Request.PhysicalApplicationPath + "\\LogError"))
                {
                    Directory.CreateDirectory(httpApp.Context.Request.PhysicalApplicationPath + "\\LogError");
                }
                outPutFile = File.AppendText(fullPathFile);
                outPutFile.WriteLine(msj);
                outPutFile.Close();
            }
            finally
            {
                log = new LogError(int.Parse(idSistema), IPCliente, hostEntry.HostName, fecha, txttipoError, msj, msjUser, listExc);
            }
            return log;

        }
        public LogError WriteFileError(List<CustomExcepcion> listExc, System.Web.HttpRequest Request)
        {
            string IPCliente = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string idSistema = "";
            System.Net.IPHostEntry hostEntry = new System.Net.IPHostEntry();
            DateTime fecha = DateTime.Now;
            if (!String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["IdSistema"]))
            {
                idSistema = System.Configuration.ConfigurationManager.AppSettings["IdSistema"].ToString();
            }
            if (IPCliente == null)
            {
                IPCliente = Request.ServerVariables["REMOTE_ADDR"];
            }
            try
            {
                hostEntry = System.Net.Dns.GetHostEntry(IPCliente);
            }
            catch
            {
                hostEntry.HostName = "No se pudo resolver el nombre del host dado que no se encuentra declarado en ningun DNS";
            }
            string codigoError = fecha.ToString("yyyyMMddHHmmss");
            string fullPathFile = Request.PhysicalApplicationPath + "LogError\\Log_Aplicacion" + fecha.ToString("yyyyMMdd") + ".txt";
            string txttipoError = "Error en la aplicación";
            string applicationPath = Request.PhysicalApplicationPath;
            System.IO.StreamWriter outPutFile;
            string msj = Serializer(listExc);
            msj = string.Format("Código :{0}\r\nIP Cliente:{1}\r\n Dns Cliente:{2}\r\n URL: {3}\r\n Descripción:\r\n {4}\r\n", codigoError, IPCliente, hostEntry.HostName, Request.Url.ToString(), msj);
            string msjUser = "El error fue informado al administrador del sistema. Por favor vuelva a intentar la operación.$$";
            msjUser += "Si el problema presiste tome nota del siguiente codigo de error $$y comuniquese con la Dirección de Infomatica.$$";
            msjUser += "Codigo de error: " + codigoError + ".";
            LogError log = new LogError();
            try
            {
                if (!Directory.Exists(Request.PhysicalApplicationPath + "\\LogError"))
                {
                    Directory.CreateDirectory(Request.PhysicalApplicationPath + "\\LogError");
                }
                outPutFile = File.AppendText(fullPathFile);
                outPutFile.WriteLine(msj);
                outPutFile.Close();
            }
            finally
            {
                log = new LogError(int.Parse(idSistema), IPCliente, hostEntry.HostName, fecha, txttipoError, msj, msjUser, listExc);
            }
            return log;

        }
        public void SendMail(string txttipoError, string msg)
        {
            if (!String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["SendMail"])
                        && bool.Parse(System.Configuration.ConfigurationManager.AppSettings["SendMail"].ToLower())
                        && !String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["MailFrom"])
                        && !String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["MailTo"])
                        )
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(System.Configuration.ConfigurationManager.AppSettings["MailFrom"].ToString(), System.Configuration.ConfigurationManager.AppSettings["MailTo"].ToString(), txttipoError, msg);
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Send(mail);
            }
        }
        private string Serializer(object obj)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = new UTF8Encoding(false);
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.OmitXmlDeclaration = true;
            XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
            XmlSerializer x = new XmlSerializer(obj.GetType());
            x.Serialize(xmlWriter, obj);
            string s = Encoding.UTF8.GetString(memoryStream.ToArray());
            s = s.Replace(">true<", ">1<").Replace(">false<", ">0<");
            return s;
        }
        public void ProcessException(ref Exception ex)
        {
            if (ex is SqlException) CheckSQLException(ref ex);
        }
        private void CheckSQLException(ref Exception ex)
        {
            CheckSQLError(ref ex, ((SqlException)ex).Number);
        }
        private void CheckSQLError(ref Exception ex, int nativeErr)
        {
            switch (nativeErr)
            {
                case (int)SQLErrors.UQ:
                case (int)SQLErrors.UQQ: ex = new FunctException(Messages.SQL_UQViolation.ToDescriptionString()); break;
                case (int)SQLErrors.FK: ex = new FunctException(Messages.SQL_FKViolation.ToDescriptionString()); break;
                default: break;
            }
        }

    }
    public class LogError
    {
        internal int idLogError;
        private int idSistema;
        private string ip;
        private string dns;
        private DateTime fecha;
        private string mensajeMail;
        private string nombreError;
        private string msjUser;
        private List<CustomExcepcion> listExc;

        public LogError()
        {
            Ip = String.Empty;
            Dns = String.Empty;
            Fecha = DateTime.Now;
            MensajeMail = String.Empty;
            NombreError = String.Empty;
            MsjUser = String.Empty;
            ListExc = new List<CustomExcepcion>();
        }
        public LogError(int idSistema, string ip, string dns, DateTime fecha, string nomError, string msjMail, string msjUser, List<CustomExcepcion> listExc)
        {
            this.IdSistema = idSistema;
            this.Ip = ip;
            this.Dns = dns;
            this.Fecha = fecha;
            this.NombreError = nomError;
            this.MensajeMail = msjMail;
            this.MsjUser = msjUser;
            this.ListExc = listExc;
        }

        public int IdLogError
        {
            get { return idLogError; }
            set { idLogError = value; }
        }

        public int IdSistema
        {
            get { return idSistema; }
            set { idSistema = value; }
        }

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public string Dns
        {
            get { return dns; }
            set { dns = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = new DateTime(value.Ticks / 10000 * 10000, DateTimeKind.Unspecified); }
        }

        [XmlIgnore]
        public string MensajeMail
        {
            get { return mensajeMail; }
            set { mensajeMail = value; }
        }

        public string NombreError
        {
            get { return nombreError; }
            set { nombreError = value; }
        }

        [XmlIgnore]
        public string MsjUser
        {
            get { return msjUser; }
            set { msjUser = value; }
        }

        public List<CustomExcepcion> ListExc
        {
            get { return listExc; }
            set { listExc = value; }
        }

    }
    public class CustomExcepcion
    {
        private string message;
        private string source;
        private string stackTrace;
        private string stackFrame;
        private string line;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public string Source
        {
            get { return source; }
            set { source = value; }
        }
        public string StackTrace
        {
            get { return stackTrace; }
            set { stackTrace = value; }
        }
        public string StackFrame
        {
            get { return stackFrame; }
            set { stackFrame = value; }
        }
        public string Line
        {
            get { return line; }
            set { line = value; }
        }

        public CustomExcepcion() { }
        public CustomExcepcion(Exception ex)
        {
            this.Message = ex.Message;
            this.Source = ex.Source;
            this.StackTrace = ex.StackTrace;

            StackTrace st = new StackTrace(ex, true);
            var frame = st.GetFrame(0); // where the error originated
            this.Line = frame.GetFileLineNumber().ToString(); // Handle line numbers etc. here

            foreach (var fr in st.GetFrames())
            {
                this.stackFrame += fr.ToString();
            }
        }
    }
}

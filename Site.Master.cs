using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace SolucionBase
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LTR_NavRightButtonName.Text = "Rodriguez Diego";
            LTR_NavRightCargo.Text = "Desarrollador";
            //LTR_NavRightDestino.Text = user.NombreDependencia;
            //LTR_NavRightNivel.Text = user.DescNivelAcceso;
            LTR_NavRightName.Text = "Rodriguez Diego";
            LTR_NavRightNivel.Text = "Desarrollador";

            LTR_SideName.Text = "Rodriguez Diego";
            LTR_SideCargo.Text = "Rodriguez Diego";
            //LTR_SideDestino.Text = user.NombreDependencia.ToUpper();
            LTR_SideNivel.Text = "Rodriguez Diego";

            string title = System.Configuration.ConfigurationManager.AppSettings["ApplicationName"].ToString();
            this.Page.Title = title;
            LTL_NombreSistema.Text = title;
            LTR_TitleSystem.Text = title;
            LTL_anio.Text = DateTime.Today.Year.ToString();

            this.SetearSidebar(10);


        }

        protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            //ErrorModule error = new ErrorModule();
            //List<CustomExcepcion> listExc = new List<CustomExcepcion>();
            //Exception exc = e.Exception;
            //while (exc != null)
            //{
            //    if (!exc.GetType().Equals(typeof(HttpUnhandledException)))
            //    {
            //        error.ProcessException(ref exc);
            //        if (exc is FunctException) { ScriptManager1.AsyncPostBackErrorMessage = exc.Message; return; }
            //        listExc.Add(new CustomExcepcion(exc));
            //    }
            //    exc = exc.InnerException;
            //}
            //LogError log = new LogError();
            //try
            //{
            //    log = error.WriteFileError(listExc, this.Request);
            //    error.SendMail(log.NombreError, log.MensajeMail);
            //}
            //catch (Exception sql)
            //{
            //    error.SendMail("Error al logear el error en la base de datos", sql.Message);
            //}
            //finally
            //{
            //    ScriptManager1.AsyncPostBackErrorMessage = log.MsjUser;
            //}
        }


        protected void CPH1_Init(object sender, EventArgs e)
        {
                   

            
            //if (Session["Usuario"] != null)
            //{
            //    //LoginXML user = (LoginXML)Session["Usuario"];
            //
            //   // this.SetearSidebar(user.NivelAcceso);
            //
            //    //this.ChequearPermisos(user, sender);
            //
            //    //seteo el titulo y el pie dela pagina
            //           //}
            //else
            //{
            //SiteMapDataSource1.SiteMapProvider = "naviVacio";
            //if (!this.Page.Request.Url.ToString().ToLower().Contains("expirosesion.aspx"))
            //{
            //    Response.Redirect("~/Notificacion_expirosesion.aspx");
            //}
            //}
        }
        private string getPaginaActual()
        {
            //le saco el directorio virtual
            string spaginaActual = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;
            //int start = spaginaActual.IndexOf("/cpanel");
            //spaginaActual = spaginaActual.Substring(start, spaginaActual.Length - start);
            return spaginaActual.Replace("~", "");
        }

        private void SetearSidebar(int nivel)
        {
            string xmlPermisos = "";
            switch (nivel)
            {
                case 10: //Administrador
                    xmlPermisos = Server.MapPath("~/App_Data/Permisos_" + nivel.ToString() + ".xml");
                    break;
                default:
                    xmlPermisos = Server.MapPath("~/App_Data/Permisos_" + nivel.ToString() + ".xml");
                    break;
            }

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlPermisos);

            XmlNodeList navigation = xDoc.GetElementsByTagName("navigation");

            string stringMenu = "";
            ProcesarNodoPadre(((XmlElement)navigation[0]), ref stringMenu);
            LTR_Sidebar.Text = stringMenu;

        }
        private void ProcesarNodoPadre(XmlElement nodoPadre, ref string stringMenu)
        {
            foreach (XmlElement nodo in nodoPadre.ChildNodes)
            {
                if (!nodo.HasChildNodes)
                {
                    //armo el menu
                    string style = nodo.GetAttribute("class") != null ? nodo.GetAttribute("class") : "";
                    string activo = SoyActivo(nodo) ? "active" : "";
                    string clase = " class=\"nav-item \"";
                    if (style != "" || activo != "")
                    { clase = " class='nav-item " + style + " '"; }

                    stringMenu += "<li" + clase + ">";
                    stringMenu += "<a class=\"nav-link  " + activo + " \" href=\"" + this.ResolveUrl(nodo.GetAttribute("href")) + "\">";
                    stringMenu += "<i class=' nav-icon " + nodo.GetAttribute("icon") + "'></i>";
                    stringMenu += "<p>" + nodo.GetAttribute("title") + "</p>";
                    stringMenu += "</a>";
                    stringMenu += "</li>";
                }
                else
                {
                    string activo = HijoActivo(nodo) ? "active" : "";
                    string clase = HijoActivo(nodo) ? " class=' nav-item  menu-is-opening menu-open'" : " class=' nav-item '";

                    //armo ul submenu
                    stringMenu += "<li" + clase + ">";
                    stringMenu += "<a class=\"nav-link  " + activo + "\" href=\"#\">";
                    stringMenu += "<i class='nav-icon " + nodo.GetAttribute("icon") + "'></i>";
                    stringMenu += "<p>" + nodo.GetAttribute("title");
                    stringMenu += "<i class=\"fas fa-angle-left right\"></i>";
                    stringMenu += "</p>";
                    stringMenu += "</a>";

                    stringMenu += "<ul class='nav nav-treeview'>";
                    ProcesarNodoPadre(nodo, ref stringMenu);
                    stringMenu += "</ul>";
                    stringMenu += "</li>";
                }
            }
        }

        private bool HijoActivo(XmlElement nodo)
        {
            XmlNodeList XmlPages = nodo.GetElementsByTagName("page");
            List<string> pages = new List<string>();

            foreach (XmlElement nodoHijo in XmlPages)
            {
                if (nodoHijo.GetAttribute("href").StartsWith("~/"))
                { pages.Add("/" + nodoHijo.GetAttribute("href").Replace("~/", "")); }
                else
                { pages.Add("/" + nodoHijo.GetAttribute("href")); }
            }

            string paginaActual = getPaginaActual();
            return pages.Contains(paginaActual);
        }
        private bool SoyActivo(XmlElement nodo)
        {
            string paginaMenu = "/";
            if (nodo.GetAttribute("href").StartsWith("~/"))
            { paginaMenu += nodo.GetAttribute("href").Replace("~/", ""); }
            else
            { paginaMenu += nodo.GetAttribute("href"); }

            string paginaActual = getPaginaActual();

            //if (paginaActual == paginaMenu)
            //{ this.SetearBreadcrumb(nodo); }

            return paginaActual == paginaMenu;
        }

    }
}
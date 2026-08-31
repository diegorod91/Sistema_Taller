using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Reflection.Emit;
using System.Globalization;

namespace System.Web.UI.WebControls
{
    [ToolboxData("<{0}:CustomGridView runat=server></{0}:CustomGridView>")]
    public class CustomGridView : GridView
    {
        public object Source
        {
            get { return System.Web.HttpContext.Current.Session[this.ClientID.ToString() + this.ID.ToString() + System.Web.HttpContext.Current.Request.Path.ToLower()];  }
        }
        public string SessionName
        {
            get { return this.ClientID.ToString() + this.ID.ToString() + System.Web.HttpContext.Current.Request.Path.ToLower(); }
        }
        public void CleanData()
        {
            System.Web.HttpContext.Current.Session[this.ClientID.ToString() + this.ID.ToString() + System.Web.HttpContext.Current.Request.Path.ToLower()] = null;
            this.DataBind();
        }
        protected override void OnPageIndexChanging(GridViewPageEventArgs e)
        {
            this.PageIndex = e.NewPageIndex;
            bool[] visiblecolumns = new bool[this.Columns.Count];
            for (int i = 0; i < this.Columns.Count; i++)
            {
                visiblecolumns[i] = this.Columns[i].Visible;
                this.Columns[i].Visible = true;
            }
            this.DataSource = System.Web.HttpContext.Current.Session[this.ClientID.ToString() + this.ID.ToString() + System.Web.HttpContext.Current.Request.Path.ToLower()];
            this.DataBind();
            for (int i = 0; i < this.Columns.Count; i++)
            {
                this.Columns[i].Visible = visiblecolumns[i];
            }
        }
        protected override void OnDataBound(EventArgs e)
        {
            if (!DesignMode)
            System.Web.HttpContext.Current.Session[this.ClientID.ToString() + this.ID.ToString() + System.Web.HttpContext.Current.Request.Path.ToLower()] = this.DataSource;
            base.OnDataBound(e);
        }
        protected override void OnPreRender(EventArgs e)
        {
            this.AlternatingRowStyle.CssClass = "";
            this.RowStyle.CssClass = "";
            this.HeaderStyle.CssClass = "";
            this.FooterStyle.CssClass = "";
            this.PagerStyle.CssClass = "paginationGrid";
            this.CssClass = "table table-bordered table-hover table-striped";
            JavascriptProxy.RegisterStartupScript(this, GetType(), this.ClientID, "$('.paginationGrid td table tbody tr td span').parent().addClass('pageActive');", true);

            base.OnPreRender(e);
           
        }
        protected override void OnSorting(GridViewSortEventArgs e)
        {
            bool[] visiblecolumns = new bool[this.Columns.Count];
            for (int i = 0; i < this.Columns.Count; i++)
            {
                visiblecolumns[i] = this.Columns[i].Visible;
                this.Columns[i].Visible = true;
            }

            Type tipo = System.Web.HttpContext.Current.Session[this.ClientID.ToString() + this.ID.ToString() + System.Web.HttpContext.Current.Request.Path.ToLower()].GetType();
            if (tipo == typeof(DataTable) || tipo == typeof(DataView))
            {
                this.DataSource = SortDataView(e.SortExpression); 
            }
            else
            {
                //tomo el tipo genérico dinamicamente
                Type tipoDinamico = System.Web.HttpContext.Current.Session[this.ClientID.ToString() + this.ID.ToString() + System.Web.HttpContext.Current.Request.Path.ToLower()].GetType();
                string clase = tipoDinamico.FullName.Split(new string[] { "[[" }, StringSplitOptions.None)[1];
                clase = clase.Split(',')[0] + ", " + clase.Split(',')[1];
                Type tipoClase = Type.GetType(clase);

                //Ejecuto dinamicamente el metodo generico de ordenacion
                Type type = typeof(CustomGridView);
                MethodInfo mi = type.GetMethod("Sort", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(string) }, null);
                mi = mi.MakeGenericMethod(new Type[] { tipoClase });
                try
                {
                    this.DataSource = mi.Invoke(this, new object[] { e.SortExpression });
                }
                catch (TargetInvocationException ex)
                {
                    if (ex.InnerException == null)
                    {
                        throw;
                    }
                    else
                    {
                        //Si falla nos mostrara el mensaje real del metodo que se ha invocado
                        throw ex.InnerException;
                    }
                }
            }

            this.PageIndex = 0;
            this.DataBind();

            
            for (int i = 0; i < this.Columns.Count; i++)
            {
                this.Columns[i].Visible = visiblecolumns[i];
            }
        }

        private DataView SortDataView(string sortExpression)
        {
            DataView defaultView = new DataView();
            if (typeof(DataView) == System.Web.HttpContext.Current.Session[this.ClientID.ToString() + this.ID.ToString() + System.Web.HttpContext.Current.Request.Path.ToLower()].GetType())
            {
                defaultView = (DataView)System.Web.HttpContext.Current.Session[this.ClientID.ToString() + this.ID.ToString() + System.Web.HttpContext.Current.Request.Path.ToLower()];
            }
            else
            {
                if (typeof(DataTable) == System.Web.HttpContext.Current.Session[this.ClientID.ToString() + this.ID.ToString() + System.Web.HttpContext.Current.Request.Path.ToLower()].GetType())
                {
                    defaultView = ((DataTable)this.Page.Session[this.ClientID.ToString() + this.ID.ToString() + System.Web.HttpContext.Current.Request.Path.ToLower()]).DefaultView;
                }
            }
            if (defaultView != null)
            {
                if (this.Direction == SortDirection.Ascending)
                {
                    defaultView.Sort = sortExpression + " asc";
                    this.Direction = SortDirection.Descending;
                }
                else
                {
                    defaultView.Sort = sortExpression + " desc";
                    this.Direction = SortDirection.Ascending;
                }
            }
            return defaultView;
        }
        private List<T> Sort<T>(string sortExpression)
        {
            if (this.Direction == SortDirection.Ascending)
            {
                this.Direction = SortDirection.Descending;
            }
            else
            {
                this.Direction = SortDirection.Ascending;
            }

            List<T> data = (List<T>)System.Web.HttpContext.Current.Session[this.ClientID.ToString() + this.ID.ToString() + System.Web.HttpContext.Current.Request.Path.ToLower()];
            if (this.Direction == SortDirection.Descending)
                return data.OrderByDescending(x => this.GetValueReflection(x, sortExpression)).ToList();
            else
                return data.OrderBy(x => this.GetValueReflection(x, sortExpression)).ToList();
        }
        private object GetValueReflection(object x, string sortExpression)
        {
            object obj = x;
            string[] props = sortExpression.Split('.');
            PropertyInfo prop = null;
            for (int i = 0; i < props.Length; i++)
            {
                if (prop == null)
                {
                    prop = x.GetType().GetProperty(props[i]);
                }
                else
                {
                    obj = prop.GetValue(obj, null);
                    prop = prop.PropertyType.GetProperty(props[i]);
                }
            }

            return prop.GetValue(obj, null);
        }
        private SortDirection Direction
        {
            get
            {
                object obj2 = this.ViewState["Direction"];
                if (obj2 != null)
                {
                    return (SortDirection)obj2;
                }
                return SortDirection.Descending;
            }
            set
            {
                this.ViewState["Direction"] = value;
            }
        }
    }
}

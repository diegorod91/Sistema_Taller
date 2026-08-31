using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace System.Web.UI.WebControls
{
    public static class ControlExtensions
    {
        #region ListControl

        public static void Fill(this ListControl listControl, object list, string valueMember, string displayMember, bool mostrarSeleccione)
        {
            if (list != null)
            {
                if (!list.GetType().Equals(typeof(DataTable)))
                {
                    listControl.DataSource = list;
                }
                else
                {
                    listControl.DataSource = ((DataTable)list).DefaultView;
                }
                listControl.DataTextField = displayMember;
                listControl.DataValueField = valueMember;
                listControl.DataBind();
            }
            if (mostrarSeleccione.Equals(true))
            {
                listControl.Items.Insert(0, new ListItem("Seleccione..", "-1"));
            }
        }
        public static void Fill(this ListControl listControl, object list, string valueMember, string displayMember, bool mostrarSeleccione, string textSeleccione)
        {
            if (list == null || !list.GetType().Equals(typeof(DataTable)))
            {
                listControl.DataSource = list;
            }
            else
            {
                listControl.DataSource = ((DataTable)list).DefaultView;
            }
            listControl.DataTextField = displayMember;
            listControl.DataValueField = valueMember;
            listControl.DataBind();
            if (mostrarSeleccione.Equals(true))
            {
                listControl.Items.Insert(0, new ListItem(textSeleccione, "-1"));
            }
        }
        public static void Fill(this ListControl listControl, DataSet ds, string valueMember, string displayMember, int table, bool mostrarSeleccione)
        {
            listControl.DataSource = ds.Tables[table].DefaultView;
            listControl.DataTextField = displayMember;
            listControl.DataValueField = valueMember;
            listControl.DataBind();
            if (mostrarSeleccione.Equals(true))
            {
                listControl.Items.Insert(0, new ListItem("Seleccione..", "-1"));
            }
        }
        public static void Fill(this ListControl listControl, DataSet ds, string valueMember, string displayMember, int table, bool mostrarSeleccione, string textSeleccione)
        {
            listControl.DataSource = ds.Tables[table].DefaultView;
            listControl.DataTextField = displayMember;
            listControl.DataValueField = valueMember;
            listControl.DataBind();
            if (mostrarSeleccione.Equals(true))
            {
                listControl.Items.Insert(0, new ListItem(textSeleccione, "-1"));
            }
        }
        public static void SelectItemWithListItem(this ListControl listControl, object valueMember, object displayMember)
        {
            listControl.SelectedIndex = listControl.Items.IndexOf(new ListItem(displayMember.ToString(), valueMember.ToString()));
        }
        public static void SelectItemWithValue(this ListControl listControl, object valueMember)
        {
            listControl.SelectedIndex = listControl.Items.IndexOf(listControl.Items.FindByValue(valueMember.ToString()));
        }
        public static void SelectItemWithText(this ListControl listControl, object displayMember)
        {
            listControl.SelectedIndex = listControl.Items.IndexOf(listControl.Items.FindByText(displayMember.ToString()));
        }
        public static ListItemCollection GetSelectedItems(this ListControl input)
        {
            ListItemCollection lic = new ListItemCollection();
            foreach (ListItem li in input.Items)
            {
                if (li.Selected)
                    lic.Add(li);
            }
            return lic;
        }
        public static void SetSelectedItems(this ListControl input,IEnumerable values)
        {
            foreach (object obj in values)
            {
                input.Items.FindByValue(obj.ToString()).Selected = true;
            }
        }
        public static string SelectedItemsForDynamicSQL(this ListControl input)
        {
            string aux = "";
            foreach (ListItem li in input.Items)
            {
                if (li.Selected)
                    aux += li.Value+",";
            }
            return aux.Length.Equals(0)?"()":"("+aux.Substring(0,aux.Length-1)+")" ;
        }

        #endregion ListControl

        public static void ClickOnEnter(this WebControl disparador, WebControl button)
        {
            disparador.Attributes.Add("onkeypress", "return clickButton(event,'" + button.ClientID + "');");
        }
        public static void FocusOnEnter(this WebControl disparador, WebControl control)
        {
            disparador.Attributes.Add("onkeypress", "return focusInput(event,'" + control.ClientID + "');");
        }
        public static void FocusSelect2OnEnter(this WebControl disparador, DropDownList control)
        {
            disparador.Attributes.Add("onkeypress", "return focusSelect2(event,'#" + control.ClientID + "');");
        }
        public static void FocusSelect2OnEnter(this WebControl disparador, string controlId)
        {
            disparador.Attributes.Add("onkeypress", "return focusSelect2(event,'#" + controlId + "');");
        }

        #region Validadores
        private static bool EsVacio(string cadena)
        {
            bool valido = false;
            cadena = cadena.Trim();

            if (cadena.Length == 0)
            {
                valido = true;
            }
            return valido;
        }
        public static string VerificarSelect(this DropDownList dropDownList, string campo)
        {
            if (dropDownList.Text == "-1")
            {
                return " - Por favor, seleccione una opción para el campo " + campo + ".<br />";
            }
            return "";
        }
        public static string VerificarCadena(this TextBox textBox, string campo, bool obligatorio)
        {
            if (obligatorio)
            {
                if (EsVacio(textBox.Text))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            return "";
        }
        public static string VerificarCadena(this HtmlControls.HtmlInputHidden htmlInputHidden, string campo, bool obligatorio)
        {
            if (obligatorio)
            {
                if (EsVacio(htmlInputHidden.Value))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            return "";
        }
        public static string VerificarCadena(this HiddenField hiddenField, string campo, bool obligatorio)
        {
            if (obligatorio)
            {
                if (EsVacio(hiddenField.Value))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            return "";
        }
        public static string VerificarMail(this TextBox textBox, string campo, bool obligatorio)
        {
            if (obligatorio)
            {
                if (EsVacio(textBox.Text))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            string expr = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            return ValidarConRegex(textBox, campo, expr);
        }
        public static string VerificarLong(this TextBox textBox, string campo, bool obligatorio)
        {
            string mensaje = string.Empty;
            if (obligatorio)
            {
                if (EsVacio(textBox.Text))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            long entero = 0;
            if (!long.TryParse(textBox.Text, out entero))
            {
                mensaje = " - El campo " + campo + " esta mal formado.<br />";
            }
            return mensaje;
        }

        public static string VerificarEntero(this TextBox textBox, string campo, bool obligatorio)
        {
            string mensaje = string.Empty;
            if (obligatorio)
            {
                if (EsVacio(textBox.Text))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            int entero = 0;
            if (!int.TryParse(textBox.Text, out entero))
            {
                mensaje = " - El campo " + campo + " esta mal formado.<br />";
            }
            return mensaje;
        }
        public static string VerificarShort(this TextBox textBox, string campo, bool obligatorio)
        {
            string mensaje = string.Empty;
            if (obligatorio)
            {
                if (EsVacio(textBox.Text))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            short entero = 0;
            if (!short.TryParse(textBox.Text, out entero))
            {
                mensaje = " - El campo " + campo + " esta mal formado.<br />";
            }
            return mensaje;
        }
        public static string VerificarByte(this TextBox textBox, string campo, bool obligatorio)
        {
            string mensaje = string.Empty;
            if (obligatorio)
            {
                if (EsVacio(textBox.Text))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            byte entero = 0;
            if (!byte.TryParse(textBox.Text, out entero))
            {
                mensaje = " - El campo " + campo + " esta mal formado.<br />";
            }
            return mensaje;
        }
        public static string VerificarFloat(this TextBox textBox, string campo, bool obligatorio)
        {
            string mensaje = string.Empty;
            if (obligatorio)
            {
                if (EsVacio(textBox.Text))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            float n = 0;
            if (!float.TryParse(textBox.Text.Replace(',', '.'), System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"), out n))
            {
                mensaje = " - El campo " + campo + " esta mal formado.<br />";
            }
            return mensaje;
        }
        public static string VerificarDecimal(this TextBox textBox, string campo, bool obligatorio)
        {
            string mensaje = string.Empty;
            if (obligatorio)
            {
                if (EsVacio(textBox.Text))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            decimal n = 0;
            if (!decimal.TryParse(textBox.Text.Replace(',', '.'), System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"), out n))
            {
                mensaje = " - El campo " + campo + " esta mal formado.<br />";
            }
            return mensaje;
        }
        public static string VerificarFecha(this TextBox textBox, string campo, bool obligatorio)
        {
            string mensaje = string.Empty;
            if (obligatorio)
            {
                if (EsVacio(textBox.Text))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            DateTime d;
            if (!DateTime.TryParse(textBox.Text, out d))
            {
                mensaje = " - El campo " + campo + " esta mal formado.<br />";
            }
            return mensaje;
        }
        public static string VerificarUrl(this TextBox textBox, string campo, bool obligatorio)
        {
            if (obligatorio)
            {
                if (EsVacio(textBox.Text))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            var expr = @"/http(s)?:\/\/([\w-]+\.)+[\w-]+(\/[\w- ./?%&=]*)?/";
            return ValidarConRegex(textBox, campo, expr);
        }
        private static string ValidarConRegex(TextBox textBox, string campo, string expr)
        {
            if ((!System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, expr)) && (!EsVacio(textBox.Text)))
            {
                return " - El campo " + campo + " esta mal formado.<br />";
            }
            return "";
        }
        #endregion Validadores
    }
}

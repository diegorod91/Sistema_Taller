using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace Utilities.UI
{
    public class Validation
    {
        private static bool EsVacio(string cadena)
        {
            return string.IsNullOrEmpty(cadena) || string.IsNullOrWhiteSpace(cadena);
        }

        public static string VerificarCadena(string campo, string cadena, bool obligatorio)
        {
            if (obligatorio)
            {
                if (EsVacio(cadena))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            return "";
        }

        public static string VerificarMail(string campo, string valor, bool obligatorio)
        {
            if (obligatorio)
            {
                if (EsVacio(valor))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }

            string expr = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

            if ((!Regex.IsMatch(valor, expr)) && (!EsVacio(valor)))
            {
                return " - El campo " + campo + " esta mal formado.<br />";
            }
            return "";
        }

        public static string VerificarSelect(string campo, string valor)
        {
            if (valor == "-1")
            {
                return " - Por favor, elija un " + campo + ".<br />";
            }
            return "";
        }


        public static string VerificarEntero(string campo, string valor, bool obligatorio)
        {
            if (obligatorio)
            {
                if (EsVacio(valor))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            try
            {
                int.Parse(valor);
            }
            catch
            {
                return " - El campo " + campo + " esta mal formado.<br />";
            }
            return "";
        }

        public static string VerificarFecha(string campo, string valor, bool obligatorio)
        {
            if (obligatorio)
            {
                if (EsVacio(valor))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            try
            {
                DateTime.Parse(valor);
            }
            catch
            {
                return " - El campo " + campo + " esta mal formado.<br />";
            }
            return "";
        }

        public static string VerificarReal(string campo, string valor, bool obligatorio)
        {
            if (obligatorio)
            {
                if (EsVacio(valor))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            try
            {
                double.Parse(valor);
            }
            catch
            {
                return " - El campo " + campo + " esta mal formado.<br />";
            }
            return "";
        }

        public static string VerificarListControl(string campo, ListControl control)
        {
            bool valor = false;
            for (int i = 0; i < control.Items.Count; i++)
            {
                valor = valor || control.Items[i].Selected;
            }
            if (!valor)
            {
                return " - Por favor, elija un " + campo + ".<br />";
            }
            return "";
        }

        public static string VerificarRangoEntero(string campo, string valor, int minimo, int maximo, bool obligatorio)
        {
            if (obligatorio)
            {
                if (EsVacio(valor))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            try
            {
                if (!((int.Parse(valor) > minimo) && (int.Parse(valor) < maximo)))
                {
                    return " - El campo " + campo + " no esta dentro del rango permitido.<br />";
                }
            }
            catch
            {
                return " - El campo " + campo + " esta mal formado.<br />";
            }
            return "";
        }

        public static string VerificarHora(string campo, string valor, bool obligatorio)
        {
            if (obligatorio)
            {
                if (EsVacio(valor))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            string expr = @"^(0[1-9]|1\d|2[0-3]):([0-5]\d)$";
            if ((!Regex.IsMatch(valor, expr)) && (!EsVacio(valor)))
            {
                return " - El campo " + campo + " esta mal formado.<br />";
            }
            return "";
        }

        public static string VerificarUrl(string campo, string valor, bool obligatorio)
        {
            if (obligatorio)
            {
                if (EsVacio(valor))
                {
                    return " - El campo " + campo + " es obligatorio.<br />";
                }
            }
            string expr = @"http(s)?:\/\/([\w-]+\.)+[\w-]+(\/[\w- ./?%&=]*)?";
            if ((!Regex.IsMatch(valor, expr)) && (!EsVacio(valor)))
            {
                return " - El campo " + campo + " esta mal formado.<br />";
            }
            return "";
        }
    }
}

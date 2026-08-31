using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Utilities
{
    public class LoginXML
    {
        public int IdUsuario { get; set; }
        public int Legajo { get; set; }
        public string Nomyape { get; set; }
        public uint NivelAcceso { get; set; }
        public string DescNivelAcceso { get; set; }
        public int CodDependencia { get; set; }
        public string NombreDependencia { get; set; }
        public int EsDe { get; set; }
        public int UrNum { get; set; }
        public bool EsUsuario { get; set; }
        public int SessionTimeOut { get; set; }
        public string Documento { get; set; }
        public string IP
        {
            get
            {
                try
                {
                    string IPCliente = Contexto.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (IPCliente == null) IPCliente = Contexto.ServerVariables["REMOTE_ADDR"];
                    return IPCliente;
                }
                catch
                {
                    return "";
                }
            }
        }

        public LoginXML()
        {
            Nomyape = String.Empty;
            DescNivelAcceso = String.Empty;
            NombreDependencia = String.Empty;
        }
    }

}
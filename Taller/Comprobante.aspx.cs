using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities.UI;
using Utilities;
using BOL.Entidades;
namespace SolucionBase.Taller
{
    public partial class Comprobante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                  int IdOrden = Convert.ToInt32(FwkCrypto.DecryptQueryString(Request.QueryString["id"]));
                  CargaInfoOrden(IdOrden);
                }
                //CargaInfoOrden(6);
            }
        }

        private void CargaInfoOrden(int idOrden)
        {
            var DatosOrden = Registro_Movimientos.GetById(idOrden);

            if (DatosOrden != null)
            {
                lblNroOrden.InnerHtml = DatosOrden.Id.ToString("D6");
                lblFecha.InnerHtml = DateTime.Now.ToString();
                lblClienteNombre.InnerHtml = DatosOrden.Cliente.Apeynom;
                lblClienteDoc.InnerHtml = DatosOrden.Cliente.Documento;
                lblClienteTel.InnerHtml = DatosOrden.Cliente.Telefono + " / " + DatosOrden.Cliente.Telefono_Alternativo;
                var mod = Modelo.GetModeloById(DatosOrden.IdModelo);
                string marca = mod?.Marca.Nombre ?? "";
                string nombreMod = mod?.Nombre ?? "Sin modelo";

                string equipoCompleto = !string.IsNullOrEmpty(marca) ? $"{marca} {nombreMod}" : nombreMod;
                lblEquipo.InnerHtml = equipoCompleto;
                lblFalla.InnerHtml = DatosOrden.Observaciones;
            }
            else 
            { 
             this.MostrarMensajeValidacion("No se encontró la orden solicitada.");
            }
        }

        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;
using BOL.Entidades;
using static ListadoCliente = BOL.Entidades.Cliente;

namespace SolucionBase.Clientes
{
    public partial class Cliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Ejemplo de recepción de ID por QueryString encriptado
                if (Request.QueryString["Id"] != null)
                {
                    try
                    {
                        string idDesencriptado = FwkCrypto.DecryptQueryString(Request.QueryString["Id"]);
                        int idCliente = Convert.ToInt32(idDesencriptado);
                        CargarDatosCliente(idCliente);
                    }
                    catch
                    {
                        // Manejo de error si el link es inválido
                        Response.Redirect("ListadoClientes.aspx");
                    }
                }
            }
        }

        private void CargarDatosCliente(int idCliente)
        {
            ListadoCliente cliente = ListadoCliente.ObtenerPorId(idCliente);
            if (cliente != null)
            {
                // Cargar Campos Principales
                ltrNombreBreadcrumb.Text = cliente.Apeynom;
                ltrNombreClienteTitulo.Text = cliente.Apeynom;

                TB_DetalleNombre.Text = cliente.Apeynom;
                TB_DetalleDocumento.Text = cliente.Documento;
                TB_DetalleTelefono.Text = cliente.Telefono;
                TB_DetalleCelular.Text = cliente.Telefono_Alternativo;
                TB_DetalleEmail.Text = cliente.Email;
                TB_DetalleDireccion.Text = cliente.Domicilio;

                // Si tienes campos adicionales en tu entidad Cliente (Localidad, Provincia, CP, Notas), los asignas aquí:
                // TB_DetalleLocalidad.Text = cliente.Localidad;
                // etc.

                // Cargar Historial de Órdenes del cliente (ejemplo de método que deberías tener en tu BOL)
                // List<Registro_Movimientos> ordenes = Registro_Movimientos.GetByClienteId(idCliente);
                // rptHistorialOrdenes.DataSource = ordenes;
                // rptHistorialOrdenes.DataBind();
                // ltrCantOrdenes.Text = ordenes.Count.ToString();

                // Cargar Equipos del cliente
                // List<Modelo> equipos = ... 
                // rptEquiposCliente.DataSource = equipos;
                // rptEquiposCliente.DataBind();
                // ltrCantEquipos.Text = equipos.Count.ToString();
            }
        }

        protected void btnNuevaOrdenCliente_Click(object sender, EventArgs e)
        {
            // Redirigir a NuevaOrden.aspx pasando el cliente preseleccionado si lo deseas
            int idCliente = Convert.ToInt32(FwkCrypto.DecryptQueryString(Request.QueryString["Id"]));
            string idEnc = FwkCrypto.EncryptQueryString(idCliente.ToString());
            Response.Redirect($"NuevaOrden.aspx?ClientId={idEnc}");
        }

        protected void btnGuardarCambiosCliente_Click(object sender, EventArgs e)
        {
            // Lógica para actualizar los datos si decides habilitar la edición en esta vista
        }
    }
}
}
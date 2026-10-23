using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOL.Controladoras;

namespace SolucionBase.Taller
{
    public partial class ListadoOrdenes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["exito"] == "1")
                {
                    // Muestras tu notificación de éxito (por ejemplo, con tu método Notificar)
                    this.Notificar(Notification.SUCCESS, "", "La orden se registró correctamente.");
                    CargarListadoOrdenes();
                }
                CargarListadoOrdenes();
            }

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void CargarListadoOrdenes()
        {
            List<Registro_Movimientos> ListadoOrdenes = Registro_Movimientos.GetListadoOrdenes();


            var dataSource = ListadoOrdenes.Select(o => {
                var mod = Modelo.GetModeloById(o.IdModelo);
                string marca = mod?.Marca.Nombre ?? "";
                string nombreMod = mod?.Nombre ?? "Sin modelo";
                string equipoCompleto = !string.IsNullOrEmpty(marca) ? $"{marca} - {nombreMod}" : nombreMod;

                return new
                {
                    IdOrden = o.Id,
                    FechaIngreso = o.FechaIngreso,
                    NombreCliente = o.Cliente != null ? o.Cliente.Apeynom : "Sin cliente",
                    ModeloEquipo = equipoCompleto, // Muestra por ejemplo: "Samsung - A03"
                    Estado = o.Estado != null ? o.Estado.Nombre : "Sin estado",
                    TelefonoLimpio = o.Cliente != null && !string.IsNullOrEmpty(o.Cliente.Telefono)
                                     ? o.Cliente.Telefono.Replace(" ", "").Replace("-", "").Replace("+", "")
                                     : ""
                };
            }).ToList();
            gvOrdenes.DataSource = dataSource;
            gvOrdenes.DataBind();

        }
}
}
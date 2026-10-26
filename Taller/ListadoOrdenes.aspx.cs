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
                    CargarGrillaOrdenes();
                }
                CargarGrillaOrdenes();
            }

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idOrden = Convert.ToInt32(btn.CommandArgument);

            // 2. Guardamos el ID en el HiddenField del modal de edición
            hfIdOrdenEditar.Value = idOrden.ToString();

            // 3. Buscamos la orden en la base de datos para cargar los datos actuales en los controles del modal
            var orden = Registro_Movimientos.GetById(idOrden); // O tu método correspondiente
            if (orden != null)
            {
                txtClienteEditar.Text = orden.Cliente.Apeynom; // Ajusta según las propiedades de tu objeto
                var mod = Modelo.GetModeloById(orden.IdModelo);
                string marca = mod?.Marca.Nombre ?? "";
                 string nombreMod = mod?.Nombre ?? "Sin modelo";
                string equipoCompleto = !string.IsNullOrEmpty(marca) ? $"{marca} - {nombreMod}" : "nombreMod";

                var modActual = Modelo.GetModeloById(orden.IdModelo);
                int idMarcaActual = (modActual != null) ? modActual.IdMarca : 0; // Ajusta según la propiedad de tu objeto Marca
                var listaModelosFiltrada = Modelo.GetAllModelos().Where(m => m.IdMarca == idMarcaActual).ToList();

                ddlModeloEditar.Fill(listaModelosFiltrada, "Id", "Nombre", true);

                // 3. Seleccionamos el modelo actual que posee la orden
                if (ddlModeloEditar.Items.FindByValue(orden.IdModelo.ToString()) != null)
                {
                    ddlModeloEditar.SelectedValue = orden.IdModelo.ToString();
                }

                LBL_EquipoEditar.Text = equipoCompleto;
                txtFallaEditar.Text = orden.Observaciones;
                TB_Direccion.Text = orden.Cliente.Domicilio;
                TB_Principal.Text = orden.Cliente.Telefono;
                TB_Secundario.Text = orden.Cliente.Telefono_Alternativo;
                TB_Correo.Text = orden.Cliente.Email;
                TB_DNI.Text = orden.Cliente.Documento;
            }

            // 4. Mostramos el modal de edición mediante ScriptManager
            string script = "$('#modalEditarOrden').modal('show');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModalEditar", script, true);
        }




        private void CargarGrillaOrdenes()
        {
            List<Registro_Movimientos> ListadoOrdenes = Registro_Movimientos.GetListadoOrdenes();


            var dataSource = ListadoOrdenes.Select(o =>
            {
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

        protected void btnCambiarEstado_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "CambiarEstado")
            {
                int idOrden = Convert.ToInt32(e.CommandArgument);
                hfIdOrdenModal.Value = idOrden.ToString();

                // Buscamos la orden para obtener su estado actual directamente
                var orden = Registro_Movimientos.GetById(idOrden);
                int estadoActual = orden != null ? orden.IdEstado : 0;

                // Llenamos el DDL y seleccionamos el estado actual
                ddlEstadosModal.Fill(Estado.GetAllEstados(), "Id", "Nombre", true);

                if (ddlEstadosModal.Items.FindByValue(estadoActual.ToString()) != null)
                {
                    ddlEstadosModal.SelectedValue = estadoActual.ToString();
                }

                // Mostramos el modal
                string script = "$('#modalCambiarEstado').modal('show');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModalEstado", script, true);
            }
        }

        protected void btnModificarEstado_Click(object sender, EventArgs e)
        {
            int idOrden = Convert.ToInt32(hfIdOrdenModal.Value);

            int nuevoIdEstado = Convert.ToInt32(ddlEstadosModal.SelectedValue);

            Registro_Movimientos.ActualizarEstado(idOrden, nuevoIdEstado);

            string script = "$('#modalCambiarEstado').modal('hide');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CerrarModalEstado", script, true);

            CargarGrillaOrdenes();
        }

        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            int idOrden = Convert.ToInt32(hfIdOrdenEditar.Value);

            // Recoges los nuevos valores de los TextBox del modal
            //string nuevoEquipo = LBL_EquipoEditar.Text.Trim();
            string nuevaFalla = txtFallaEditar.Text.Trim();

            Registro_Movimientos updateOrden = Registro_Movimientos.GetById(idOrden);

            updateOrden.IdModelo = Convert.ToInt32(ddlModeloEditar.SelectedValue);
            updateOrden.Observaciones = nuevaFalla;
            // Llamas a tu método de actualización en la capa de negocio/datos
            //int rta=updateOrden.RegistroMovimiento_Update(idOrden, nuevoEquipo, nuevaFalla);

            // Cerramos el modal
            string script = "$('#modalEditarOrden').modal('hide');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CerrarModalEditar", script, true);

            // Refrescamos la grilla para ver los cambios reflejados
            CargarGrillaOrdenes();
        }


    }
}

using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOL.Controladoras;
using Utilities;

namespace SolucionBase.Taller
{
    public partial class ListadoOrdenes : System.Web.UI.Page
    {
        LoginXML Usuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario = (LoginXML)Session["Usuario"];
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
                LBL_Cliente.Text = orden.Cliente.Apeynom;

                // Buscamos el modelo una sola vez
                var mod = Modelo.GetModeloById(orden.IdModelo);
                string marca = mod?.Marca.Nombre ?? "";
                string nombreMod = mod?.Nombre ?? "Sin modelo";

                // Corregido: se usa la variable nombreMod en lugar del texto literal
                string equipoCompleto = !string.IsNullOrEmpty(marca) ? $"{marca} {nombreMod}" : nombreMod;

                int idMarcaActual = (mod != null) ? mod.IdMarca : 0;
                var listaModelosFiltrada = Modelo.GetAllModelos().Where(m => m.IdMarca == idMarcaActual).ToList();
                ddlModeloEditar.Fill(listaModelosFiltrada, "Id", "Nombre", true);

                // 3. Seleccionamos el modelo actual que posee la orden
                if (ddlModeloEditar.Items.FindByValue(orden.IdModelo.ToString()) != null)
                {
                    ddlModeloEditar.SelectedValue = orden.IdModelo.ToString();
                }

                LBL_EquipoEditar.Text = equipoCompleto;
                txtFallaEditar.Text = orden.Observaciones;
                LBL_Direccion.Text = orden.Cliente.Domicilio;
                LBL_TelPrincipal.Text = orden.Cliente.Telefono;
                LBL_TelSecundario.Text = orden.Cliente.Telefono_Alternativo;
                LBL_Correo.Text = string.IsNullOrEmpty(orden.Cliente.Email) ? "se desconoce" : orden.Cliente.Email;
                LBL_DNI.Text = orden.Cliente.Documento;

                var historialTareas = Registro_Tareas.GetTareasPorOrden(idOrden); // O tu método correspondiente para traer las tareas
                gvHistorialTareas.DataSource = historialTareas;
                gvHistorialTareas.DataBind();
            }

            // 4. Mostramos el modal de edición mediante ScriptManager
            string script = "$('#modalEditarOrden').modal('show');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModalEditar", script, true);
        }




        private void CargarGrillaOrdenes(int? idEstadoFiltro = null)
        {
            List<Registro_Movimientos> ListadoOrdenes = Registro_Movimientos.GetListadoOrdenes();
            lblIngresoTaller.InnerText = ListadoOrdenes.Count(o => o.IdEstado == 1).ToString();
            lblDiagnostico.InnerText = ListadoOrdenes.Count(o => o.IdEstado == 2).ToString();
            lblPendienteRespuesto.InnerText = ListadoOrdenes.Count(o => o.IdEstado == 3).ToString();
            lblEntregado.InnerText = ListadoOrdenes.Count(o => o.IdEstado == 4).ToString();

            var listaFiltrada = ListadoOrdenes;
            if (idEstadoFiltro.HasValue)
            {
                listaFiltrada = ListadoOrdenes.Where(o => o.IdEstado == idEstadoFiltro.Value).ToList();
            }

            var dataSource = listaFiltrada.Select(o =>
            {
                var mod = Modelo.GetModeloById(o.IdModelo);
                string marca = mod?.Marca.Nombre ?? "";
                string nombreMod = mod?.Nombre ?? "Sin modelo";
                string equipoCompleto = !string.IsNullOrEmpty(marca) ? $"{marca} {nombreMod}" : nombreMod;

                return new
                {
                    IdOrden = o.Id,
                    FechaIngreso = o.FechaIngreso,
                    NombreCliente = o.Cliente != null ? o.Cliente.Apeynom : "Sin cliente",
                    ModeloEquipo = equipoCompleto,
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
            LBL_Error.Text = "";                                                           // Recuperamos el usuario de sesión para la auditoría
            int idOrden = Convert.ToInt32(hfIdOrdenModal.Value);
            int nuevoIdEstado = Convert.ToInt32(ddlEstadosModal.SelectedValue);
            string tareaRealizada = txtTareaRealizadaEstado.Text.Trim(); // El campo de texto que agregues en la UI

            if (string.IsNullOrEmpty(tareaRealizada))
            {
                LBL_Error.Text = "La descripción es obligatoria para actualizar el estado";
                return;
            }
            LoginXML usuario = Session["Usuario"] as LoginXML;
            // Armamos el XML de datos
            string xmlDoc = $"<Registro_Movimientos Id=\"{idOrden}\" IdEstado=\"{nuevoIdEstado}\" TareaRealizada=\"{System.Security.SecurityElement.Escape(tareaRealizada)}\" />";
            // Armamos el XML de usuario para la auditoría
            string xmlUsr = usuario.Serialize();
            //string xmlUsr = $"<Usuario><Nombre>{System.Security.SecurityElement.Escape(usuario)}</Nombre></Usuario>";

            // Llamada a la capa de datos/negocio que ejecuta el SP
            // Llamada al método DAO
            int resultado = Registro_Movimientos.ActualizarEstado(xmlDoc, xmlUsr);
            if (resultado > 0)
            {
                // Limpiamos el campo de texto de la tarea
                txtTareaRealizadaEstado.Text = string.Empty;

                string script = "$('#modalCambiarEstado').modal('hide');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CerrarModalEstado", script, true);

                CargarGrillaOrdenes();
            }
            else
            {
                // Manejar error o mostrar mensaje
            }

        }

        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            int idOrden = Convert.ToInt32(hfIdOrdenEditar.Value);

            // Recoges los nuevos valores de los TextBox del modal
            //string nuevoEquipo = LBL_EquipoEditar.Text.Trim();
            string nuevaFalla = txtFallaEditar.Text.Trim();

            Registro_Movimientos updateOrden = Registro_Movimientos.GetById(idOrden);
            updateOrden.Id = idOrden;
            updateOrden.IdModelo = Convert.ToInt32(ddlModeloEditar.SelectedValue);
            updateOrden.Observaciones = nuevaFalla;
            // Llamas a tu método de actualización en la capa de negocio/datos
            int rta = updateOrden.RegistroMovimiento_Update(Usuario);
            if (rta != 0)
            {
                string script = "$('#modalEditarOrden').modal('hide');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CerrarModalEditar", script, true);
                CargarGrillaOrdenes();
                this.Notificar(Notification.SUCCESS, "", "Edicion de orden exitosa");

            }
            else
            {
                this.Notificar(Notification.ERROR, "", "Error al editar la orden.Por favor intente nuevamente");
                return;
            }
            // Cerramos el modal

            // Refrescamos la grilla para ver los cambios reflejados
        }



        // Evento cuando hacen clic en cualquiera de las 4 cajas de estado
        protected void FiltrarEstado_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int idEstado = Convert.ToInt32(btn.CommandArgument);

            // Recargamos la grilla aplicando el filtro de ese estado específico
            CargarGrillaOrdenes(idEstado);
        }

        // Evento para blanquear el filtro y ver el listado completo
        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            CargarGrillaOrdenes(null);
        }
    }
}

using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace SolucionBase
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTablero();

            }
        }
        protected void rptOrdenes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Avanzar")
            {
                int idOrden = Convert.ToInt32(e.CommandArgument);

                // 1. Buscamos la orden y actualizamos su estado al siguiente en la cadena
                var orden = Registro_Movimientos.GetById(idOrden);
                if (orden != null)
                {
                    if (orden.Estado.Nombre == "Ingreso al taller")
                        orden.Estado.Nombre = "Diagnostico";
                    else if (orden.Estado.Nombre == "Diagnostico")
                        orden.Estado.Nombre = "Pendiente de respuesta";
                    else if (orden.Estado.Nombre == "Pendiente de respuesta")
                        orden.Estado.Nombre = "Entregado";

                    // 2. Guardamos los cambios en la base de datos
                    // orden.UpdateEstado(); // O tu método correspondiente de la capa BOL

                    // 3. Recargamos el tablero para que la tarjeta se mueva de columna
                    CargarTablero();
                }
            }
        }
        private void CargarTablero()
        {
            // 1. Traemos todas las órdenes crudas de la base de datos
            var todasLasOrdenes = Registro_Movimientos.GetListadoOrdenes();

            if (todasLasOrdenes != null)
            {
                // 2. Proyectamos y formateamos todos los datos una sola vez
                var ordenesProyectadas = todasLasOrdenes.Select(o =>
                {
                    var mod = Modelo.GetModeloById(o.IdModelo);
                    string marca = mod?.Marca.Nombre ?? "";
                    string nombreMod = mod?.Nombre ?? "Sin modelo";
                    string equipoCompleto = !string.IsNullOrEmpty(marca) ? $"{marca} - {nombreMod}" : nombreMod;

                    return new
                    {
                        IdOrden = o.Id,
                        Fecha = o.FechaIngreso,
                        ClienteNombre = o.Cliente != null ? o.Cliente.Apeynom : "Sin cliente",
                        EquipoModelo = equipoCompleto,
                        IdEstado = o.IdEstado, // Necesario para filtrar por columna
                        EstadoNombre = o.Estado != null ? o.Estado.Nombre : "Sin estado",
                        TelefonoLimpio = o.Cliente != null && !string.IsNullOrEmpty(o.Cliente.Telefono)
                                         ? o.Cliente.Telefono.Replace(" ", "").Replace("-", "").Replace("+", "")
                                         : ""
                    };
                }).ToList();

                // 3. Columna: Ingreso al taller (IdEstado == 1)
                var listaIngreso = ordenesProyectadas.Where(o => o.IdEstado == 1).ToList();
                rptIngreso.DataSource = listaIngreso;
                rptIngreso.DataBind();
                lblCountIngreso.InnerText = listaIngreso.Count.ToString();

                // 4. Columna: Diagnóstico (IdEstado == 2)
                var listaDiagnostico = ordenesProyectadas.Where(o => o.IdEstado == 2).ToList();
                rptDiagnostico.DataSource = listaDiagnostico;
                rptDiagnostico.DataBind();
                lblCountDiagnostico.InnerText = listaDiagnostico.Count.ToString();

                // 5. Columna: Pendiente de respuesta (IdEstado == 3)
                var listaPendiente = ordenesProyectadas.Where(o => o.IdEstado == 3).ToList();
                rptPendiente.DataSource = listaPendiente;
                rptPendiente.DataBind();
                lblCountPendiente.InnerText = listaPendiente.Count.ToString();

                // 6. Columna: Entregado (IdEstado == 4)
                var listaEntregado = ordenesProyectadas.Where(o => o.IdEstado == 4).ToList();
                rptEntregado.DataSource = listaEntregado;
                rptEntregado.DataBind();
                lblCountEntregado.InnerText = listaEntregado.Count.ToString();
            }
        }
        [System.Web.Services.WebMethod]
        public static bool CambiarEstadoConTarea(int idOrden, int idEstado, string tareaRealizada)
        {
            try
            {
                // 1. Obtener el usuario logueado de la sesión actual
                LoginXML usuario = HttpContext.Current.Session["Usuario"] as LoginXML;

                // Convertir el objeto usuario a XML (según cómo lo maneje tu arquitectura)
                // Si ya tenés un método que serializa el usuario a XML, lo usás acá.
                string xmlUsr = usuario.Serialize(); // Ajustá según tu helper

                // 2. Armar el XML para el parámetro @XML que espera tu Store Procedure
                string xmlDoc = $"<Registro_Movimientos Id=\"{idOrden}\" IdEstado=\"{idEstado}\" TareaRealizada=\"{System.Security.SecurityElement.Escape(tareaRealizada)}\" />";

                // 3. Instanciar y ejecutar el método de tu capa BOL que llama al Store Procedure SQL
                // Ejemplo: Registro_Movimientos.CambiarEstado(xmlMovimiento, xmlUsuarioStr);
                int resultado = Registro_Movimientos.ActualizarEstado(xmlDoc, xmlUsr);

                return true;
            }
            catch (Exception ex)
            {
                // Loguear error si es necesario
                return false;
            }
        }
    }
}
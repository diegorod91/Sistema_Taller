using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;
using Utilities.UI;



namespace SolucionBase.Taller
{
    public partial class NuevaOrden : System.Web.UI.Page
    {
        public string EstadoFisicoGlobal { get; private set; } = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //CargarClientes();
                IniciarInfo();

            }
        }

        private void IniciarInfo()
        {
            //List<Cliente> ListaCliente= Cliente.GetAllClientes("Id", "Nombre", true);
            ddlCliente.Fill(Cliente.GetAllClientes(), "Id", "ApeyNom", true);
            ddlCategoria.Fill(Categoria.GetAllCategorias(), "Id", "Nombre", true);
            ddlMarca.Fill(Marca.GetAllMarcas(), "Id", "Nombre", true);
            ddlModelo.Fill(Modelo.GetAllModelos(), "Id", "Nombre", true);
            DDL_TipoOperacion.Fill(TipoOperacion.GetAllTiposOperacion(), "Id", "Nombre", true);
            DDL_Estado.Fill(Estado.GetAllEstados(), "Id", "Nombre", true);
            

        }


        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategoria = Convert.ToInt32(ddlCategoria.SelectedValue);

            if (idCategoria > 0)
            {
                // Filtra las marcas relacionadas a la categoría elegida
                var listadodeMarcas = Modelo.MarcasByIdCategoria(idCategoria);
                ddlMarca.Fill(listadodeMarcas, "Id", "Nombre", true);
            }
            else
            {
                // Si vuelve a la opción por defecto, recarga todas las marcas
                ddlMarca.Fill(Marca.GetAllMarcas(), "Id", "Nombre", true);
            }

            // Al cambiar la categoría, se resetea el combo de modelos
            ddlModelo.Items.Clear();
            ddlModelo.Items.Insert(0, new ListItem("Seleccione..", "0"));
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategoria = Convert.ToInt32(ddlCategoria.SelectedValue);
            int idMarca = Convert.ToInt32(ddlMarca.SelectedValue);

            if (idMarca > 0)
            {
                // Trae los modelos filtrados por Marca y Categoría desde tu capa BOL
                var listadoModelos = Modelo.GetModelosByCategoriaYMarca(idCategoria, idMarca);
                ddlModelo.Fill(listadoModelos, "Id", "Nombre", true);
            }
            else
            {
                ddlModelo.Items.Clear();
                ddlModelo.Items.Insert(0, new ListItem("Seleccione..", "0"));
            }
        }



        protected void btnGuardarOrden_Click(object sender, EventArgs e)
        {
            string msj = "";
            msj += ValidarClienteSeleccionado();
            msj += ValidarModeloSeleccionado();
            msj += ValidarDiagnosticoInicial();
            msj += ValidarPresupuesto_AnticipoEstado();

            if (msj != "")
            {
                this.MostrarMensajeValidacion(msj);
                return;
            }

            try
            {
                // 1. Capturar valores de los controles
                int idCliente = Convert.ToInt32(ddlCliente.SelectedValue);
                int idModelo = Convert.ToInt32(ddlModelo.SelectedValue);
                int tipoOperacion = Convert.ToInt32(DDL_TipoOperacion.SelectedValue);
                int estadoInicial = Convert.ToInt32(DDL_Estado.SelectedValue);

                DateTime fechaIngreso = DateTime.Now;
                DateTime fechaEntrega = Convert.ToDateTime(TB_FechaEntrega.Text);
                decimal totalPresupuesto = Convert.ToDecimal(TB_Presupuesto.Text.Replace('.', ','));
                decimal anticipo = string.IsNullOrEmpty(TB_Anticipo.Text) ? 0 : Convert.ToDecimal(TB_Anticipo.Text.Replace('.', ','));

                // 2. Consolidar toda la info de recepción en el campo Observaciones
                string falla = TB_Observaciones.Text.Trim();
                string clave = TB_ClaveEquipo.Text.Trim();
                LoginXML usuario = Session["Usuario"] as LoginXML;
                //string observacionesExtras = txtObservaciones?.Text.Trim() ?? ""; // Por si tienes un cuadro de notas extra
                string nombreTecnico = usuario.Nomyape;
                string observacionesCompletas = $"[Falla]: {falla} | " +
                                                $"[Clave/PIN]: {(string.IsNullOrEmpty(clave) ? "Ninguna" : clave)} | " +
                                                $"[Estado Físico]: {EstadoFisicoGlobal} | " +
                                                $"[Seña/Anticipo]: ${anticipo} |" +
                                                $"[Recibió/Técnico]: {nombreTecnico}";
                //$"[Notas]: {observacionesExtras}";
                Registro_Movimientos nuevoMovimiento = new Registro_Movimientos();
                nuevoMovimiento.IdCliente = idCliente;
                nuevoMovimiento.IdModelo = idModelo;
                nuevoMovimiento.Observaciones = observacionesCompletas;
                nuevoMovimiento.FechaEntrega = fechaEntrega;
                nuevoMovimiento.IdEstado = estadoInicial;
                nuevoMovimiento.IdTipoOperacion = tipoOperacion;
                nuevoMovimiento.Total = totalPresupuesto;
                nuevoMovimiento.FechaIngreso = DateTime.Now;

                usuario = Session["Usuario"] as LoginXML;
                int idOrden = nuevoMovimiento.RegistroMovimiento_Save(usuario);



                // 4. Envío automático por WhatsApp si corresponde
                if (idOrden > 0 && CHK_AvisoWhatsapp.Checked)
                {
                    //int idCliente = Convert.ToInt32(ddlCliente.SelectedValue); // O hfIdCliente.Value

                    // Traemos los datos del cliente desde tu lógica de negocio
                    Cliente cliente = Cliente.ObtenerPorId(idCliente);

                    if (cliente != null)
                    {
                        // string telefono = cliente.Telefono;
                        string nombreCliente = cliente.Apeynom;

                        // ... aquí usas el teléfono para armar la URL de WhatsApp ...
                    }
                    string numeroOrdenFormateado = idOrden.ToString("D6");
                    string telefonoLimpios = cliente.Telefono_Alternativo.Replace(" ", "").Replace("-", "").Replace("+", "");
                    //string mensaje = $"Hola {cliente.Apeynom}, tu equipo {ddlModelo.SelectedItem.Text} ingresó al taller de RD solution's.";
                    string mensaje = $"¡Hola {cliente.Apeynom}! Te escribimos de RD solution's. " +
                          $"Te confirmamos que ingresó tu equipo ({ddlModelo.SelectedItem.Text}) con la orden N° {numeroOrdenFormateado}. " +
                          $"Motivo: {falla}. " +
                          $"En breve te estaremos contactando para informarte el presupuesto. ¡Gracias por confiar en nosotros!";

                    // ==========================================
                    // OPCIÓN A: Enlace directo wa.me (Activo)
                    // ==========================================
                    // Guardas la orden...
                    string urlWhatsApp = $"https://wa.me/{telefonoLimpios}?text={Uri.EscapeDataString(mensaje)}";

                    // Asignamos el link al botón del modal
                    lnkWhatsAppModal.HRef = urlWhatsApp;

                    // Inyectamos el link directamente en el elemento mediante JavaScript y abrimos el modal
                    string script = $"document.getElementById('{lnkWhatsAppModal.ClientID}').href = '{urlWhatsApp}'; $('#modalExito').modal('show');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModalWsp", script, true);

                    // ==========================================
                    /*
                    // Para usar esta opción asíncrona, asegúrate de que el método contenedor sea async o usa .Wait() / .Result
                    bool enviado = WhatsAppService.EnviarMensajeAsync(telefono, mensaje).Result;
                    */
                }

            }
            catch (Exception ex)
            {
                this.MostrarMensajeValidacion("Ocurrió un error al guardar la orden: " + ex.Message);
            }

        }

        protected void btnGuardarCliente_Click(object sender, EventArgs e)
        {
            string msj = ValidarInfoCliente();
            if (msj != "")
            {
                // Mostrar mensaje de error
                this.MostrarMensajeValidacion(msj);

                return;
            }
            else
            {
                Cliente newclient = NuevoCliente();
                //newclient.Registro_Movimientos = new List<Registro_Movimientos>(); //<- no hay movimientos aun

                LoginXML usuario = Session["Usuario"] as LoginXML;
                int idCliente = newclient.Cliente_Save(usuario);
                if (idCliente != 0)
                {
                    this.Notificar(Notification.SUCCESS, "", "Cliente agregado correctamente");
                    ddlCliente.Fill(Cliente.GetAllClientes(), "Id", "ApeyNom", true);
                    // Seleccionar el cliente recién agregado
                    ListItem item = ddlCliente.Items.FindByText(newclient.Apeynom);
                    if (item != null)
                    {
                        ddlCliente.SelectedIndex = ddlCliente.Items.IndexOf(item);

                        // 3. Guardar el ID real obtenido del Value del DropDownList en el HiddenField
                        hfClienteId.Value = ddlCliente.SelectedValue;
                    }// Recargar el DropDownList de clientes
                    LimpiarFormulario();
                    pnlBoxCliente.Visible = false; // Oculta el Box

                }
                else
                {
                    this.MostrarMensajeValidacion("Error al guardar el cliente. Intente nuevamente.");
                }

            }

        }

        public string GenerarEnlaceWhatsApp(string telefonoCliente, string nombreCliente, string modeloEquipo)
        {
            // Limpiamos el teléfono por si tiene espacios o guiones, asegurando el formato internacional (ej: 549...)
            string telefonoLimpios = telefonoCliente.Replace(" ", "").Replace("-", "").Replace("+", "");

            // Armamos el mensaje personalizado
            string mensaje = $"Hola {nombreCliente}, tu equipo {modeloEquipo} ingresó al taller de RD solution's.";

            // Codificamos el mensaje para que soporte espacios (%20) y tildes correctamente en la URL
            string mensajeCodificado = Uri.EscapeDataString(mensaje);

            // Retornamos el link listo para usar
            return $"https://wa.me/{telefonoLimpios}?text={mensajeCodificado}";
        }
        private Cliente NuevoCliente()
        {
            Cliente newclient = new Cliente();
            newclient.Apeynom = TB_NombreApellido.Text;
            //newclient.Apellidos = TB_Apellido.Text;
            newclient.Documento = TB_DNI.Text;
            newclient.Domicilio = TB_Direccion.Text;
            newclient.Telefono = TB_Telefono.Text;
            newclient.Telefono_Alternativo = TB_TelefonoSecundario.Text;
            newclient.Email = TB_Correo.Text;
            return newclient;
        }
        private void LimpiarFormulario()
        {
            // < Mo < deloId.Value = "0";
            TB_NombreApellido.Text = string.Empty;
            TB_Direccion.Text = string.Empty;
            TB_Correo.Text = string.Empty;
            TB_Telefono.Text = string.Empty;
            TB_TelefonoSecundario.Text = string.Empty;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlBoxCliente.Visible = false; // Oculta el Box
        }

        protected void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            pnlBoxCliente.Visible = true;
        }

        protected void BTN_GuardarModelo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TB_NombreModelo.Text))
            {
                return;
            }

            int idCategoria = Convert.ToInt32(ddlCategoria.SelectedValue);
            int idMarca = Convert.ToInt32(ddlMarca.SelectedValue);

            Modelo nuevoModelo = new Modelo
            {
                Idcategoria = idCategoria,
                IdMarca = idMarca,
                Nombre = TB_NombreModelo.Text.Trim(),
                NroModeloTecnico = TB_ModeloTecnico.Text.Trim()
            };
            LoginXML usuario = Session["Usuario"] as LoginXML;

            // 1. Guardar modelo en Base de Datos
            int nuevoModeloId = nuevoModelo.Modelo_Save(usuario);
            // 2. Recargar los modelos de la marca en el DropDownList principal
            CargarGridModelos();

            // 3. Seleccionar el nuevo modelo creado automáticamente
            ddlModelo.SelectedValue = nuevoModeloId.ToString();
            ddlModelo.SelectedIndex = ddlModelo.Items.IndexOf(ddlModelo.Items.FindByText(nuevoModelo.Nombre));


            // 4. Cerrar el modal mediante JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModalModelo", @"
               $('#modalAltaModelo').modal('hide');
               $('body').removeClass('modal-open');
               $('.modal-backdrop').remove();
               $('body').css('padding-right', ''); // Por si queda el padding aplicado
           ", true);
        }
        private void CargarGridModelos()
        {
            int idCat = Convert.ToInt32(ddlCategoria.SelectedValue);
            int idMarca = Convert.ToInt32(ddlMarca.SelectedValue);

            //List<Modelo> modelo = Modelo.GetAllModelos();
            List<Modelo> modelosFiltrados = Modelo.GetAllModelos()
        .Where(m => m.Idcategoria == idCat && m.IdMarca == idMarca)
        .ToList();
            ddlModelo.DataSource = modelosFiltrados.ToList();
            ddlModelo.DataTextField = "Nombre";
            ddlModelo.DataValueField = "Id";
            ddlModelo.DataBind();
        }

        protected void LNK_NuevoModelo_Click(object sender, EventArgs e)
        {
            // Validar que primero hayan seleccionado Categoría y Marca afuera
            if (ddlCategoria.SelectedValue == "-1" || string.IsNullOrEmpty(ddlCategoria.SelectedValue) ||
                ddlMarca.SelectedValue == "-1" || string.IsNullOrEmpty(ddlMarca.SelectedValue))
            {
                this.MostrarMensajeValidacion("Debe seleccionar categoría y marca");
                return;
            }

            // Mostrar categoría y marca en el modal
            LBL_CategMarca.Text = $"{ddlCategoria.SelectedItem.Text} > {ddlMarca.SelectedItem.Text}";

            TB_NombreModelo.Text = string.Empty;
            TB_ModeloTecnico.Text = string.Empty;

            // Abrir el modal mediante JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModalModelo", "$('#modalAltaModelo').modal('show');", true);
        }

        private string ValidarClienteSeleccionado()
        {
            string msj = "";
            if (ddlCliente.SelectedItem.Value == "-1")
            {
                msj += "Debe seleccionar un cliente valido.<br>";
                return msj;
            }
            else
            {
                return msj;
            }
        }
        private string ValidarModeloSeleccionado()
        {
            string msj = "";
            if (ddlModelo.SelectedItem.Value == "-1")
            {
                msj += "Debe seleccionar un modelo válido.<br>";
                return msj;
            }
            else
            {
                return msj;
            }
        }

        private string ValidarDiagnosticoInicial()
        {
            string msj = "";
            msj += ControlExtensions.VerificarSelect(DDL_TipoOperacion, "Tipo de operación");
            msj += ControlExtensions.VerificarSelect(DDL_Estado, "Estado");
            msj += ControlExtensions.VerificarCadena(TB_Observaciones, "Observaciones", true);
            return msj;
        }
        private string ValidarPresupuesto_AnticipoEstado()
        {

            string msj = "";
            msj += ControlExtensions.VerificarCadena(TB_ClaveEquipo, "Contraseña", false);
            msj += ControlExtensions.VerificarDecimal(TB_Presupuesto, "Presupuesto", true);
            msj += ControlExtensions.VerificarDecimal(TB_Anticipo, "Seña/Anticipo", false);
            EstadoFisicoGlobal = ControlExtensions.ObtenerResumenChecklist(
                  ("Encendido", CHK_Encendido), ("Apagado", CHK_Apagado),
                  ("Golpes", CHK_Golpes), ("Humedad", CHK_Humedad),
                  ("Sin SIM", CHK_SinSim), ("Con Funda", CHK_Accesorios)
            );
            msj += ControlExtensions.VerificarFecha(TB_FechaEntrega, "Fecha entrega", true);
            msj += ControlExtensions.VerificarCheckbox(CHK_AvisoWhatsapp, "Aviso por Wsp", false);


            return msj;
        }
        private string ValidarInfoCliente()
        {
            string msj = "";

            msj += ControlExtensions.VerificarCadena(TB_NombreApellido, "Nombre y Apellido", true);
            // Expresión regular flexible para DNI (argentino o extranjero)
            // o Pasaporte (entre 5 y 20 caracteres alfanuméricos)
            string patronDocumento = @"^[a-zA-Z0-9\.\-]{5,20}$";

            string docLimpio = TB_DNI.Text.Trim();

            if (!string.IsNullOrEmpty(docLimpio))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(docLimpio, patronDocumento))
                {
                    msj += "El formato del documento no es válido. Debe tener entre 5 y 20 caracteres alfanuméricos.<br/>";
                }
            }
            msj += ControlExtensions.VerificarTelefono(TB_Telefono, "Teléfono principal", true);
            msj += ControlExtensions.VerificarTelefono(TB_TelefonoSecundario, "Teléfono secundario", true);
            msj += ControlExtensions.VerificarCadena(TB_Direccion, "Dirección", true);
            msj += ControlExtensions.VerificarMail(TB_Correo, "Correo Electrónico", false);

            return msj;
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoOrdenes.aspx?exito=1", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;
using Utilities.UI;



namespace SolucionBase.Taller
{
    public partial class NuevaOrden : System.Web.UI.Page
    {
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
            int idCliente = Convert.ToInt32(ddlCliente.SelectedValue);
            int idModelo = Convert.ToInt32(ddlModelo.SelectedValue);

            if (idCliente == 0 || idModelo == 0)
            {
                // Mostrar alerta / notificación Toastr
                return;
            }

            //using (var db = new TallerDbContext())
            //{
            //    var nuevaOrden = new RegistroMovimiento
            //    {
            //        IdCliente = idCliente,
            //        IdModelo = idModelo,
            //        IdTipoOperacion = Convert.ToInt32(ddlTipoOperacion.SelectedValue),
            //        IdEstado = Convert.ToInt32(ddlEstado.SelectedValue),
            //        Observaciones = txtObservaciones.Text.Trim(),
            //        FechaIngreso = DateTime.Now,
            //        Total = 0
            //    };
            //
            //    db.RegistroMovimientos.Add(nuevaOrden);
            //    db.SaveChanges();
            //
            //    // Redirigir a la vista de la orden creada o al listado general
            //    Response.Redirect("Default.aspx");
            //}
        }

        protected void btnGuardarCliente_Click(object sender, EventArgs e)
        {
            string msj = ValidarInfoCliente();
            if(msj != "")
            {
                // Mostrar mensaje de error
                this.MostrarMensajeValidacion(msj);

                return;
            }
            else
            {
                Cliente newclient = NuevoCliente();
                LoginXML usuario = Session["Usuario"] as LoginXML;
                int idCliente = newclient.Cliente_Save(usuario);
                // Recargar el DropDownList de clientes
                ddlCliente.Fill(Cliente.GetAllClientes(), "Id", "ApeyNom", true);
                // Seleccionar el cliente recién agregado
                ddlCliente.SelectedItem.Text = newclient.Apeynom;
            }

            LimpiarFormulario();
            pnlBoxCliente.Visible = false; // Oculta el Box
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

            // 4. Cerrar el modal mediante JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModalModelo", "$('#modalAltaModelo').modal('hide');", true);
        }
        private void CargarGridModelos()
        {
            int idCat = Convert.ToInt32(ddlCategoria.SelectedValue);
            int idMarca = Convert.ToInt32(ddlMarca.SelectedValue);

            List<Modelo> modelo = Modelo.GetAllModelos();
            ddlModelo.DataSource = modelo.ToList();
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

        private string  ValidarClienteSeleccionado()
        {
            string msj = "";
            if (ddlCliente.SelectedItem.Value == "-1")
            {
                msj += "Debe seleccionar un cliente valido.";
                return msj;
            }
            else
            {
                return msj;
            }
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
            msj += ControlExtensions.VerificarCadena(TB_Telefono, "Teléfono principal", true);
            msj += ControlExtensions.VerificarCadena(TB_TelefonoSecundario, "Teléfono secundario", true);
            msj += ControlExtensions.VerificarCadena(TB_Direccion, "Dirección", true);
            msj += ControlExtensions.VerificarCadena(TB_Correo, "Correo Electrónico", false);

            return msj;
        }
    }
}
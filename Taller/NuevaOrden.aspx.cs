using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



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
            ddlCliente.Fill(Cliente.GetAllClientes(), "Id", "Nombres", true);
            ddlCategoria.Fill(Categoria.GetAllCategorias(), "Id", "Nombre", true);
            ddlMarca.Fill(Marca.GetAllMarcas(), "Id", "Nombre", true);
            ddlModelo.Fill(Modelo.GetAllModelos(), "Id", "Nombre", true);
            ddlTipoOperacion.Fill(TipoOperacion.GetAllTiposOperacion(), "Id", "Nombre", true);
            ddlEstado.Fill(Estado.GetAllEstados(), "Id", "Nombre", true);

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
    }
}
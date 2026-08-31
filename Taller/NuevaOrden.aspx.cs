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
             CargarClientes();
                CargarCategorias();
                CargarTiposOperacion();
                CargarEstados();
            }
        }

        private void CargarClientes()
        {
            
            //using (var db = new  TallerDbContext())
            //{
            //    ddlCliente.DataSource = 
            //        .Select(c => new { c.Id, NombreCompleto = c.Nombres + " " + c.Apellidos + " (" + c.Documento + ")" })
            //        .ToList();
            //    ddlCliente.DataTextField = "NombreCompleto";
            //    ddlCliente.DataValueField = "Id";
            //    ddlCliente.DataBind();
            //    ddlCliente.Items.Insert(0, new ListItem("-- Seleccionar Cliente --", "0"));
            //}
        }

        private void CargarCategorias()
        {
            //using (var db = new TallerDbContext())
            //{
            //    ddlCategoria.DataSource = db.Categorias.ToList();
            //    ddlCategoria.DataTextField = "Nombre";
            //    ddlCategoria.DataValueField = "Id";
            //    ddlCategoria.DataBind();
            //    ddlCategoria.Items.Insert(0, new ListItem("-- Seleccionar Categoría --", "0"));
            //}
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategoria = Convert.ToInt32(ddlCategoria.SelectedValue);

            //using (var db = new TallerDbContext())
            //{
            //    // Obtenemos las marcas con modelos en esta categoría
            //    var marcas = db.Modelos
            //        .Where(m => m.IdCategoria == idCategoria)
            //        .Select(m => m.Marca)
            //        .Distinct()
            //        .ToList();
            //
            //    ddlMarca.DataSource = marcas;
            //    ddlMarca.DataTextField = "Nombre";
            //    ddlMarca.DataValueField = "Id";
            //    ddlMarca.DataBind();
            //    ddlMarca.Items.Insert(0, new ListItem("-- Seleccionar Marca --", "0"));
            //}

            ddlModelo.Items.Clear();
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategoria = Convert.ToInt32(ddlCategoria.SelectedValue);
            int idMarca = Convert.ToInt32(ddlMarca.SelectedValue);

            //using (var db = new TallerDbContext())
            //{
            //    ddlModelo.DataSource = db.Modelos
            //        .Where(m => m.IdCategoria == idCategoria && m.IdMarca == idMarca)
            //        .ToList();
            //    ddlModelo.DataTextField = "Nombre";
            //    ddlModelo.DataValueField = "Id";
            //    ddlModelo.DataBind();
            //    ddlModelo.Items.Insert(0, new ListItem("-- Seleccionar Modelo --", "0"));
            //}
        }

        private void CargarTiposOperacion()
        {
            //using (var db = new TallerDbContext())
            //{
            //    ddlTipoOperacion.DataSource = db.TiposOperacion.ToList();
            //    ddlTipoOperacion.DataTextField = "Nombre";
            //    ddlTipoOperacion.DataValueField = "Id";
            //    ddlTipoOperacion.DataBind();
            //}
        }

        private void CargarEstados()
        {
           // using (var db = new TallerDbContext())
           // {
           //     ddlEstado.DataSource = db.Estados.ToList();
           //     ddlEstado.DataTextField = "Nombre";
           //     ddlEstado.DataValueField = "Id";
           //     ddlEstado.DataBind();
           // }
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
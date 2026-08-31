using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SolucionBase.Catalogo
{
    public partial class ABMCatalogo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CargarDesplegablesFiltro();
                CargarGridModelos();
                CargarGridMarcas();
                CargarGridCategorias();
            }
        }


        private void CargarDesplegablesFiltro()
        {
            //using (var db = new TallerDbContext())
            //{
            //    ddlFiltroCategoria.DataSource = db.Categorias.ToList();
            //    ddlFiltroCategoria.DataTextField = "Nombre";
            //    ddlFiltroCategoria.DataValueField = "Id";
            //    ddlFiltroCategoria.DataBind();
            //    ddlFiltroCategoria.Items.Insert(0, new ListItem("-- Todas --", "0"));
            //
            //    ddlFiltroMarca.DataSource = db.Marcas.ToList();
            //    ddlFiltroMarca.DataTextField = "Nombre";
            //    ddlFiltroMarca.DataValueField = "Id";
            //    ddlFiltroMarca.DataBind();
            //    ddlFiltroMarca.Items.Insert(0, new ListItem("-- Todas --", "0"));
            //}
        }

        private void CargarGridModelos()
        {
            //int idCat = Convert.ToInt32(ddlFiltroCategoria.SelectedValue);
            //int idMarca = Convert.ToInt32(ddlFiltroMarca.SelectedValue);

            //using (var db = new TallerDbContext())
            //{
            //    var query = db.Modelos.Include("Marca").Include("Categoria").AsQueryable();
            //
            //    if (idCat > 0) query = query.Where(m => m.IdCategoria == idCat);
            //    if (idMarca > 0) query = query.Where(m => m.IdMarca == idMarca);
            //
            //    gvModelos.DataSource = query.ToList();
            //    gvModelos.DataBind();
            //}
        }

        protected void ddlFiltroCategoria_SelectedIndexChanged(object sender, EventArgs e) => CargarGridModelos();
        protected void ddlFiltroMarca_SelectedIndexChanged(object sender, EventArgs e) => CargarGridModelos();

        protected void btnNuevoModelo_Click(object sender, EventArgs e)
        {
            hfModeloId.Value = "0";
            txtModalNombre.Text = string.Empty;
            txtModalTecnico.Text = string.Empty;
            ltrTituloModal.Text = "Nuevo Modelo";

            CargarDesplegablesModal();
            ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "abrirModalModelo();", true);
        }

        private void CargarDesplegablesModal()
        {
            //using (var db = new TallerDbContext())
            //{
            //    ddlModalCategoria.DataSource = db.Categorias.ToList();
            //    ddlModalCategoria.DataTextField = "Nombre";
            //    ddlModalCategoria.DataValueField = "Id";
            //    ddlModalCategoria.DataBind();
            //
            //    ddlModalMarca.DataSource = db.Marcas.ToList();
            //    ddlModalMarca.DataTextField = "Nombre";
            //    ddlModalMarca.DataValueField = "Id";
            //    ddlModalMarca.DataBind();
            //}
        }

        protected void btnGuardarModelo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfModeloId.Value);

            //using (var db = new TallerDbContext())
            //{
            //    Modelo modelo = id == 0 ? new Modelo() : db.Modelos.Find(id);
            //
            //    modelo.Nombre = txtModalNombre.Text.Trim();
            //    modelo.NumeroModeloTecnico = txtModalTecnico.Text.Trim();
            //    modelo.IdCategoria = Convert.ToInt32(ddlModalCategoria.SelectedValue);
            //    modelo.IdMarca = Convert.ToInt32(ddlModalMarca.SelectedValue);
            //
            //    if (id == 0) db.Modelos.Add(modelo);
            //
            //    db.SaveChanges();
            //}

            CargarGridModelos();
            ScriptManager.RegisterStartupScript(this, GetType(), "PopClose", "cerrarModalModelo();", true);
        }

        protected void gvModelos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                //using (var db = new TallerDbContext())
                //{
                //    var m = db.Modelos.Find(id);
                //    if (m != null)
                //    {
                //        hfModeloId.Value = m.Id.ToString();
                //        txtModalNombre.Text = m.Nombre;
                //        txtModalTecnico.Text = m.NumeroModeloTecnico;
                //
                //        CargarDesplegablesModal();
                //        ddlModalCategoria.SelectedValue = m.IdCategoria.ToString();
                //        ddlModalMarca.SelectedValue = m.IdMarca.ToString();
                //
                //        ltrTituloModal.Text = "Editar Modelo";
                //        ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "abrirModalModelo();", true);
                //    }
                //}
            }   //
        }

        private void CargarGridMarcas() { /* Cargar gvMarcas */ }
        private void CargarGridCategorias() { /* Cargar gvCategorias */ }
        protected void btnNuevaMarca_Click(object sender, EventArgs e) { }
        protected void btnNuevaCategoria_Click(object sender, EventArgs e) { }
    }
}


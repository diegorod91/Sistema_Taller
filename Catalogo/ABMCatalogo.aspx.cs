using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;


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
            //DDL_Categoria.Fill(Categoria.GetAllCategorias(), "Id", "Nombre", true);
            DDL_FiltroCategoria.Fill(Categoria.GetAllCategorias(), "Id", "Nombre", true);
            DDL_FiltroMarca.Fill(Marca.GetAllMarcas(), "Id", "Nombre", true);

        }

        private void CargarGridModelos()
        {
            int idCat = Convert.ToInt32(DDL_FiltroCategoria.SelectedValue);
            int idMarca = Convert.ToInt32(DDL_FiltroMarca.SelectedValue);

            List<Modelo> modelo = Modelo.GetAllModelos();
            gvModelos.DataSource = modelo.ToList();
            gvModelos.DataBind();
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

        protected void DDL_FiltroCategoria_SelectedIndexChanged(object sender, EventArgs e) => CargarGridModelos();
        protected void DDL_FiltroMarca_SelectedIndexChanged(object sender, EventArgs e) => CargarGridModelos();

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
            DDL_Categoria.Fill(Categoria.GetAllCategorias(), "Id", "Nombre", true);
            DDL_Marca.Fill(Marca.GetAllMarcas(), "Id", "Nombre", true);

        }

        protected void btnGuardarModelo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfModeloId.Value);
            string nombre = txtModalNombre.Text.Trim();
            string tecnico = txtModalTecnico.Text.Trim();
            int idCategoria = Convert.ToInt32(DDL_Categoria.SelectedValue);
            int idMarca = Convert.ToInt32(DDL_Marca.SelectedValue);

            Modelo model = id == 0 ? new BOL.Entidades.Modelo() : Modelo.GetAllModelos().FirstOrDefault(m => m.Id == id);
            model.Nombre = nombre; 
            model.NroModeloTecnico = tecnico;
            model.Idcategoria = idCategoria;
            model.IdMarca = idMarca;
            LoginXML usuario = Session["UsuarioActual"] as LoginXML;

            if(id == 0)
            {
                model.Modelo_Save(usuario);
            }
            else
            {
                model.Update_Save(usuario);
            }

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


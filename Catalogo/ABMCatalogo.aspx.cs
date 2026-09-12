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
            LoginXML usuario = Session["Usuario"] as LoginXML;

            if (id == 0)
            {
                model.Modelo_Save(usuario);
            }
            else
            {
                model.Update_Save(usuario);
            }

            CargarGridModelos();
            ScriptManager.RegisterStartupScript(this, GetType(), "PopClose", "cerrarModalModelo();", true);
        }

        protected void gvModelos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Modelo updatemodelo = Modelo.GetModeloById(id);
                hfModeloId.Value = updatemodelo.Id.ToString();
                txtModalNombre.Text = updatemodelo.Nombre;
                txtModalTecnico.Text = updatemodelo.NroModeloTecnico;
                //
                CargarDesplegablesModal();
                DDL_Categoria.SelectedValue = updatemodelo.Idcategoria.ToString();
                DDL_Marca.SelectedValue = updatemodelo.IdMarca.ToString();

                ltrTituloModal.Text = "Editar Modelo";
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "abrirModalModelo();", true);
                //    }
                //}
            }   //
        }

        private void CargarGridMarcas()
        {
            List<Marca> listaMarcas = Marca.GetAllMarcas();
            gvMarcas.DataSource = listaMarcas;
            gvMarcas.DataBind();
        }
        private void CargarGridCategorias()
        {
            /* Cargar gvCategorias */
            List<Categoria> listaCategorias = Categoria.GetAllCategorias();
            gvCategorias.DataSource = listaCategorias;
            gvCategorias.DataBind();
        }
        protected void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            hfTabActiva.Value = "#tab-marcas";
            ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "abrirModalMarca();", true);
        }
        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            hfTabActiva.Value = "#tab-categorias";

            ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "abrirModalCategoria();", true);
        }

        protected void BTN_AltaMarca_Click(object sender, EventArgs e)
        {
            Marca nuevaMarca = new Marca();
            nuevaMarca.Nombre = TB_Marca.Text.Trim();

            //Verificar si la marca ya existe
            bool marcaExistente = Marca.GetAllMarcas().Any(m => m.Nombre.Equals(nuevaMarca.Nombre, StringComparison.OrdinalIgnoreCase));
            if (marcaExistente)
            {
                this.MostrarMensajeValidacion("La marca ya existe. Por favor, ingrese un nombre diferente.");
            }
            else
            {
                LoginXML usuario = Session["Usuario"] as LoginXML;
                nuevaMarca.Marca_Save(usuario);
                CargarGridMarcas();
                CargarDesplegablesFiltro();
                CargarDesplegablesModal();

                TB_Marca.Text = string.Empty;
                this.Notificar(Notification.SUCCESS, "", "Marca agregada correctamente.");
                ScriptManager.RegisterStartupScript(this, GetType(), "PopClose", "cerrarModalMarca();", true);
            }


        }

        protected void BTN_AltaCategoria_Click(object sender, EventArgs e)
        {
            Categoria nuevaCategoria = new Categoria();
            nuevaCategoria.Id = 0;
            nuevaCategoria.Nombre = TB_Categoria.Text.Trim();
            nuevaCategoria.Modelo = null; //← PARA EVITAR REFERENCIA CIRCULAR
            //verificar si la categoria ya existe
            bool categoriaExistente = Categoria.GetAllCategorias().Any(c => c.Nombre.Equals(nuevaCategoria.Nombre, StringComparison.OrdinalIgnoreCase));
            if (categoriaExistente)
            {
                this.MostrarMensajeValidacion("La categoría ya existe. Por favor, ingrese un nombre diferente.");
            }
            else
            {
                LoginXML usuario = Session["Usuario"] as LoginXML;
                //var NCat = nuevaCategoria.Serialize()
                ;
                int rta = nuevaCategoria.Categoria_Save(usuario);
                CargarGridCategorias();
                CargarDesplegablesFiltro();
                CargarDesplegablesModal();


                TB_Categoria.Text = string.Empty;
                this.Notificar(Notification.SUCCESS, "", "Categoría agregada correctamente.");
                ScriptManager.RegisterStartupScript(this, GetType(), "PopClose", "cerrarModalCategoria();", true);
            }
        }

        protected void gvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            hfTabActiva.Value = "#tab-marcas";
            int idMarca = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Marca updateMarca = Marca.GetMarcaById(idMarca);
                hfModeloId.Value = updateMarca.Id.ToString();
                txtModalNombre.Text = updateMarca.Nombre;
                //
                CargarDesplegablesModal();
                DDL_Marca.SelectedValue = updateMarca.Id.ToString();

                ltrTituloModal.Text = "Editar Marca";
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "abrirModalMarca();", true);

            }
        }
        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            hfTabActiva.Value = "#tab-categorias";
            int idCategoria = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Categoria updateCategoria = Categoria.GetCategoriaById(idCategoria);
                hfModeloId.Value = updateCategoria.Id.ToString();
                txtModalNombre.Text = updateCategoria.Nombre;
                //
                CargarDesplegablesModal();
                DDL_Marca.SelectedValue = updateCategoria.Id.ToString();

                ltrTituloModal.Text = "Editar Marca";
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "abrirModalCategoria();", true);

            }
        }
    }
}

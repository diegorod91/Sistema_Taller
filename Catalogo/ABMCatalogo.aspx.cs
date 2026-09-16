using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;
using Utilities.UI;

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

        private void CargarDesplegablesModal()
        {
            DDL_Categoria.Fill(Categoria.GetAllCategorias(), "Id", "Nombre", true);
            DDL_Marca.Fill(Marca.GetAllMarcas(), "Id", "Nombre", true);
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

                CargarDesplegablesModal();
                DDL_Categoria.SelectedValue = updatemodelo.Idcategoria.ToString();
                DDL_Marca.SelectedValue = updatemodelo.IdMarca.ToString();

                ltrTituloModal.Text = "Editar Modelo";
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "abrirModalModelo();", true);
            }
        }

        private void CargarGridMarcas()
        {
            List<Marca> listaMarcas = Marca.GetAllMarcas();
            gvMarcas.DataSource = listaMarcas;
            gvMarcas.DataBind();
        }

        private void CargarGridCategorias()
        {
            List<Categoria> listaCategorias = Categoria.GetAllCategorias();
            gvCategorias.DataSource = listaCategorias;
            gvCategorias.DataBind();
        }

        protected void gvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            hfTabActiva.Value = "#tab-marcas";
            int idMarca = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Marca updateMarca = Marca.GetMarcaById(idMarca);

                // Asignar al HiddenField de Marcas y a su respectivo TextBox
                HF_Marca.Value = updateMarca.Id.ToString();
                TB_Marca.Text = updateMarca.Nombre;

                LTR_ModalMarca.Text = "Editar Marca";
                string script = "abrirModalMarca(); restaurarTabActiva();";
                ScriptManager.RegisterStartupScript(this, GetType(), "PopEditMarca", script, true);
            }
        }

        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            hfTabActiva.Value = "#tab-categorias";
            int idCategoria = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Categoria updateCategoria = Categoria.GetCategoriaById(idCategoria);

                // Asignar al HiddenField de Categorías y a su respectivo TextBox
                HF_Categoria.Value = updateCategoria.Id.ToString();
                TB_Categoria.Text = updateCategoria.Nombre;

                LTR_ModalCategoria.Text = "Editar Categoría";
                string script = "abrirModalCategoria(); restaurarTabActiva();";
                ScriptManager.RegisterStartupScript(this, GetType(), "PopEditCat", script, true);
            }
        }

        #region Acciones_Botones

        protected void btnGuardarModelo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(string.IsNullOrEmpty(hfModeloId.Value) ? "0" : hfModeloId.Value);
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

        protected void btnNuevoModelo_Click(object sender, EventArgs e)
        {
            hfModeloId.Value = "0";
            txtModalNombre.Text = string.Empty;
            txtModalTecnico.Text = string.Empty;
            ltrTituloModal.Text = "Nuevo Modelo";

            CargarDesplegablesModal();
            ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "abrirModalModelo();", true);
        }

        protected void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            hfTabActiva.Value = "#tab-marcas";
            HF_Marca.Value = "0";
            TB_Marca.Text = string.Empty;
            LTR_ModalMarca.Text = "Nueva Marca";

            ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "abrirModalMarca();", true);
        }

        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            hfTabActiva.Value = "#tab-categorias";
            HF_Categoria.Value = "0";
            TB_Categoria.Text = string.Empty;
            LTR_ModalCategoria.Text = "Nueva Categoría";

            ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "abrirModalCategoria();", true);
        }

        protected void BTN_AltaMarca_Click(object sender, EventArgs e)
        {
            string mensajeValidacion = validarAltaMarca();
            if (mensajeValidacion != "")
            {
                this.MostrarMensajeValidacion(mensajeValidacion);
            }
            else
            {
                LoginXML usuario = Session["Usuario"] as LoginXML;
                int idMarca = Convert.ToInt32(string.IsNullOrEmpty(HF_Marca.Value) ? "0" : HF_Marca.Value);

                if (idMarca != 0)
                {
                    // Lógica para Edición
                    Marca updateMarca = Marca.GetMarcaById(idMarca);
                    updateMarca.Nombre = TB_Marca.Text.Trim();
                    updateMarca.Modelo = null; // Asegúrate de establecer Modelo en null si no lo estás utilizando
                    int rta = updateMarca.Marca_Update(usuario); // Asegúrate de llamar al método Update de tu BOL
                    if (rta > 0)
                    {
                        this.Notificar(Notification.SUCCESS, "", "Marca actualizada correctamente.");
                        CargarGridMarcas();
                        CargarDesplegablesFiltro();
                        CargarDesplegablesModal();

                        TB_Marca.Text = string.Empty;
                        HF_Marca.Value = "0";
                        ScriptManager.RegisterStartupScript(this, GetType(), "PopClose", "cerrarModalMarca();", true);
                    }
                    else
                    {
                        this.Notificar(Notification.ERROR, "", "Error al actualizar la marca. Por favor, inténtelo de nuevo.");
                    }
                }
                else
                {
                    // Lógica para Alta
                    Marca nuevaMarca = new Marca();
                    nuevaMarca.Nombre = TB_Marca.Text.Trim();
                    nuevaMarca.Modelo = null; // Asegúrate de establecer Modelo en null si no lo estás utilizando
                    int rta = nuevaMarca.Marca_Save(usuario);
                    if (rta <= 0)
                    {
                        this.Notificar(Notification.ERROR, "", "Error al agregar la marca. Por favor, inténtelo de nuevo.");
                        return;
                    }
                    this.Notificar(Notification.SUCCESS, "", "Marca agregada correctamente.");
                }

                CargarGridMarcas();
                CargarDesplegablesFiltro();
                CargarDesplegablesModal();

                TB_Marca.Text = string.Empty;
                HF_Marca.Value = "0";
                ScriptManager.RegisterStartupScript(this, GetType(), "PopClose", "cerrarModalMarca();", true);
            }
        }

        protected void BTN_AltaCategoria_Click(object sender, EventArgs e)
        {
            string mensajeValidacion = validarAltaCategoria();
            if (mensajeValidacion != "")
            {
                this.MostrarMensajeValidacion(mensajeValidacion);
            }
            else
            {
                LoginXML usuario = Session["Usuario"] as LoginXML;
                int idCategoria = Convert.ToInt32(string.IsNullOrEmpty(HF_Categoria.Value) ? "0" : HF_Categoria.Value);

                if (idCategoria != 0)
                {
                    // Lógica para Edición
                    Categoria updateCategoria = Categoria.GetCategoriaById(idCategoria);
                    updateCategoria.Nombre = TB_Categoria.Text.Trim();
                    updateCategoria.Modelo = null;
                    int rta = updateCategoria.Categoria_Update(usuario); // Asegúrate de llamar al método Update de tu BOL
                    if (rta > 0)
                    {
                        this.Notificar(Notification.SUCCESS, "", "Categoría actualizada correctamente.");
                        CargarGridCategorias();
                        CargarDesplegablesFiltro();
                        CargarDesplegablesModal();

                        TB_Categoria.Text = string.Empty;
                        HF_Categoria.Value = "0";
                        ScriptManager.RegisterStartupScript(this, GetType(), "PopClose", "cerrarModalCategoria();", true);
                    }
                    else
                    {
                        this.Notificar(Notification.ERROR, "", "Error al actualizar la categoría. Por favor, inténtelo de nuevo.");
                    }

                }
                else
                {
                    // Lógica para Alta
                    Categoria nuevaCategoria = new Categoria();
                    nuevaCategoria.Id = 0;
                    nuevaCategoria.Nombre = TB_Categoria.Text.Trim();
                    nuevaCategoria.Modelo = null;
                    int rta = nuevaCategoria.Categoria_Save(usuario);
                    if (rta > 0)
                    {
                        this.Notificar(Notification.SUCCESS, "", "Categoría agregada correctamente.");
                        CargarGridCategorias();
                        CargarDesplegablesFiltro();
                        CargarDesplegablesModal();

                        TB_Categoria.Text = string.Empty;
                        HF_Categoria.Value = "0";
                        ScriptManager.RegisterStartupScript(this, GetType(), "PopClose", "cerrarModalCategoria();", true);
                    }
                    else
                    {
                        this.Notificar(Notification.ERROR, "", "Error al agregar la categoría. Por favor, inténtelo de nuevo.");

                    }


                }
            }
        }

        #endregion

        #region Validaciones

        private string validarAltaCategoria()
        {
            string mensaje = string.Empty;
            int idCategoria = Convert.ToInt32(string.IsNullOrEmpty(HF_Categoria.Value) ? "0" : HF_Categoria.Value);

            if (string.IsNullOrWhiteSpace(TB_Categoria.Text))
            {
                mensaje += "El nombre de la categoría no puede estar vacío. ";
            }
            else
            {
                // Al validar, ignoramos la misma categoría si estamos editando (c.Id != idCategoria)
                bool categoriaExistente = Categoria.GetAllCategorias()
                    .Any(c => c.Nombre.Equals(TB_Categoria.Text.Trim(), StringComparison.OrdinalIgnoreCase) && c.Id != idCategoria);

                if (categoriaExistente)
                {
                    mensaje += "La categoría ya existe. Por favor, ingrese un nombre diferente.";
                }
            }
            return mensaje;
        }

        private string validarAltaMarca()
        {
            string mensaje = string.Empty;
            int idMarca = Convert.ToInt32(string.IsNullOrEmpty(HF_Marca.Value) ? "0" : HF_Marca.Value);

            if (string.IsNullOrWhiteSpace(TB_Marca.Text))
            {
                mensaje += "El nombre de la marca no puede estar vacío. ";
            }
            else
            {
                // Al validar, ignoramos la misma marca si estamos editando (m.Id != idMarca)
                bool marcaExistente = Marca.GetAllMarcas()
                    .Any(m => m.Nombre.Equals(TB_Marca.Text.Trim(), StringComparison.OrdinalIgnoreCase) && m.Id != idMarca);

                if (marcaExistente)
                {
                    mensaje += "La marca ya existe. Por favor, ingrese un nombre diferente.";
                }
            }
            return mensaje;
        }

        #endregion
    }
}
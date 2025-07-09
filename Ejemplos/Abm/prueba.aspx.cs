//using BOL.Controladoras;
//using BOL.Entidades;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Script.Services;
//using System.Web.Services;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using Utilities;
//using Utilities.UI;

//namespace WebApplication
//{
//	public partial class prueba : System.Web.UI.Page
//	{
//		LoginXML user;
//		protected void Page_Load(object sender, EventArgs e)
//		{
//			user = Session["Usuario"] as LoginXML;
//			TB_Busqueda.ClickOnEnter(BTN_Filtrar);
//			if (!IsPostBack)
//			{


//				ActualizarGrilla();
//			}
//		}
//		protected void ActualizarGrilla(List<Consecionaria> list = null)
//		{
//			if (list == null)
//			{
//				list = Consecionaria.GetAll();
//			}
//			LTR_countGrilla.Text = list.Count.ToString();
//			Grilla.DataSource = list;
//			Grilla.DataBind();
//		}
//		protected void Btn_Alta_Click(object sender, EventArgs e)
//		{
//			string msjValidacion = ValidarAlta();
//			if (!string.IsNullOrEmpty(msjValidacion))
//			{
//				this.MostrarMensajeValidacion(msjValidacion);
//				return;
//			}
//			Consecionaria newConsecionaria = new Consecionaria();
//			newConsecionaria.NombreConsecionaria = TB_NombreConsecionaria.Text;
//			newConsecionaria.Direccion = TB_Direccion.Text;
//			newConsecionaria.Save(user);
//			ActualizarGrilla();
//			this.Notificar(Notification.SUCCESS, "Alta", "La operaci&oacute;n ha sido exitosa.");

//		}
//		private string ValidarAlta()
//		{
//			string mensaje = string.Empty;
//			mensaje += Validation.VerificarCadena("NombreConsecionaria", TB_NombreConsecionaria.Text, true);
//			mensaje += Validation.VerificarCadena("Direccion", TB_Direccion.Text, true);

//			return mensaje;
//		}
//		protected void Btn_Filtrar_Click(object sender, EventArgs e)
//		{
//			List<Consecionaria> list = Consecionaria.GetListByNombreConsecionaria(TB_Busqueda.Text);

//			ActualizarGrilla(list);
//		}
//		protected void Btn_Editar_Click(object sender, EventArgs e)
//		{
//			string msjValidacion = ValidarEditar();
//			if (!string.IsNullOrEmpty(msjValidacion))
//			{
//				this.MostrarMensajeValidacion(msjValidacion);
//				return;
//			}
//			Consecionaria editConsecionaria = new Consecionaria();
//			editConsecionaria.Id = HF_IdAfectado.Value.ToInt();
//			editConsecionaria.NombreConsecionaria = TB_Edit_NombreConsecionaria.Text;
//			editConsecionaria.Direccion = TB_Edit_Direccion.Text;
//			editConsecionaria.Update(user);
//			ActualizarGrilla();
//			this.Notificar(Notification.SUCCESS, "Editar", "La operaci&oacute;n ha sido exitosa.");

//		}
//		private string ValidarEditar()
//		{
//			string mensaje = string.Empty;
//			mensaje += Validation.VerificarCadena("NombreConsecionaria", TB_Edit_NombreConsecionaria.Text, true);
//			mensaje += Validation.VerificarCadena("Direccion", TB_Edit_Direccion.Text, true);

//			return mensaje;
//		}
//		protected void Btn_Cancelar_Click(object sender, EventArgs e)
//		{
//			Response.Redirect("prueba.aspx");
//		}

//		protected void Btn_Eliminar_Click(object sender, EventArgs e)
//		{
//			Consecionaria deleteConsecionaria = new Consecionaria();
//			deleteConsecionaria.Id = HF_IdAfectado.Value.ToInt();
//			if (!deleteConsecionaria.SePuedeEliminar())
//			{
//				this.Notificar(Notification.WARNING, "Baja", "La operaci&oacute;n no se pudo realizar. El registro esta relacionado con otros registros.");
//				return;
//			}
//			deleteConsecionaria.Delete(user);
//			ActualizarGrilla();
//			this.Notificar(Notification.SUCCESS, "Baja", "La operaci&oacute;n ha sido exitosa.");

//		}

//	}
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SolucionBase.Ejemplos.Abm
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
    }
}

//		protected void btnAlta_Click(object sender, EventArgs e)
//		{
//			string msjValidacion = ValidarAlta();
//			if (!string.IsNullOrEmpty(msjValidacion))
//			{
//				this.MostrarMensajeValidacion(msjValidacion);
//				return; // este Return genera el corte de la instruccion;
//			}
//			//Empresa em = new Empresa(TBNombre.Text, TBDomicilio.Text);
//			//em.Save();
//			this.Notificar(Notification.SUCCESS, "Agregar", "El Alta fue Satisfactoria.");

//			ActualizarGrilla();
//		}
//		private string ValidarAlta()
//		{
//			string mensaje = string.Empty;
//			mensaje += Validation.VerificarCadena("Nombre ", TBNombre.Text, true);
//			mensaje += Validation.VerificarCadena("Domicilio ", TBDomicilio.Text, true);

//			return mensaje;
//		}

//		protected void btnEdicion_Click(object sender, EventArgs e)
//		{
//			string msjValidacion = ValidarEditar();
//			if (!string.IsNullOrEmpty(msjValidacion))
//			{
//				this.MostrarMensajeValidacion(msjValidacion);
//				return;
//			}
//			//Empresa empresa = new Empresa(idAfectado.Value.ToInt(), TBNombreE.Text, TBDomicilioE.Text);
//			//empresa.Update();
//			this.Notificar(Notification.SUCCESS, "Editar", "El Edici&oacute;n fue Satisfactoria.");
//			ActualizarGrilla();
//		}
//		private string ValidarEditar()
//		{
//			string mensaje = string.Empty;
//			mensaje += Validation.VerificarCadena("Nombre ", TBNombreE.Text, true);
//			mensaje += Validation.VerificarCadena("Domicilio ", TBDomicilioE.Text, true);

//			return mensaje;
//		}

//		protected void btnBorrar_Click(object sender, EventArgs e)
//		{
//			//Empresa empresa = new Empresa(idAfectado.Value.ToInt());
//			//empresa.Delete();
//			this.Notificar(Notification.SUCCESS, "Eliminar", "La Baja fue Satisfactoria.");
//			ActualizarGrilla();
//		}

//		#region Actualizar Grilla
//		protected void ActualizarGrilla(List<Empresa> list = null)
//		{
//			if (list == null)
//			{
//				list = Empresa.GetAll();
//			}
//			LTR_countGrilla.Text = list.Count.ToString();
//			Grilla.DataSource = list;
//			Grilla.DataBind();
//		}
//		protected void btnFiltrarxNombre_Click(object sender, EventArgs e)
//		{
//			string nombre = TBNombreBusqueda.Text;
//			//ActualizarGrilla(Empresa.GetList(nombre));
//		}
//		#endregion

//		protected void btnLimpiar_Click(object sender, EventArgs e)
//		{
//			TBNombreBusqueda.Text = "";
//			this.ActualizarGrilla();
//		}
//	}
//}
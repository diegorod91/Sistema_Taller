//using BOL.Entidades;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace WebApplication.Ejemplos.Notificacion
//{
//	public partial class index : System.Web.UI.Page
//	{
//		protected void Page_Load(object sender, EventArgs e)
//		{

//		}

//		protected void btnNotificar_Click(object sender, EventArgs e)
//		{
//			//ScriptManager.RegisterStartupScript(this, GetType(), "notificar", "notificar(1, 'titulo', 'mensaje');", true);
//			this.Notificar(Notification.INFO, "Notificacion", "Notificacion desde el servidor");
//		}

//		protected void btnSinNotificar_Click(object sender, EventArgs e)
//		{
//		}

//		protected void btnNotificarAjax_Click(object sender, EventArgs e)
//		{
//			Thread.Sleep(3000);
//			this.Notificar(Notification.WARNING, "Notificacion", "Notificacion desde el servidor mediante ajax");
//		}
//	}
//}
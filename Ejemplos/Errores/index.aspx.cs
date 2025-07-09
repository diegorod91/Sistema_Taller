using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.Ejemplos.Errores
{
	public partial class index : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{ }
		}

		protected void btn_error_Click(object sender, EventArgs e)
		{
			throw new Exception("Error de prueba");
		}
		protected void btn_errorAjax_Click(object sender, EventArgs e)
		{
			throw new Exception("Error de prueba con ajax");
		}
		protected void btnBloqueo_Click(object sender, EventArgs e)
		{
			Thread.Sleep(5000);
		}
	}
}
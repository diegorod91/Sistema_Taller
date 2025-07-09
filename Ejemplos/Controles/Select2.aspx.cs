//using BOL.Entidades;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace WebApplication.Ejemplos.Controles
//{
//	public partial class Select2 : System.Web.UI.Page
//	{
//		protected void Page_Load(object sender, EventArgs e)
//		{
//			if (!IsPostBack)
//			{
//				DDL_empresa.Fill(Empresa.GetAll(), "IdEmpresa", "NombreEmpresa", true);
//			}
//		}

//		protected void BTN_Buscar_Click(object sender, EventArgs e)
//		{
//			string id = DDL_empresa.SelectedItem.Value;
//		}
//	}
//}
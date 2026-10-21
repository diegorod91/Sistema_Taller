using BOL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SolucionBase.Taller
{
    public partial class ListadoOrdenes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["exito"] == "1")
                {
                    // Muestras tu notificación de éxito (por ejemplo, con tu método Notificar)
                    this.Notificar(Notification.SUCCESS, "", "La orden se registró correctamente.");
                }
            }

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }
    }
}
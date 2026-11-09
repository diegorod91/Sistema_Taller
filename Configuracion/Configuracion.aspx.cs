using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOL;
using BOL.Entidades;
using Utilities;
using Utilities.UI;
namespace SolucionBase.Configuracion
{
    public partial class Configuracion : System.Web.UI.Page
    {
        LoginXML usuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = (LoginXML)Session["Usuario"];
            if(!IsPostBack)
            {
                
            }
        }

        protected void BtnGuardarConfig_Click(object sender, EventArgs e)
        {
            string errorValidacion = ValidarInfoTaller();
            if (!string.IsNullOrEmpty(errorValidacion))
            {
                this.MostrarMensajeValidacion(errorValidacion);
            }
            else
            {
                ConfiguracionTaller newconfig= new ConfiguracionTaller
                {
                    
                     NombreTaller= TB_NombreTaller.Text.Trim(),
                    Direccion = TB_DireccionTaller.Text.Trim(),
                    TelefonoContacto = TB_TelefonoTaller.Text.Trim(),
                    MensajePlantillaWsp = TB_PlantillaWsp.Text.Trim()
                };

                string rutaLogoRelativa = null;

                // Validar si subió un nuevo logo
                if (FuLogoTaller.HasFile)
                {
                    string extension = System.IO.Path.GetExtension(FuLogoTaller.FileName).ToLower();
                    if (extension == ".png" || extension == ".jpg" || extension == ".jpeg")
                    {
                        // Nombre único para evitar conflictos entre talleres
                        string nombreArchivo = "logo_" + Guid.NewGuid().ToString() + extension;
                        string carpetaServidor = Server.MapPath("~/Uploads/Logos/");

                        // Asegurar que la carpeta exista
                        if (!System.IO.Directory.Exists(carpetaServidor))
                        {
                            System.IO.Directory.CreateDirectory(carpetaServidor);
                        }

                        string rutaCompleta = System.IO.Path.Combine(carpetaServidor, nombreArchivo);
                        FuLogoTaller.SaveAs(rutaCompleta);

                        rutaLogoRelativa = "/Uploads/Logos/" + nombreArchivo;
                    }
                }
                int rta= ConfiguracionTaller.Taller_Save(newconfig, usuario);
                // Guardar en Base de Datos (ejecutando tu comando SQL o Stored Procedure)
                // Si 'rutaLogoRelativa' viene con valor, actualizás el campo LogoUrl. Si es null, mantenés el anterior.

                LblMensaje.Text = "✔ Configuración guardada correctamente.";
                LblMensaje.CssClass = "d-block mt-3 font-weight-bold text-center text-success";
            }
        }

        private string ValidarInfoTaller()
        {
            string mje = string.Empty;

            mje += ControlExtensions.VerificarCadena(TB_NombreTaller, "El nombre del taller.", true);
            mje += ControlExtensions.VerificarCadena(TB_DireccionTaller, "Dirección del taller.", true);
            mje += ControlExtensions.VerificarTelefono(TB_TelefonoTaller, "Telefono del taller.", true);
            mje += ControlExtensions.VerificarCadena(TB_PlantillaWsp, "Plantilla", true);

            // Validar el archivo si el usuario seleccionó uno
            if (FuLogoTaller.HasFile)
            {
                // Límite: Máximo 2 MB -> 2 * 1024 * 1024 bytes
                int tamanoMaximoBytes = 2 * 1024 * 1024;

                if (FuLogoTaller.PostedFile.ContentLength > tamanoMaximoBytes)
                {
                    mje += "- El archivo es demasiado pesado. El tamaño máximo permitido es de 2 MB.<br/>";
                }

                string extension = System.IO.Path.GetExtension(FuLogoTaller.FileName).ToLower();
                if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
                {
                    mje += "- Formato de imagen no válido. Solo se permiten archivos PNG o JPG.<br/>";
                }
            }

            return mje;
        }
    }
}
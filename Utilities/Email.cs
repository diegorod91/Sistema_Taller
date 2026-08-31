using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Net.Mail;

namespace Utilities
{
    public class Email
    {
        /// <summary>
        /// Envia mail.
        /// </summary>
        /// <param name="asunto">Asunto del mail.</param>
        /// <param name="cuerpo">Cuerpo del mail.</param>
        /// <param name="cuentaOrigen">Cuenta con la que se envia el mail,si es null toma el correo del atributo "from" de la sección "smtp" del web.config</param>
        /// <param name="cuentasDestino">Destinatarios del mail, uno o mas destinatarios separados por coma.</param>
        public static void SendMail(string asunto, string cuerpo, string cuentaOrigen, string cuentasDestino)
        {
            Email.SendMail(asunto, cuerpo, cuentaOrigen, cuentasDestino, null, null, false, null);
        }
        /// <summary>
        /// Envia mail.
        /// </summary>
        /// <param name="asunto">Asunto del mail.</param>
        /// <param name="cuerpo">Cuerpo del mail.</param>
        /// <param name="cuentaOrigen">Cuenta con la que se envia el mail,si es null toma el correo del atributo "from" de la sección "smtp" del web.config</param>
        /// <param name="cuentasDestino">Destinatarios del mail, uno o mas destinatarios separados por coma.</param>
        /// <param name="isBodyHtml">Especifica si el cuerpo del mail es un HTML</param>
        public static void SendMail(string asunto, string cuerpo, string cuentaOrigen, string cuentasDestino, bool isBodyHtml)
        {
            Email.SendMail(asunto, cuerpo, cuentaOrigen, cuentasDestino, null, null, isBodyHtml, null);
        }
        /// <summary>
        /// Envia mail.
        /// </summary>
        /// <param name="asunto">Asunto del mail.</param>
        /// <param name="cuerpo">Cuerpo del mail.</param>
        /// <param name="cuentaOrigen">Cuenta con la que se envia el mail,si es null toma el correo del atributo "from" de la sección "smtp" del web.config</param>
        /// <param name="cuentasDestino">Destinatarios del mail, uno o mas destinatarios separados por coma.</param>
        /// <param name="adjuntos">Archivos adjuntos.</param>
        public static void SendMail(string asunto, string cuerpo, string cuentaOrigen, string cuentasDestino, params Attachment[] adjuntos)
        {
            Email.SendMail(asunto, cuerpo, cuentaOrigen, cuentasDestino, null, null, false, adjuntos);
        }
        /// <summary>
        /// Envia mail.
        /// </summary>
        /// <param name="asunto">Asunto del mail.</param>
        /// <param name="cuerpo">Cuerpo del mail.</param>
        /// <param name="cuentaOrigen">Cuenta con la que se envia el mail,si es null toma el correo del atributo "from" de la sección "smtp" del web.config</param>
        /// <param name="cuentasDestino">Destinatarios del mail, uno o mas destinatarios separados por coma.</param>
        /// <param name="isBodyHtml">Especifica si el cuerpo del mail es un HTML</param>
        /// <param name="adjuntos">Archivos adjuntos.</param>
        public static void SendMail(string asunto, string cuerpo, string cuentaOrigen, string cuentasDestino, bool isBodyHtml, params Attachment[] adjuntos)
        {
            Email.SendMail(asunto, cuerpo, cuentaOrigen, cuentasDestino, null, null, isBodyHtml, adjuntos);
        }
        /// <summary>
        /// Envia mail.
        /// </summary>
        /// <param name="asunto">Asunto del mail.</param>
        /// <param name="cuerpo">Cuerpo del mail.</param>
        /// <param name="cuentaOrigen">Cuenta con la que se envia el mail,si es null toma el correo del atributo "from" de la sección "smtp" del web.config</param>
        /// <param name="cuentasDestino">Destinatarios del mail, uno o mas destinatarios separados por coma.</param>
        /// <param name="cuentasDestinoCC">Destinatarios con copia del mail, uno o mas destinatarios separados por coma,si no es necesario especificar null</param>
        /// <param name="cuentasDestinoCCO">Destinatarios con copia oculta del mail, uno o mas destinatarios separados por coma,si no es necesario especificar null</param>
        public static void SendMail(string asunto, string cuerpo, string cuentaOrigen, string cuentasDestino, string cuentasDestinoCC, string cuentasDestinoCCO)
        {
            Email.SendMail(asunto, cuerpo, cuentaOrigen, cuentasDestino, cuentasDestinoCC, cuentasDestinoCCO, false, null);
        }
        /// <summary>
        /// Envia mail.
        /// </summary>
        /// <param name="asunto">Asunto del mail.</param>
        /// <param name="cuerpo">Cuerpo del mail.</param>
        /// <param name="cuentaOrigen">Cuenta con la que se envia el mail,si es null toma el correo del atributo "from" de la sección "smtp" del web.config</param>
        /// <param name="cuentasDestino">Destinatarios del mail, uno o mas destinatarios separados por coma.</param>
        /// <param name="cuentasDestinoCC">Destinatarios con copia del mail, uno o mas destinatarios separados por coma,si no es necesario especificar null</param>
        /// <param name="cuentasDestinoCCO">Destinatarios con copia oculta del mail, uno o mas destinatarios separados por coma,si no es necesario especificar null</param>
        /// <param name="isBodyHtml">Especifica si el cuerpo del mail es un HTML</param>
        public static void SendMail(string asunto, string cuerpo, string cuentaOrigen, string cuentasDestino, string cuentasDestinoCC, string cuentasDestinoCCO, bool isBodyHtml)
        {
            Email.SendMail(asunto, cuerpo, cuentaOrigen, cuentasDestino, cuentasDestinoCC, cuentasDestinoCCO, isBodyHtml, null);
        }
        /// <summary>
        /// Envia mail.
        /// </summary>
        /// <param name="asunto">Asunto del mail.</param>
        /// <param name="cuerpo">Cuerpo del mail.</param>
        /// <param name="cuentaOrigen">Cuenta con la que se envia el mail,si es null toma el correo del atributo "from" de la sección "smtp" del web.config</param>
        /// <param name="cuentasDestino">Destinatarios del mail, uno o mas destinatarios separados por coma.</param>
        /// <param name="cuentasDestinoCC">Destinatarios con copia del mail, uno o mas destinatarios separados por coma,si no es necesario especificar null</param>
        /// <param name="cuentasDestinoCCO">Destinatarios con copia oculta del mail, uno o mas destinatarios separados por coma,si no es necesario especificar null</param>
        /// <param name="adjuntos">Archivos adjuntos.</param>
        public static void SendMail(string asunto, string cuerpo, string cuentaOrigen, string cuentasDestino, string cuentasDestinoCC, string cuentasDestinoCCO, params Attachment[] adjuntos)
        {
            Email.SendMail(asunto, cuerpo, cuentaOrigen, cuentasDestino, cuentasDestinoCC, cuentasDestinoCCO, false, adjuntos);
        }
        /// <summary>
        /// Envia mail.
        /// </summary>
        /// <param name="asunto">Asunto del mail.</param>
        /// <param name="cuerpo">Cuerpo del mail.</param>
        /// <param name="cuentaOrigen">Cuenta con la que se envia el mail,si es null toma el correo del atributo "from" de la sección "smtp" del web.config</param>
        /// <param name="cuentasDestino">Destinatarios del mail, uno o mas destinatarios separados por coma.</param>
        /// <param name="cuentasDestinoCC">Destinatarios con copia del mail, uno o mas destinatarios separados por coma,si no es necesario especificar null</param>
        /// <param name="cuentasDestinoCCO">Destinatarios con copia oculta del mail, uno o mas destinatarios separados por coma,si no es necesario especificar null</param>
        /// <param name="isBodyHtml">Especifica si el cuerpo del mail es un HTML</param>
        /// <param name="adjuntos">Archivos adjuntos.</param>
        public static void SendMail(string asunto, string cuerpo, string cuentaOrigen, string cuentasDestino, string cuentasDestinoCC, string cuentasDestinoCCO, bool isBodyHtml, params Attachment[] adjuntos)
        {
            MailMessage mail = new MailMessage();
            if (!String.IsNullOrEmpty(cuentaOrigen))
            {
                mail.From = new MailAddress(cuentaOrigen);
            }
            mail.To.Add(cuentasDestino);
            if (!String.IsNullOrEmpty(cuentasDestinoCC))
            {
                mail.CC.Add(cuentasDestinoCC);
            }
            if (!String.IsNullOrEmpty(cuentasDestinoCCO))
            {
                mail.Bcc.Add(cuentasDestinoCCO);
            }
            mail.Subject = asunto;
            mail.Body = cuerpo;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = isBodyHtml;
            foreach (Attachment adjunto in adjuntos)
            {
                mail.Attachments.Add(adjunto);
            }
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Send(mail);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Security.Cryptography;

namespace System.Web.UI.WebControls
{
    [DefaultProperty("Text"), ToolboxData("<{0}:PasswordControl runat=server></{0}:PasswordControl>")]
    public class PasswordControl:TextBox
    {

        public PasswordControl()
        {
            this.TextMode = TextBoxMode.Password;
        }
        [Browsable(false)]

        public override TextBoxMode TextMode
        {
            get
            {
                return base.TextMode;
            }
            set
            {
                base.TextMode = value;
            }
        }
        [Category("Behavior"),
           DefaultValue(""), 
            Browsable(false)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = HashWithSalt(value,KeyEncrypt);
            }
        }
        public string KeyEncrypt
        {
            get
            {
                return (ViewState["KeyEncrypt"] == null) ? string.Empty : (string)ViewState["KeyEncrypt"];
            }
            set
            {
                ViewState["KeyEncrypt"] = value;
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            if (!this.DesignMode)
            {
                if (string.IsNullOrEmpty(KeyEncrypt))
                {
                    throw new ArgumentNullException("Debe asignar un valor a la propiedad KeyEncrypt.");
                }
                if (this.TextMode != TextBoxMode.Password)
                {
                    throw new ArgumentOutOfRangeException("Solo se permite el valor Password para propiedad TextMode.");
                }
            }
            base.OnPreRender(e);
        }
        private string HashWithSalt(string pwd, string salt)
        {
            string pwdAndSalt = string.Format("{0}+{1}", pwd, salt);
            string hwsPwd = ComputeHash(pwdAndSalt);
            return hwsPwd;
        }
        private string ComputeHash(string s)
        {
            byte[] b = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(s));
            return Convert.ToBase64String(b, 0, b.Length);
        }
    }
}

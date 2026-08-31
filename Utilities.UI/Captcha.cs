#region Using Statements

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using Utilities.UI;

#endregion

namespace  System.Web.UI.WebControls
{
    [DefaultProperty("Text"), ToolboxData("<{0}:Captcha runat=\"server\"> </{0}:Captcha>")]
    public class Captcha : CompositeControl
    {
        #region Campos
        private CaptchaImage _captchaImage;
        private Label errorMessage;
        #endregion
        #region Propiedades
        [
           Category("Captcha"),
           Themeable(false),
           DefaultValue(CaptchaCacheType.Session),
           Description("Especifica donde se va a mantener el estado del captcha."),
        ]
        public CaptchaCacheType CacheStrategy
        {
            get { return ViewState["CacheStrategy"] == null ? CaptchaCacheType.Session : (CaptchaCacheType)ViewState["CacheStrategy"]; }
            set { ViewState["CacheStrategy"] = value; }
        }
        private CaptchaImage CaptchaImage
        {
            get { return _captchaImage; }
            set { _captchaImage = value; }
        }
        [
           Category("Appearance"),
           Themeable(false),
           DefaultValue(typeof(Color), "Black"),
           Description("Color de las lineas del control."),
           TypeConverter(typeof(WebColorConverter))
        ]
        public Color LineColor
        {
            get { return ViewState["LineColor"] == null ? Color.Black : (Color)ViewState["LineColor"]; }
            set { ViewState["LineColor"] = value; }
        }
        [
          Category("Appearance"),
          Themeable(false),
          DefaultValue(typeof(Color), "Black"),
          Description("Color del ruido del control."),
          TypeConverter(typeof(WebColorConverter))
        ]
        public Color NoiseColor
        {
            get { return ViewState["NoiseColor"] == null ? Color.Black : (Color)ViewState["NoiseColor"]; }
            set { ViewState["NoiseColor"] = value; }
        }
        
        private string PrevGuid
        {
            get { return ViewState["PrevGuid"] == null ? String.Empty : (string)ViewState["PrevGuid"]; }
            set { ViewState["PrevGuid"] = value; }
        }
        [
            Category("Behavior"),
            Themeable(false),
            DefaultValue(""),
            IDReferenceProperty(typeof(TextBox)),
            Description("Id. del control que se va a validar."),
            TypeConverter(typeof(CaptchaControlCoverter))
        ]
        public string ControlToValidate
        {
            get
            {
                object obj2 = this.ViewState["ControlToValidate"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["ControlToValidate"] = value;
            }
        }
        [
           Category("Behavior"),
           Themeable(false),
           DefaultValue(5),
           Description("Cantidad de caracteres que se van a generar."),
        ]
        public int MaxLength
        {
            get { return ViewState["MaxLength"] == null ? 5 : (int)ViewState["MaxLength"]; }
            set { ViewState["MaxLength"] = value; }
        }
        [
           Category("Behavior"),
           Themeable(false),
           DefaultValue(3),
           Description("Número mínimo de segundos que se debe mostrar la imagen antes de que ser validada. Establecer en cero para desactivarlo."),
        ]
        public int TimeoutSecondsMin
        {
            get { return ViewState["TimeoutSecondsMin"] == null ? 3 : (int)ViewState["TimeoutSecondsMin"]; }
            set 
            {
                if (value > 15)
                {
                    throw new ArgumentOutOfRangeException("TimeoutSecondsMin", "Tiempo de espera debe ser inferior a 15 segundos. Los seres humanos no son tan lento!");
                }
                ViewState["TimeoutSecondsMin"] = value; 
            }
        }
        [
           Category("Behavior"),
           Themeable(false),
           DefaultValue(90),
           Description("Número máximo de segundos que se almacena la imagén del control. Establecer en cero para desactivarlo."),
        ]
        public int TimeoutSecondsMax
        {
            get { return ViewState["TimeoutSecondsMax"] == null ? 90 : (int)ViewState["TimeoutSecondsMax"]; }
            set 
            {
                if (value < 15)
                {
                    throw new ArgumentOutOfRangeException("TimeoutSecondsMax", "Tiempo de espera debe ser mayor de 15 segundos. Los seres humanos no puede escribir tan rápido!");
                }
                ViewState["TimeoutSecondsMax"] = value; 
            }
        }
        [
           Category("Behavior"),
           Themeable(false),
           DefaultValue(CaptchaImage.BackgroundNoiseLevel.Low),
           Description("Establece el nivel de ruido del fondo de la imagén que dibuja el control."),
        ]
        public CaptchaImage.BackgroundNoiseLevel BackgroundNoiseLevel
        {
            get { return ViewState["BackgroundNoiseLevel"] == null ? CaptchaImage.BackgroundNoiseLevel.Low : (CaptchaImage.BackgroundNoiseLevel)ViewState["BackgroundNoiseLevel"]; }
            set { ViewState["BackgroundNoiseLevel"] = value; }
        }
        [
           Category("Behavior"),
           Themeable(false),
           DefaultValue(CaptchaImage.FontWarpFactor.Low),
           Description("Establece el factor de deformación de la fuente que dibuja el control."),
        ]
        public CaptchaImage.FontWarpFactor FontWarpFactor
        {
            get { return ViewState["FontWarpFactor"] == null ? CaptchaImage.FontWarpFactor.Low : (CaptchaImage.FontWarpFactor)ViewState["FontWarpFactor"]; }
            set { ViewState["FontWarpFactor"] = value; }
        }
        [
           Category("Behavior"),
           Themeable(false),
           DefaultValue(CaptchaImage.LineNoiseLevel.None),
           Description("Establece el nivel de ruido de las lineas que dibuja el control."),
        ]
        public CaptchaImage.LineNoiseLevel LineNoiseLevel
        {
            get { return ViewState["LineNoiseLevel"] == null ? CaptchaImage.LineNoiseLevel.None : (CaptchaImage.LineNoiseLevel)ViewState["LineNoiseLevel"]; }
            set { ViewState["LineNoiseLevel"] = value; }
        }
        [
           Category("Behavior"),
           Themeable(false),
           DefaultValue(TipoCaracteres.LetrasYNumeros),
           Description("Establece si el control dibuja solo letras, solo números o ambos."),
        ]
        public TipoCaracteres TypeChars
        {
            get { return ViewState["TipoCaracteres"] == null ? TipoCaracteres.LetrasYNumeros : (TipoCaracteres)ViewState["TipoCaracteres"]; }
            set { ViewState["TipoCaracteres"] = value; }
        }
        [
           Category("Behavior"),
           Themeable(false),
           DefaultValue(UpperChars.MayusculasYMinusculas),
           Description("Establece si el control dibuja letras, solo en mayusculas, solo en minusculas o ambas."),
        ]
        public UpperChars UpperChars
        {
            get { return ViewState["UpperChars"] == null ? UpperChars.MayusculasYMinusculas : (UpperChars)ViewState["UpperChars"]; }
            set { ViewState["UpperChars"] = value; }
        }
        [DefaultValue(typeof(Color), "White")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        private string ErrorMessage
        {
            get
            {
                object obj2 = this.ViewState["ErrorMessage"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["ErrorMessage"] = value;
            }
        }
        [DefaultValue(typeof(Unit), "30px")]
        public override Unit Height
        {
            get
            {
                return base.Height;
            }
            set
            {
                base.Height = value;
            }
        }
        [DefaultValue(typeof(Unit), "100px")]
        public override Unit Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                base.Width = value;
            }
        }
        #endregion
        #region Constructor y otros
        public Captcha()
        {
            this.Font.Names = new string[] { "arial", "arial black", "comic sans ms", "courier new", "estrangelo edessa", "franklin gothic medium", "georgia", "lucida console", "lucida sans unicode", "mangal", "microsoft sans serif", "palatino linotype", "sylfaen", "tahoma", "times new roman", "trebuchet ms", "verdana" };
            this.BackColor = Color.White;
            this.Height = Unit.Parse("30px");
            this.Width = Unit.Parse("100px");
        }
        private string GetChars(TipoCaracteres tipoCaracteres)
        {
            string salida = "";
            switch (UpperChars)
            {
                case UpperChars.Mayusculas:
                    switch (tipoCaracteres)
                    {
                        case TipoCaracteres.Letras: salida = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; break;
                        case TipoCaracteres.Numeros: salida = "1234567890"; break;
                        case TipoCaracteres.LetrasYNumeros: salida = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"; break;
                    }
                    break;
                case UpperChars.Minusculas:
                    switch (tipoCaracteres)
                    {
                        case TipoCaracteres.Letras: salida = "abcdefghijklmnopqrstuvwxyz"; break;
                        case TipoCaracteres.Numeros: salida = "1234567890"; break;
                        case TipoCaracteres.LetrasYNumeros: salida = "abcdefghijklmnopqrstuvwxyz1234567890"; break;
                    }
                    break;
                case UpperChars.MayusculasYMinusculas:
                    switch (tipoCaracteres)
                    {
                        case TipoCaracteres.Letras: salida = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"; break;
                        case TipoCaracteres.Numeros: salida = "1234567890"; break;
                        case TipoCaracteres.LetrasYNumeros: salida = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890"; break;
                    }
                    break;
            }
            return salida;
        }
        #endregion
        #region Metodos de inicializacion y renderizado del control
        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            errorMessage = new Label();
            errorMessage.ForeColor = Color.Red;
            this.Controls.Add(errorMessage);
        }
        protected override void OnInit(EventArgs e)
        {
            if (!this.DesignMode)
            {
                System.Web.Configuration.HttpHandlersSection httpHandlersSection = (System.Web.Configuration.HttpHandlersSection)System.Web.Configuration.WebConfigurationManager.GetSection("system.web/httpHandlers");

                int i = 0;
                bool yaEsta = false;
                while (!yaEsta && i < httpHandlersSection.Handlers.Count)
                {
                    if (httpHandlersSection.Handlers[i].Path.ToLower().Contains("captchaimage.aspx"))
                    {
                        yaEsta = true;
                    }
                    i++;
                }
                if (!yaEsta)
                {
                    throw new Exception("No se pudo encontrar el tag <add path=\"CaptchaImage.aspx\" verb=\"GET\" type=\"WCL.CaptchaImageHandler, WCL\" />\" en la sección httpHandler del Web.Config.");
                }
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            this.GenerateNewCaptcha();
            base.OnPreRender(e);
        }
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.DesignMode)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "#89a4e7");
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderColor, "#7792b5");
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "solid");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "3px 7px 3px 7px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.FontFamily, "Arial,Helvetica,sans-serif");
                writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "11px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "#ffffff");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write("<b>" + GetType().Name + "</b>" + " - " + this.ID);
                writer.RenderEndTag();
            }
            if (!this.DesignMode)
            {
            this.AddAttributesToRender(writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
           
                if (this.CacheStrategy == CaptchaCacheType.Session)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, "CaptchaImage.aspx?guid=" + Convert.ToString(this.CaptchaImage.UniqueId) + "&s=1");
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, "CaptchaImage.aspx?guid=" + Convert.ToString(this.CaptchaImage.UniqueId));
                }
            
            writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.ToolTip);
            writer.AddAttribute(HtmlTextWriterAttribute.Width, this.Width.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Height, this.Height.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();
            this.errorMessage.Text = this.ErrorMessage;
            this.errorMessage.RenderControl(writer);
            writer.RenderEndTag();
            }
        }
        #endregion
        #region Metodos del captcha
        private void GenerateNewCaptcha()
        {
            if (!this.DesignMode)
            {
                this.CaptchaImage = new CaptchaImage();
                this.CaptchaImage.BackColor = this.BackColor;
                this.CaptchaImage.BackgroundNoise = this.BackgroundNoiseLevel;
                this.CaptchaImage.FontColor = this.ForeColor;
                this.CaptchaImage.FontWarp = this.FontWarpFactor;
                this.CaptchaImage.FontWhitelist = this.Font.Names;
                this.CaptchaImage.Height = Convert.ToInt32(this.Height.Value);
                this.CaptchaImage.LineColor = this.LineColor;
                this.CaptchaImage.LineNoise = this.LineNoiseLevel;
                this.CaptchaImage.NoiseColor = this.NoiseColor;
                this.CaptchaImage.TextLength = this.MaxLength;
                this.CaptchaImage.Width = Convert.ToInt32(this.Width.Value);
                this.CaptchaImage.TextChars = this.GetChars(this.TypeChars);

                if (this.CacheStrategy == CaptchaCacheType.Cache)
                {
                    HttpRuntime.Cache.Add(this.CaptchaImage.UniqueId, this.CaptchaImage, null, DateTime.Now.AddSeconds(Convert.ToDouble((this.TimeoutSecondsMax == 0) ? 90 : this.TimeoutSecondsMax)), TimeSpan.Zero, CacheItemPriority.NotRemovable, null);
                }
                else
                {
                    HttpContext.Current.Session.Add(this.CaptchaImage.UniqueId, this.CaptchaImage);
                }
                this.PrevGuid = this.CaptchaImage.UniqueId;
            }
        }
        private CaptchaImage GetCachedCaptcha(string guid)
        {
            if (this.CacheStrategy == CaptchaCacheType.Cache)
            {
                return (CaptchaImage)HttpRuntime.Cache.Get(guid);
            }
            return (CaptchaImage)HttpContext.Current.Session[guid];
        }
        /// <summary>
        /// Metodo que valida el control.
        /// </summary>
        /// <returns>Retorno true si el texto ingresado coinside con el de la imagen.</returns>
        public bool ValidateCaptcha()
        {
            bool salida = false;
            CaptchaImage cachedCaptcha = this.GetCachedCaptcha(this.PrevGuid);
            if (cachedCaptcha == null)
            {
                this.ErrorMessage = "El código que ha escrito está terminado después de  " + this.TimeoutSecondsMax.ToString() + " segundos.";
            }
            else if ((this.TimeoutSecondsMin > 0) && (cachedCaptcha.RenderedAt.AddSeconds((double)this.TimeoutSecondsMin) > DateTime.Now))
            {
                this.ErrorMessage = "Código se ha escrito demasiado rápido. Espere por lo menos  " + this.TimeoutSecondsMin.ToString() + " segundos.";
            }
            else if (string.Compare(((TextBox)this.Parent.FindControl(ControlToValidate)).Text, cachedCaptcha.Text, true) != 0)
            {
                this.ErrorMessage = "El código que ha escrito no coincide con el código en la imagen.";
                this.RemoveCachedCaptcha(this.PrevGuid);
            }
            else
            {
                this.ErrorMessage = "";
                salida = true;
                this.RemoveCachedCaptcha(this.PrevGuid);
            }
            return salida;
        }
        private void RemoveCachedCaptcha(string guid)
        {
            if (this.CacheStrategy == CaptchaCacheType.Cache)
            {
                HttpRuntime.Cache.Remove(guid);
            }
            else
            {
                HttpContext.Current.Session.Remove(guid);
            }
        }
        #endregion
    }
    public enum CaptchaCacheType
    {
        None = 0,
        Cache = 1,
        Session = 2
    }
    public enum TipoCaracteres
    {
        Numeros=1,
        Letras=2,
        LetrasYNumeros =3
    }
    public enum UpperChars
    {
        Mayusculas = 1,
        Minusculas = 2,
        MayusculasYMinusculas = 3
    }
    public class CaptchaControlCoverter : ControlIDConverter
    {
        protected override bool FilterControl(Control control)
        {
            return control.GetType() == typeof(TextBox);
        }
    }
}

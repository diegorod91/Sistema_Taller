using System.ComponentModel;

namespace System.Web.UI.WebControls
{
    [DefaultProperty("Text"), ToolboxData("<{0}:ReCaptcha runat=server SiteKey= SecretKey= ErrorMessage= - Demuestra que no eres un robot.></{0}:ReCaptcha>")]
    public class ReCaptcha : WebControl
    {
        private HiddenField hf;
        [Category("Behavior"), Themeable(false), DefaultValue("")]
        public string SiteKey
        {
            get
            {
                return (ViewState["SiteKey"] == null) ? string.Empty : (string)ViewState["SiteKey"];
            }
            set
            {
                ViewState["SiteKey"] = value;
            }
        }
        [Category("Behavior"), Themeable(false), DefaultValue("")]
        public string SecretKey
        {
            get
            {
                return (ViewState["SecretKey"] == null) ? string.Empty : (string)ViewState["SecretKey"];
            }
            set
            {
                ViewState["SecretKey"] = value;
            }
        }
        [Browsable(false)]
        public bool IsValid
        {
            get
            {
                return (ViewState["IsValid"] == null) ? false : (bool)ViewState["IsValid"];
            }
            set
            {
                ViewState["IsValid"] = value;
            }
        }
        [Category("Behavior"), Themeable(false)]
        public string ErrorMessage
        {
            get
            {

                return (ViewState["ErrorMessage"] == null) ? string.Empty : (string)ViewState["ErrorMessage"];
            }
            set
            {
                ViewState["ErrorMessage"] = value;
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            if (!this.DesignMode)
            {
                if (string.IsNullOrEmpty(SiteKey))
                {
                    throw new ArgumentNullException("Debe asignar un valor a la propiedad SiteKey.");
                }
                if (string.IsNullOrEmpty(SecretKey))
                {
                    throw new ArgumentNullException("Debe asignar un valor a la propiedad SecretKey.");
                }
            }
            base.OnPreRender(e);
        }
        protected override void OnInit(EventArgs e)
        {
            if (!this.DesignMode)
            {
                if (String.IsNullOrEmpty(base.Font.Name))
                { base.Font.Name = "Trebuchet MS"; }

                if (base.Font.Size.Equals(FontSize.NotSet))
                { base.Font.Size = new FontUnit(11.0, UnitType.Pixel); }
            }
            hf = new HiddenField();
            hf.ID = "HF_" + this.ID;
            Controls.Add(hf);
        }
        protected override void RenderContents(HtmlTextWriter output)
        {
            if (this.DesignMode)
            {
                output.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "#89a4e7");
                output.AddStyleAttribute(HtmlTextWriterStyle.BorderColor, "#7792b5");
                output.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "solid");
                output.AddStyleAttribute(HtmlTextWriterStyle.Padding, "3px 7px 3px 7px");
                output.AddStyleAttribute(HtmlTextWriterStyle.FontFamily, "Arial,Helvetica,sans-serif");
                output.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "11px");
                output.AddStyleAttribute(HtmlTextWriterStyle.Color, "#ffffff");
                output.RenderBeginTag(HtmlTextWriterTag.Span);
                output.Write("<b>" + GetType().Name + "</b>" + " - " + this.ID);
                output.RenderEndTag();
            }

            if (!this.DesignMode)
            {

                output.AddAttribute("id", "divError" + this.ID);
                output.AddStyleAttribute(HtmlTextWriterStyle.Width, "350px");
                output.RenderBeginTag(HtmlTextWriterTag.Div);
                output.AddAttribute("id", "div" + this.ID);
                output.RenderBeginTag(HtmlTextWriterTag.Div);
                output.RenderEndTag();
                output.AddAttribute("id", "divErrorMsg" + this.ID);
                output.AddAttribute("class", "recaptcha-error-message");
                output.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
                output.AddStyleAttribute(HtmlTextWriterStyle.Color, "#dd4b39");
                output.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "12px");
                output.AddStyleAttribute(HtmlTextWriterStyle.Padding, "4px 0");
                output.RenderBeginTag(HtmlTextWriterTag.Div);
                output.Write(this.ErrorMessage);
                output.RenderEndTag();
                output.RenderEndTag();
                string style = "<style type=\"text/css\"> " +
                               ".recaptcha-error { border: 1px solid #dd4b39;  padding: 5px; } " +
                               //".recaptcha-error-message { color:; font-size: 12px; padding: 4px 0; }" +
                               "</style>";
                output.Write(style);
                string script = "<script type=\"text/javascript\">" +

                                    "var " + this.ID + "; " +
                                    "if (!" + this.ID + ") " +
                                    "{ " +
                                    "    " + this.ID + " = { }; " +
                                    "} " +
                                    "var " + this.ID + "load = function() { " +
                                            "grecaptcha.render('div" + this.ID + "', { " +
                                                "'sitekey': '" + this.SiteKey + "'," +
                                                "'callback': " + this.ID + ".verifyCallback  " +
                                            "}); " +
                                    "}; " +
                                    this.ID + ".verifyCallback = function(response) { " +
                                       "$('#" + hf.ClientID + "').val(response); " +
                                       "$('#divError" + this.ID + "').removeClass(\"recaptcha-error\"); " +
                                       "$('#divErrorMsg" + this.ID + "').hide(); " +
                                    "}; " +
                                    this.ID + ".reload = function() { " +
                                        "if (typeof grecaptcha !== 'undefined') { " +
                                            "grecaptcha.render('div" + this.ID + "', { " +
                                                "'sitekey': '" + this.SiteKey + "'," +
                                                "'callback': " + this.ID + ".verifyCallback  " +
                                            "}); " +
                                        "} " +
                                    "}; " +
                                    this.ID + ".validate =  function() { " +
                                    "if ($('#" + hf.ClientID + "').val().length <= 1) { " +
                                    "$('#divError" + this.ID + "').addClass('recaptcha-error'); " +
                                    "$('#divErrorMsg" + this.ID + "').show(); " +
                                    "return \"" + this.ErrorMessage + "\" } return \"\"; };" +
                            "</script>" +
                            "<script src = \"https://www.google.com/recaptcha/api.js?onload=" + this.ID + "load&render=explicit&hl=es-419\" async defer></script>";
                output.Write(script);
                JavascriptProxy.RegisterStartupScript(this, GetType(), "ReCaptcha.reload", this.ID + ".reload();", true);
                base.RenderContents(output);
            }
        }
        public void Validate()
        {
            var response = hf.Value;
            var client = new System.Net.WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", SecretKey, response));
            var obj = Newtonsoft.Json.Linq.JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            this.IsValid = status;
        }
    }
}
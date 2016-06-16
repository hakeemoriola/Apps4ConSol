using System;
using System.ComponentModel;
using System.Web;

namespace ConSol.Data
{
    [System.ComponentModel.Designer(typeof(CustomControlDesigner))]
    public class DoClickCalendarScriptSetup : System.Web.UI.WebControls.WebControl
    {
        private bool renderClientScript = false;
        private bool EnableClientScript = false;

        public DoClickCalendarScriptSetup()
        {
            this.SupportDir = "~/Support/JsCalendar";
        }

        [Bindable(true), Category("Behavior"), DefaultValue(CalendarLanguage.en)]
        public CalendarLanguage Language
        {
            get { return (ViewState["Language"] != null ? (CalendarLanguage)ViewState["Language"] : CalendarLanguage.en); }
            set { ViewState["Language"] = value; }
        }

        [Bindable(true), Category("Behavior"), DefaultValue("~/Support/JsCalendar")]
        public string SupportDir
        {
            get { return (ViewState["SupportDir"] != null ? (string)ViewState["SupportDir"] : ""); }
            set { ViewState["SupportDir"] = value; }
        }

        public enum CalendarLanguage
        {
            af,
            br,
            ca,
            da,
            de,
            du,
            el,
            en,
            es,
            fi,
            fr,
            hr,
            hu,
            it,
            jp,
            ko,
            lt,
            nl,
            no,
            pl,
            pt,
            ro,
            ru,
            sl,
            si,
            sk,
            sp,
            sv,
            tr,
            zh
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Type type = this.GetType();
            string calendarScripts = "";
            calendarScripts += GetClientScriptInclude("calendar.js");
            calendarScripts += GetClientScriptInclude("calendar-setup.js");
            string languageFile = String.Format("lang/calendar-{0}.js", this.Language.ToString());
            calendarScripts += GetClientScriptInclude(languageFile);
            //Page.RegisterClientScriptBlock("calendarscripts", calendarScripts);
            DetermineRenderClientScript();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "calendarscripts", calendarScripts, false);
        }

        private string GetClientScriptInclude(string scriptFile)
        {
            return "<script language=\"JavaScript\" src=\"" +
                GetClientFileUrl(scriptFile) + "\"></script>\n";
        }

        private string GetClientFileUrl(string fileName)
        {
            return ResolveUrl(this.SupportDir + "/" + fileName);
        }

        protected virtual void DetermineRenderClientScript()
        {
            // Checks whether the page developer wants your control to turn of its client
            // side functionality regardless of the Ajax capabilities of the requesting
            // browser
            if (!EnableClientScript)
            {
                renderClientScript = false;
                return;
            }

            // The code that inspects the Ajax capabilities of the requesting browser goes
            // here.  HttpBrowserCapabilities browser = Page.Request.Browser;
            HttpBrowserCapabilities browser = Page.Request.Browser;
            if (browser.MajorVersion >= 4)
                renderClientScript = true;
        }
    }
}
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ConSolHWeb.Data
{
    /// <summary>
    /// Summary description for BasePage
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        public new UserControl TemplateControl
        {
            get
            {
                if (this.Controls.Count > 0)
                {
                    if (this.Controls[0] is UserControl)
                    {
                        return (UserControl)this.Controls[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public HtmlGenericControl MessageBox
        {
            get
            {
                if (this.TemplateControl != null)
                {
                    return this.Page.FindControl("message") as HtmlGenericControl;
                }
                else
                {
                    return null;
                }
            }
        }

        protected virtual void ShowError(string errorText)
        {
            if (this.MessageBox != null)
            {
                this.MessageBox.InnerHtml = "An error occured: " + errorText;
                this.MessageBox.Attributes["class"] = "errorbox";
                this.MessageBox.Visible = true;
            }
            else
            {
                // Throw an Exception and hope it will be handled by the global application exception handler.
                throw new Exception(errorText);
            }
        }

        /// <summary>
        /// Try to find the MessageBox control, insert the message and set visibility to true.
        /// </summary>
        /// <param name="message"></param>
        protected virtual void ShowMessage(string message)
        {
            if (this.MessageBox != null)
            {
                string me = "<div class='ui-widget'>" +
                                 " <div class='ui-state-highlight ui-corner-all' style='margin-top: 20px; padding: 0 .7em;'> " +
                                   "   <p><span class='ui-icon ui-icon-info' style='float: left; margin-right: .3em;'></span>" +
                                    "  <strong>" + message + "</strong> Sample ui-state-highlight style.</p>" +
                                "  </div>" +
                             " </div>";
                this.MessageBox.InnerHtml = me;
                //this.MessageBox.Attributes["class"] = "messagebox";
                this.MessageBox.Visible = true;
            }
            // TODO: change the class attribute to make a difference with the error (nice background image?)
        }

        /// <summary>
        /// Show the message of the exception, and the messages of the inner exceptions.
        /// </summary>
        /// <param name="ex"></param>
        protected virtual void ShowException(Exception exception)
        {
            string exceptionMessage = "<p>" + exception.Message + "</p>";
            Exception innerException = exception.InnerException;
            while (innerException != null)
            {
                exceptionMessage += "<p>" + innerException.Message + "</p>";
                innerException = innerException.InnerException;
            }
            ShowError(exceptionMessage);
        }
    }
}
using System;
using System.Web.UI.HtmlControls;

namespace ConSolHWeb.Data
{
    public class BaseControl : System.Web.UI.UserControl
    {
        public string BodyPageTitle { get; set; }

        public new HtmlGenericControl TemplateControl
        {
            get
            {
                if (this.Controls.Count > 0)
                {
                    return this.FindControl("message") as HtmlGenericControl;
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
                    return this.FindControl("message") as HtmlGenericControl;
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
                string me = "<div class=''>" + errorText + "</div>";
                this.MessageBox.InnerHtml = me;
                this.MessageBox.Attributes["class"] = "alert alert-error";
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
                string me = "<div class=''>" + message + "</div>";
                this.MessageBox.InnerHtml = me;
                this.MessageBox.Attributes["class"] = "alert alert-success";
                this.MessageBox.Visible = true;
            }
            // TODO: change the class attribute to make a difference with the error (nice background image?)
        }

        protected virtual void ShowWarningMessage(string message)
        {
            if (this.MessageBox != null)
            {
                string me = "<div class=''><h4>Warning!</h4>" + message + "</div>";
                this.MessageBox.InnerHtml = me;
                this.MessageBox.Attributes["class"] = "alert alert-block";
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
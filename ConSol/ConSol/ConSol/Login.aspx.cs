using ConSolHWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Util.CKE())
            {
                lblError.Text = "This Application has expired contact the Administrator";
                username.Enabled = false;
                password.Enabled = false;
                btnCommand.Enabled = false;
            }
            else
            {
            }
            SynchSetup();
        }

        private void SynchSetup()
        {
            string SQL = @" insert into DataDashBoard(DbId)
                        select id from ConSettings
                        where id not in (select dbId from DataDashBoard)";
            int result = DataService.Provider.ExecuteSQL(SQL);
        }

        protected void btnCommand_Click(object sender, EventArgs e)
        {
            string retUrl = "";
            try
            {
                AuthenticationModule am = new AuthenticationModule();
                if (am != null)
                {
                    int result = am.AuthenticateUser(username.Text, password.Text, remember.Checked);
                    if (result > 0)
                    {
                        if (Request.Params["ru"] != null)
                        {
                            retUrl = Request.QueryString["ru"];
                            Response.Redirect(Util.BaseSiteUrl + "p.aspx?p=" + retUrl, true);
                        }
                        else
                        {
                            Response.Redirect(Util.BaseSiteUrl + "p.aspx?p=home", true);
                        }
                    }
                    else if (result < 0)
                    {
                        this.lblError.Text = "You have not activated you account,  Kindly check your email and and click on the activate this account link.";
                        this.lblError.Visible = true;
                        ViewState["Tries"] = System.Convert.ToInt32(ViewState["Tries"]) + 1;
                        if (System.Convert.ToInt32(ViewState["Tries"]) > 3)
                        {
                            Response.Redirect("~/Denied.aspx?times=" + ViewState["Tries"].ToString(), true);
                        }
                    }
                    else
                    {
                        this.lblError.Text = "Invalid username or password.";
                        this.lblError.Visible = true;
                        // Otherwise, increment number of tries.
                        ViewState["Tries"] = System.Convert.ToInt32(ViewState["Tries"]) + 1;
                        if (System.Convert.ToInt32(ViewState["Tries"]) > 3)
                        {
                            Response.Redirect("Denied.aspx?times=" + ViewState["Tries"].ToString(), true);
                        }
                    }
                }
                else
                {
                    throw new Exception("Modules Not Supported on the Server");
                }
            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.Message;
            }
        }
    }
}
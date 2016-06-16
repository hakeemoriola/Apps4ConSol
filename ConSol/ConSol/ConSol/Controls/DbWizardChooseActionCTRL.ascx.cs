using ConSolHWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class DbWizardChooseActionCTRL : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            if (rblAction.SelectedIndex == -1)
            {
                ShowError("Please Select an action!");
            }
            else
            {
                string action = rblAction.SelectedValue;
                string db = ddlDatabase.SelectedValue;
                if (action == "0")
                {
                    Session["ACTION"] = action;
                    Response.Redirect(Util.BaseSiteUrl + "p.aspx?p=add-new-database", true);
                }
                else if (action == "1")
                {
                    Session["ACTION"] = action;
                    Session["DB_SELECTED"] = db;
                    Response.Redirect(Util.BaseSiteUrl + "p.aspx?p=database-update", true);
                }
                else { }
            }
        }

        protected void rblAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblAction.SelectedValue == "1")
            {
                pnlDatabases.Visible = true;
            }
            else
            {
                pnlDatabases.Visible = false;
                //rblAction.ClearSelection();
            }
        }
    }
}
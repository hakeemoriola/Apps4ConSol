using ConSolHWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class SetUserInRoleCTRL : BaseControl
    {
        private string username = "";
        private string action = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "::  User In Role Maintenance ::";

            // Determine unique action for Update
            if (Request.Params["username"] != null)
            {
                username = Request.Params["username"];
                lblUsername.Text = username;
            }
            if (Request.Params["action"] != null)
            {
                action = Request.Params["action"];
            }
        }

        protected void BtnCommand_Click(object sender, EventArgs e)
        {
            int res = DataService.Provider.SetUserInRole(username, ddlRoleName.SelectedValue);
            Response.Redirect("~/p.aspx?p=users");
        }
    }
}
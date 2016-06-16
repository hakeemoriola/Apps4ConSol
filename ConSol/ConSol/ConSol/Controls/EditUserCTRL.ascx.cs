using ConSolHWeb.Data;
using ConSolHWeb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class EditUserCTRL : BaseControl
    {
        private string username = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = ":: User(s) Maintenance ::";

            // Determine unique action for Update
            if (Request.Params["UserName"] != null)
            {
                username = Request.Params["UserName"].ToString();
            }

            if (Page.IsPostBack == false)
            {
                if (username != "")
                {
                    // Obtain a single row of event information
                    User dr = DataService.Provider.getUserDetailByUsername(username);

                    txtUserName.Text = (string)dr.UserName;

                    txtFirstName.Text = dr.FirstName;
                    txtLastName.Text = dr.LastName;
                    ddlTitle.SelectedValue = (string)dr.Title;
                    chkActive.Checked = Convert.ToBoolean(dr.IsActive);
                    txtUserName.Enabled = false;
                }
            }
        }

        protected void BtnCommand_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (HttpContext.Current.Session["UserRecID"] != null)
            {
                User usr = new User();
                usr.UserName = txtUserName.Text;
                usr.FirstName = txtFirstName.Text;
                usr.LastName = txtLastName.Text;
                usr.Title = ddlTitle.SelectedValue;
                usr.IsActive = Convert.ToInt32(chkActive.Checked);
                usr.Invaildated = 1;
                usr.Activated = 1;

                if (txtPassword.Text == string.Empty)
                {
                    result = DataService.Provider.UpdateUserDetailWithoutPasswordByAdmin(usr);
                }
                else
                {
                    usr.Password = PortalSecurity.Encrypt(txtPassword.Text.Trim());
                    usr.Text_Pass = txtPassword.Text.Trim();
                    result = DataService.Provider.UpdateUserDetailByAdmin(usr);
                }
                if (result > 0)
                {
                    ShowMessage("Information Saved successfully!");
                }
            }
        }
    }
}
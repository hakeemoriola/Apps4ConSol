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
    public partial class AddNewUserCTRL : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnCommand_Click(object sender, EventArgs e)
        {
            User usr = new User();
            usr.UserName = txtUserName.Text;
            usr.FirstName = txtFirstName.Text;
            usr.LastName = txtLastName.Text;
            usr.Title = ddlTitle.SelectedValue;
            usr.Text_Pass = txtPassword.Text;
            usr.Password = PortalSecurity.Encrypt(txtPassword.Text);

            int result = DataService.Provider.AddUserDetail(usr);
            if (result > 0)
            {
                Response.Redirect(Util.BaseSiteUrl + "p.aspx?p=users", true);
            }
        }
    }
}
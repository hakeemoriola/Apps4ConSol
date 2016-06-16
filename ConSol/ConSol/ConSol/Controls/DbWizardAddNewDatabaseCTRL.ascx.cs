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
    public partial class DbWizardAddNewDatabaseCTRL : BaseControl
    {
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();

            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage, true);
        }

        protected void BtnCommand_Click(object sender, EventArgs e)
        {
            ConSetting p = new ConSetting();
            p.ProjectName = txtProjectName.Text;
            p.ServerIP = txtServerIP.Text;
            p.Username = txtUsername.Text;
            p.Password = txtPassword.Text;
            p.Database = txtDatabase.Text;
            p.Port = txtPort.Text;
            p.IsActive = chkActive.Checked;
            int result = DataService.Provider.AddConSetting(p);
            if (result > 0)
            {
                Session["DB_SELECTED"] = result;
                ShowMessage("Database setting added successfully!");
            }
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            if (Session["DB_SELECTED"] != null)
                Response.Redirect(Util.BaseSiteUrl + "p.aspx?p=pick-or-edit-table", true);
            else
                ShowError("Please Fill the database parameters and submit!");
        }
    }
}
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
    public partial class DatabaseUpdateCTRL : BaseControl
    {
        static string prevPage = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
            }
            if (Session["DB_SELECTED"] != null) {
                ConSetting cm = DataService.Provider.GetConSettingById(int.Parse(Session["DB_SELECTED"].ToString()));
                if (cm != null) {
                    lblDbId.Text = cm.Id.ToString();
                    txtDatabase.Text = cm.Database;
                    txtPassword.Text = cm.Password;
                    txtPort.Text = cm.Port;
                    txtProjectName.Text = cm.ProjectName;
                    txtServerIP.Text = cm.ServerIP;
                    txtUsername.Text = cm.Username;
                    chkActive.Checked = false;
                }
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
            p.Id = int.Parse(lblDbId.Text);
            int result = DataService.Provider.UpdateConSetting(p);
            if (result > 0)
            {
                ShowMessage("Database setting updated successfully!");
            }
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            Response.Redirect(Util.BaseSiteUrl + "p.aspx?p=pick-or-edit-table", true);
        }
    }
}
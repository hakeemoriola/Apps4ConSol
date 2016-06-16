using ConSolHWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class ModulesInRoleSelection : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = ":: Select Modules ::";
            ModuleList.DataSource = ConSolHWeb.Data.DataService.Provider.getAllModules();
            ModuleList.DataBind();
            lblRoleName.Text = (Session["pickRoleName"] != null) ? Session["pickRoleName"].ToString() : string.Empty;
            Util.getModulesinRole(lblRoleName.Text, ModuleList);
            //if (ModuleList.Items.Count > 0)
            //buildControl();
        }

        protected void BtnCommand_Click(object sender, EventArgs e)
        {
            string modulesselected = "";
            int count = 0;
            for (int i = 0; i < ModuleList.Items.Count; i++)
            {
                CheckBox SelectTime = ((CheckBox)ModuleList.Items[i].FindControl("chkSelected"));
                if (SelectTime.Checked == true)
                {
                    count++;
                    Label lblModuleID = ((Label)ModuleList.Items[i].FindControl("lblModuleID"));
                    modulesselected += lblModuleID.Text + ",";
                }
                else
                {
                }
            }
            if (count == 0)
            {
                ShowWarningMessage(" Please select at least a TMS_SelfModule for this role!");
            }
            else
            {
                DataService.Provider.AddModulesInRole(lblRoleName.Text, modulesselected.Remove(modulesselected.Length - 1, 1));
                ShowMessage("Modules Saved Successfully for Role!");
                //buildControl();
            }
        }

        protected void chkAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ModuleList.Items.Count; i++)
            {
                CheckBox SelectTime = ((CheckBox)ModuleList.Items[i].FindControl("chkSelected"));
                if (SelectTime.Checked == true)
                { }
                else
                {
                    SelectTime.Checked = true;
                }
            }
        }

        protected void chkuncheckall_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ModuleList.Items.Count; i++)
            {
                CheckBox SelectTime = ((CheckBox)ModuleList.Items[i].FindControl("chkSelected"));
                if (SelectTime.Checked == true)
                { SelectTime.Checked = false; }
                else
                {
                    SelectTime.Checked = false;
                }
            }
        }

        private void buildControl()
        {
            plcModulePriviledges.Controls.Clear();
            HyperLink hplnk = new HyperLink();
            hplnk.Text = "Set Modules Privileges for this Role";
            hplnk.NavigateUrl = "~/p.aspx?p=modules-priviledges";
            hplnk.Visible = (Session["pickRoleName"].ToString() == "System Administrator") ? false : true;
            plcModulePriviledges.Controls.Add(hplnk);
        }
    }
}
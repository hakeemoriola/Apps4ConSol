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
    public partial class RoleControl : BaseControl
    {
        private string userrolename = "";
        private string action = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "::  UserRole Maintenance ::";

            // Determine unique action for Update
            if (Request.Params["userrolename"] != null)
            {
                userrolename = Request.Params["userrolename"];
            }
            if (Request.Params["action"] != null)
            {
                action = Request.Params["action"];
                if (int.Parse(action) == 1)
                {
                    BtnCommand.Text = "Update UserRole";
                    txtUserRoleName.Enabled = false;
                }
            }

            if (Page.IsPostBack == false)
            {
                if (userrolename != "")
                {
                    // Obtain a single row of event information
                    UserRole dr = ConSolHWeb.Data.DataService.Provider.getUserRoleByUserRoleName(userrolename);

                    txtUserRoleName.Text = dr.UserRoleName;
                    txtRoleDescription.Text = dr.RoleDescription;
                    buildControl();
                }
            }
        }

        protected void BtnCommand_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.Params["userrolename"] != null)
                {
                    userrolename = Request.Params["userrolename"];
                }
                if (Request.Params["action"] != null)
                {
                    action = Request.Params["action"];
                }
                if (int.Parse(action) == 0)
                {
                    UserRole supplier = new UserRole();
                    supplier.UserRoleName = (txtUserRoleName.Text == string.Empty) ? string.Empty : txtUserRoleName.Text;
                    supplier.RoleDescription = (txtRoleDescription.Text == string.Empty) ? string.Empty : txtRoleDescription.Text;
                    DataService.Provider.AddUserRole(supplier);
                    ShowMessage("UserRole Saved Successfully!");
                    buildControl();
                }
                else if (int.Parse(action) == 1)
                {
                    UserRole supplier = new UserRole();
                    supplier.UserRoleName = (txtUserRoleName.Text == string.Empty) ? string.Empty : txtUserRoleName.Text;
                    supplier.RoleDescription = (txtRoleDescription.Text == string.Empty) ? string.Empty : txtRoleDescription.Text;
                    ConSolHWeb.Data.DataService.Provider.UpdateUserRole(supplier);
                    ShowMessage("UserRole Updated Successfully!");
                    buildControl();
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void buildControl()
        {
            HyperLink hplnk = new HyperLink();
            hplnk.Text = "Set Modules Access for this Role";
            hplnk.NavigateUrl = "~/p.aspx?p=modules-in-role";
            hplnk.Visible = (userrolename == "Administrator") ? false : true;
            Session["pickRoleName"] = userrolename;
            plcModuleinRole.Controls.Add(hplnk);
        }
    }
}

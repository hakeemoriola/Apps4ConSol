using ConSolHWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class RolesGrid : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = ":: User Role Maintenance ::";
            BindList();
        }

        protected void UserRoleList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UserRoleList.PageIndex = e.NewPageIndex;
            BindList();
        }

        private void BindList()
        {
            UserRoleList.DataSource = ConSolHWeb.Data.DataService.Provider.getAllUserRoles();
            UserRoleList.DataBind();
        }
    }
}
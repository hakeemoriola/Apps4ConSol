using ConSolHWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class UsersGrid : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = ":: User(s) Maintenance ::";
            BindList();
        }

        protected void UsersList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UsersList.PageIndex = e.NewPageIndex;
            BindList();
        }

        private void BindList()
        {
            UsersList.DataSource = ConSolHWeb.Data.DataService.Provider.getAllUserDetails();
            UsersList.DataBind();
        }        
    }
}
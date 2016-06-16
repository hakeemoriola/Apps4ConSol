using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class ModuleGrid : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = ":: Module(s) ::";
            BindGrid();
        }

        public void VesselsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            VesselsList.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        private void BindGrid()
        {
            VesselsList.DataSource = ConSolHWeb.Data.DataService.Provider.getAllModules();
            VesselsList.DataBind();
        }
    }
}
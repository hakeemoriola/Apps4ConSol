using ConSolHWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class DbWizardFinishCTRL : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            Response.Redirect(Util.BaseSiteUrl + "p.aspx?p=home", true);
        }
    }
}
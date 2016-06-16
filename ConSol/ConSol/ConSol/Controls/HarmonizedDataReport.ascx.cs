using ConSolHWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class HarmonizedDataReport : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnRefresh_Click(object sender, EventArgs e)
        {
            //select count(*) from [VxTelephones]
            Label3.Text = DataService.Provider.GetHarmonisedCount();
        }
    }
}
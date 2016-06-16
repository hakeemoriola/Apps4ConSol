using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated) { Response.Redirect("~/Login.aspx"); }
            else
            {
                if (Session["loginUserId"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                }
            }
        }
    }
}
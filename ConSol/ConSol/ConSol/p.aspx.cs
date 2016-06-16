using ConSolHWeb.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol
{
    public partial class p : System.Web.UI.Page
    {
        private string job = "";
        private Hashtable ht = new Hashtable();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ht = DataService.Provider.getListControls();
                Literal Literal1, Literal2;

                if (Request.Params["p"] != null)
                {
                    job = Request.Params["p"].ToString();
                    cort.Controls.Add(Page.LoadControl(((ConSolHWeb.Data.Models.Page)ht[job]).ControlPath));
                    // Gets a reference to a Label control that not in a ContentPlaceHolder
                    Literal1 = (Literal)Master.FindControl("Literal1");
                    Literal2 = (Literal)Master.FindControl("Literal2");
                    if (Literal1 != null)
                    {
                        Literal1.Text = ((ConSolHWeb.Data.Models.Page)ht[job]).meta_title;
                    }
                    if (Literal2 != null)
                    {
                        Literal2.Text = @"<meta name='" + ((ConSolHWeb.Data.Models.Page)ht[job]).meta_title + "' content='" + ((ConSolHWeb.Data.Models.Page)ht[job]).meta_keywords + @"' />
                                  <meta name='author' content='Oriola Enterprises Nigeria Limited' />
                                ";
                    }
                }
                else
                {
                    job = "home";
                    Response.Redirect("~/p.aspx?p=home", true);
                }
            }
            catch (Exception ex)
            {
                msgBox1.alert(ex.Message);
            }
        }
    }
}
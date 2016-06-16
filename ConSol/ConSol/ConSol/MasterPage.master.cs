using ConSolHWeb.Data;
using ConSolHWeb.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
             * // Define the name and type of the client scripts on the page.
            String csname2 = "ButtonClickScript";
            Type cstype = this.GetType();

            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager cs = Page.ClientScript;

            // Check to see if the client script is already registered.
            if (!cs.IsClientScriptBlockRegistered(cstype, csname2))
            {
                StringBuilder cstext2 = new StringBuilder();
                cstext2.Append("<script type=\"text/javascript\"> function openReportViewer(rptUrl) { ");
                cstext2.Append("var windowWidth = window.innerWidth;");
                cstext2.Append("var windowHeight = window.outerHeight;");
                cstext2.Append("var href = rptUrl;");
                cstext2.Append("$('#dialog').dialog({");
                cstext2.Append("    width: windowWidth - 50,");
                cstext2.Append("    autoOpen: false,");
                cstext2.Append("    dialogClass: \"test\",");
                cstext2.Append("    modal: false,");
                cstext2.Append("    height: windowHeight,");
                cstext2.Append("    resizable: true,");
                cstext2.Append("    responsive: true");
                cstext2.Append("});");

                cstext2.Append("$(\"#opener\").on(\"click\", function () {");
                cstext2.Append("    $(\"#dialog\").empty()");
                cstext2.Append("    $(\"#dialog\").append('<iframe width=\"100%\" height=\"100%\" src=\"' + href + '\"></iframe>').dialog(\"open\");");
                cstext2.Append("});");
                cstext2.Append("} </");
                cstext2.Append("script>");
                cs.RegisterClientScriptBlock(cstype, csname2, cstext2.ToString(), false);
            }
             */

            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (Session["UserName"] != null)
                {
                    User selcart = DataService.Provider.getUserDetailByUsername(Session["UserName"].ToString());
                    if (selcart != null)
                    {
                        LastName.Text = selcart.LastName;
                        OtherNames.Text = selcart.FirstName + " " + selcart.MiddleName;
                        rolename.Text = Session["UserRole"].ToString();
                    }
                    GetModulesLinks();
                }
                else { Response.Redirect("~/Login.aspx"); }
            }
        }

        private void GetModulesLinks()
        {
            Hashtable ht = new Hashtable();
            List<ModulesAction> moduleactions = new List<ModulesAction>();
            List<Module> modules = new List<Module>();
            if (Session["UserRole"] != null)
            {
                if (Session["UserRole"].ToString() == "System Administrator")
                { modules = DataService.Provider.getAllModules().OrderBy(c => c.M_Order).ToList(); }
                else
                {
                    modules = DataService.Provider.getAllModulesByRole(Session["UserRole"].ToString());
                    moduleactions = DataService.Provider.getAllModulesActionPermmitedByRole(Session["UserRole"].ToString());
                    ht = Util.GroupActions(moduleactions);
                }
                if (moduleactions != null)
                {
                    plhToolbar.Controls.Clear();
                    string upper = @"<div class='toolbar'>
                                        <div class='toolbarLeft'>
                                        </div>
                                        <div class='toolbarContent'>";
                    string lower = @"       </div>
                                        <div class='toolbarRight'>
                                        </div>
                                        <div class='clear'>
                                        </div>
                                </div>";
                    int met = 45;
                    foreach (string item in ht.Keys)
                    {
                        StringBuilder middle = new StringBuilder();
                        List<ModulesAction> actiongroup = (List<ModulesAction>)ht[item];
                        foreach (ModulesAction em in actiongroup)
                        {
                            middle.Append(@"<a href='" + Util.BaseSiteUrl + "p.aspx?p=" + em.ModuleActionUrl.ToLower() + "'><img src='images/" + (met++) + ".png' alt='' />" + em.ModuleActionName + "</a> |");
                        }
                        plhToolbar.Controls.Add(new LiteralControl(upper + middle.ToString() + lower));
                        middle = null;
                    }
                }

                if (modules != null)
                {
                    //modules.Sort();
                    plhModuleLinks.Controls.Clear();
                    foreach (Module item in modules)
                    {
                        plhModuleLinks.Controls.Add(new LiteralControl(@"<a href='" + Util.BaseSiteUrl + "p.aspx?p=" + item.ModuleID.ToLower() + "'><img src='images/" + item.ModuleImage + "' alt='' />" + item.ModuleName + "</a>"));
                    }
                }
            }
        }
    }
}
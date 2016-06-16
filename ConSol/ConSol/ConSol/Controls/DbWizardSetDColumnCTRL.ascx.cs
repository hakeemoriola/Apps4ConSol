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
    public partial class DbWizardSetDColumnCTRL : BaseControl
    {
        static string prevPage = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
            }
            if (Session["DB_SELECTED"] != null)
            {
                int dbId = int.Parse(Session["DB_SELECTED"].ToString());
                List<MatchMetaColumn> list =  DataService.Provider.GetMatchMetaColumnsByDbId(int.Parse(Session["DB_SELECTED"].ToString()));
                if (list != null) {
                    RadioButtonList1.DataSource = list;
                    RadioButtonList1.DataBind();

                    getSelected(dbId);
                }
            }
        }

        private void getSelected(int DBId)
        {
            MatchMetaColumn mt = DataService.Provider.GetMatchMetaColumnPicked(DBId);
            if (mt != null)
            {
                foreach (ListItem item in RadioButtonList1.Items)
                {
                    if (item.Value == mt.BaseDbColumn)
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            if (Session["DB_SELECTED"] != null)
            {
                int dbId = int.Parse(Session["DB_SELECTED"].ToString());
                MatchMetaColumn dmt = DataService.Provider.GetMatchMetaColumnPicked(dbId);
                if (dmt != null && dmt.IsDColumn)
                {
                    Response.Redirect(Util.BaseSiteUrl + "p.aspx?p=dm-finish", true);
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (Session["DB_SELECTED"] != null)
            {
                if (RadioButtonList1.SelectedIndex != -1)
                {
                    string columnName = RadioButtonList1.SelectedValue;
                    int DbId = int.Parse(Session["DB_SELECTED"].ToString());
                    int result = DataService.Provider.SetIsDColumn(DbId,columnName);
                    if (result > 0) { ShowMessage("Table saved successfully!"); }
                }
                else
                {
                    ShowError("Please pick a unique column!");
                }
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage, true);
        }
    }
}
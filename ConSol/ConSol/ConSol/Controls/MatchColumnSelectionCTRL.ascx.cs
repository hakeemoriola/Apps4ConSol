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
    public partial class MatchColumnSelectionCTRL : System.Web.UI.UserControl
    {      
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();

                if (Session["DB_SELECTED"] != null)
                {
                    int dbId = int.Parse(Session["DB_SELECTED"].ToString());

                    DBMetaTable curo = DataService.Provider.GetDBMetaTableByDbId(dbId);
                    if (curo != null)
                    {
                        Session["TABLE_SELECTED"] = curo.Id;
                        GridView1.DataSource = DataService.Provider.GetMatchMetaColumnsByDbId(int.Parse(Session["DB_SELECTED"].ToString()));
                        GridView1.DataBind();

                        //getDBSelection(dbId)
                    }
                }
            }
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlBaseDBColumn = e.Row.FindControl("ddlBaseDBColumn") as DropDownList;

                MatchMetaColumn data = (MatchMetaColumn)e.Row.DataItem;
                if (data != null)
                {
                    ddlBaseDBColumn.DataSource = DataService.Provider.GetAllBaseDbColumns();
                    ddlBaseDBColumn.DataBind();

                    if (!string.IsNullOrEmpty(data.BaseDbColumn))
                    {
                        ddlBaseDBColumn.SelectedValue = data.BaseDbColumn;
                    }
                    else { }
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            List<MatchMetaColumn> list = new List<MatchMetaColumn>();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                MatchMetaColumn curr = new MatchMetaColumn();
                Label lblDbId = (Label)GridView1.Rows[i].FindControl("lblDbId");
                Label lblTableId = (Label)GridView1.Rows[i].FindControl("lblTableId");
                Label lblDBcolumn = (Label)GridView1.Rows[i].FindControl("lblDBcolumn");
                DropDownList ddlBaseDBColumn = (DropDownList)GridView1.Rows[i].FindControl("ddlBaseDBColumn");
                if (lblDBcolumn != null)
                {
                    curr.DBColumn = lblDBcolumn.Text;
                }
                if (lblDbId != null)
                {
                    curr.DbId = int.Parse(lblDbId.Text.ToString());
                }
                if (lblTableId != null)
                {
                    curr.TableId = int.Parse(lblTableId.Text.ToString());
                }
                if (ddlBaseDBColumn != null)
                {
                    curr.BaseDbColumn = ddlBaseDBColumn.SelectedValue;
                }
                list.Add(curr);
            }
            int result = DataService.Provider.SaveMatchMetaColumn(list);
            if (result > 0) { }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage, true);
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            if (Session["DB_SELECTED"] != null)
            {
                int dbId = int.Parse(Session["DB_SELECTED"].ToString());
                List<MatchMetaColumn> dmt = DataService.Provider.GetMatchMetaColumnsByDbId(dbId);
                if (dmt != null && !string.IsNullOrEmpty(dmt[0].BaseDbColumn))
                {
                    Response.Redirect(Util.BaseSiteUrl + "p.aspx?p=dm-set-d-column", true);
                }
            }
        }
    }
}
using ConSolHWeb.Data;
using ConSolHWeb.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class DbWizardColumnSelectionCTRL : BaseControl
    {
        private static string prevPage = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
            }
            if (Session["DB_SELECTED"] != null)
            {
                int dbId = int.Parse(Session["DB_SELECTED"].ToString());
                DBMetaTable curo = DataService.Provider.GetDBMetaTableByDbId(dbId);
                if (curo != null)
                {
                    GetTop10Datarows(curo.TableName);
                    List<Stuff> list = DataService.Provider.GetJustDbColumns(curo.TableName, dbId);
                    if (list != null)
                    {
                        chkColumns.DataSource = list;
                        chkColumns.DataBind();
                    }
                }
            }

            getSelectedColumns();
        }

        private void getSelectedColumns()
        {
            if (Session["DB_SELECTED"] != null)
            {
                int dbId = int.Parse(Session["DB_SELECTED"].ToString());
                List<DBMetaColumn> curo = DataService.Provider.GetDBMetaColumnByDbId(dbId);
                if (curo != null)
                {
                    foreach (ListItem item in chkColumns.Items)
                    {
                        foreach (var jt in curo)
                        {
                            if (item.Value == jt.ColumnName)
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
            }
        }

        private void GetTop10Datarows(string tablename)
        {
            DataSet tbl = new DataSet();
            string SQL = "select TOP 10 * from " + tablename;
            if (Session["DB_SELECTED"] != null)
            {
                int dbId = int.Parse(Session["DB_SELECTED"].ToString());
                ConSetting gb = DataService.Provider.GetConSettingById(dbId);
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3};", gb.ServerIP, gb.Database.Remove(gb.Database.Length - 2, 2), gb.Username, gb.Password);
                    con.Open();
                    SqlDataAdapter r = new SqlDataAdapter(SQL, con);
                    r.Fill(tbl, "newTable");
                    // con.Close();
                }
            }
            GridView1.DataSource = tbl;
            GridView1.DataBind();
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            Response.Redirect(Util.BaseSiteUrl + "p.aspx?p=dm-match-column-selection", true);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (Session["DB_SELECTED"] != null)
            {
                int dbId = int.Parse(Session["DB_SELECTED"].ToString());

                List<DBMetaColumn> list = new List<DBMetaColumn>();
                string selected = "";
                if (chkColumns.SelectedIndex != -1)
                {
                    foreach (ListItem item in chkColumns.Items)
                    {
                        if (item.Selected)
                        {
                            selected += item.Value + ",";
                        }
                    }
                }
                else
                {
                    ShowError("Please select a column!");
                    return;
                }
                DBMetaTable tb = DataService.Provider.GetDBMetaTableByDbId(dbId);
                if (tb != null)
                {
                    DBMetaColumn curr = null;
                    string finalText = selected.Remove(selected.Length - 1, 1);
                    string[] fArr = finalText.Split(',');
                    foreach (string item in fArr)
                    {
                        curr = new DBMetaColumn();
                        curr.ColumnName = item;
                        curr.DbId = dbId;
                        curr.TableId = tb.Id;
                        list.Add(curr);
                    }
                }
                int result = DataService.Provider.SaveDBMetaColumn(list, dbId);
                if (result > 0)
                {
                    ShowMessage("Column Selection Saved!");
                }
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage, true);
        }
    }
}
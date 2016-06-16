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
    public partial class PickOrEditTableCTRL : BaseControl
    {
        static string prevPage = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
            }
            List<Stuff> list = new List<Stuff>();
            if (Session["DB_SELECTED"] != null)
            {
                int dbId = int.Parse(Session["DB_SELECTED"].ToString());
                ConSetting gb = DataService.Provider.GetConSettingById(dbId);
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3};", gb.ServerIP, gb.Database.Remove(gb.Database.Length - 2, 2), gb.Username, gb.Password);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("", con);
                    cmd.CommandText = "SELECT name FROM sysobjects WHERE xtype='U' order by name";
                    SqlDataReader r = cmd.ExecuteReader();
                    Stuff curr;
                    while (r.Read())
                    {
                        curr = new Stuff();
                        curr.Name = (r["name"] is DBNull) ? string.Empty : (string)r["name"];
                        list.Add(curr);
                    }
                    r.Close();
                }
            }
            rlbTable.DataSource = list;
            rlbTable.DataBind();

            getSelection();
        }

        private void getSelection()
        {
            if (Session["DB_SELECTED"] != null)
            {
                int dbId = int.Parse(Session["DB_SELECTED"].ToString());
                DBMetaTable curo = DataService.Provider.GetDBMetaTableByDbId(dbId);
                if (curo != null) {
                    foreach (ListItem item in rlbTable.Items){
                        if (item.Value == curo.TableName) {
                            item.Selected = true;
                        }
                    }
                    GetTop10Datarows(curo.TableName);
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (Session["DB_SELECTED"] != null)
            {
                if (rlbTable.SelectedIndex != -1)
                {
                    DBMetaTable mt = new DBMetaTable();
                    string tablename = rlbTable.SelectedValue;
                    int DbId = int.Parse(Session["DB_SELECTED"].ToString());
                    mt.TableName = tablename;
                    mt.DBId = DbId;
                    int result = DataService.Provider.SaveDBMetaTable(mt);
                    if (result > 0) { ShowMessage("Table saved successfully!"); }
                }
                else
                {
                    ShowError("Please pick a table!");
                }
            }
        }

        protected void rlbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rlbTable.SelectedIndex != -1)
            {
                string tablename = rlbTable.SelectedValue;
                GetTop10Datarows(tablename);
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
                    con.Close();
                }
            }
            GridView1.DataSource = tbl;
            GridView1.DataBind();
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            if (Session["DB_SELECTED"] != null)
            {
                int dbId = int.Parse(Session["DB_SELECTED"].ToString());
                DBMetaTable dmt = DataService.Provider.GetDBMetaTableByDbId(dbId);
                if (dmt != null && !string.IsNullOrEmpty(dmt.TableName))
                {
                    Response.Redirect(Util.BaseSiteUrl + "p.aspx?p=dm-column-selection", true);
                }
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage, true);
        }
    }
}
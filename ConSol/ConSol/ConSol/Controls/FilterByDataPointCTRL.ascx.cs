using ConSolHWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class FilterByDataPointCTRL : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            chkDatabase.DataSource = DataService.Provider.GetJustDbColumns();
            chkDatabase.DataBind();
        }

        protected void chkDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = "";
            if (chkDatabase.SelectedIndex != -1)
            {
                foreach (ListItem item in chkDatabase.Items)
                {
                    if (item.Selected)
                    {
                        selected += item.Value + ",";
                    }
                }
            }
            else
            {
                msgBox1.alert("Please select a data point!");
                return;
            }
            string finalText = selected.Remove(selected.Length - 1, 1);
            string SQL = GetSQL(finalText);
            string SQL2 = GetCountSQL(finalText);
            Session["FITER_SQL"] = SQL;
            Session["FILTER_SQLCOUNT"] = SQL2;
        }

        private string GetSQL(string finalText)
        {
            /*select MOBILENUMBER1, Gender, Email
            from[dbo].[VxDataPoints]
            where(MOBILENUMBER1 is not null and MOBILENUMBER1 <> '') and(Gender is not null AND Gender <> '') and(Email is not null AND Email <> '')*/
            if (!string.IsNullOrEmpty(finalText)) {
                string[] strArr = finalText.Split(',');
                string start = "SELECT " + finalText + " from VxDataPoints WHERE ";
                string end = "";
                foreach (string item in strArr)
                {
                    string p = string.Format("({0} is not null AND {0} <> '')",item);
                    end += p + "&";
                    p = null;
                }
               return start + end.Remove(end.Length - 1, 1).Replace("&", " and ");
            }
            return string.Empty;
        }

        protected void BtnCommand_Click(object sender, EventArgs e)
        {
            string selected = "";
            if (chkDatabase.SelectedIndex != -1)
            {
                foreach (ListItem item in chkDatabase.Items)
                {
                    if (item.Selected)
                    {
                        selected += item.Value + ",";
                    }
                }
            }
            else
            {
                msgBox1.alert("Please select a data point!");
                return;
            }
            string finalText = selected.Remove(selected.Length - 1, 1);
            string SQL = GetSQL(finalText);
            string SQL2 = GetCountSQL(finalText);
            Session["FITER_SQL"] = SQL;
            Session["FILTER_SQLCOUNT"] = SQL2;
            Response.Redirect(ConSolHWeb.Data.Util.BaseSiteUrl + "p.aspx?p=show-filter-data", true);
        }

        private string GetCountSQL(string finalText)
        {
            if (!string.IsNullOrEmpty(finalText))
            {
                string[] strArr = finalText.Split(',');
                string start = "SELECT count(*) from VxDataPoints WHERE ";
                string end = "";
                foreach (string item in strArr)
                {
                    string p = string.Format("({0} is not null AND {0} <> '')", item);
                    end += p + "&";
                    p = null;
                }
                return start + end.Remove(end.Length - 1, 1).Replace("&", " and ");
            }
            return string.Empty;
        }

    }
}
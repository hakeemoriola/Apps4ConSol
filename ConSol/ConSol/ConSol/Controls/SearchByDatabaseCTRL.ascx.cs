using ConSol.DAL;
using ConSolHWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class SearchByDatabaseCTRL : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildControl();
            checkAll();
        }

        private void BuildControl()
        {
            chkDatabase.DataSource = DataService.Provider.GetJustDabase();
            chkDatabase.DataBind();

            chkDbColumns.DataSource = DataService.Provider.GetJustDbColumns();
            chkDbColumns.DataBind();
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
                msgBox1.alert("Please select a Database!");
                return;
            }
            string finalText = selected.Remove(selected.Length - 1, 1);
            Label1.Text = finalText;
            selected = "";
            if (chkDbColumns.SelectedIndex != -1)
            {
                foreach (ListItem item in chkDbColumns.Items)
                {
                    if (item.Selected)
                    {
                        selected += item.Value + ",";
                    }
                }
            }
            else
            {
                msgBox1.alert("Please select a Column!");
                return;
            }
            string finalText2 = selected.Remove(selected.Length - 1, 1);
            Label2.Text = finalText;

            /*
             *  select a.NAME, a.Gender, a.ADDRESS, a.Town,a.MOBILENUMBER1,a.MOBILENUMBER2,a.Occupation,a.JOBSTATUS,
             *  a.Email,a.Industry,a.STATE,a.LGA,a.Source  from VxDataPoints a
                where Source in (1,4)
            */
            string SQL = GetSQL(finalText, finalText2, chkMustHaveValue.Checked);
            string SQL2 = GetCountSQL(finalText, finalText2, chkMustHaveValue.Checked);

            //Label1.Text = SQL;
           // Label2.Text = SQL2;
            Session["FITER_SQL"] = SQL;
            Session["FILTER_SQLCOUNT"] = SQL2;
            Response.Redirect(ConSolHWeb.Data.Util.BaseSiteUrl + "p.aspx?p=show-filter-data", true);
        }

        private string GetCountSQL(string finalText, string finalText2, bool musthave)
        {
            if (!string.IsNullOrEmpty(finalText) && !string.IsNullOrEmpty(finalText2))
            {
                string[] strArr = finalText2.Split(',');
                string start = "SELECT count(*) from VxDataPoints WHERE Source in (" + finalText + ") &";
                string end = "";
                if (musthave)
                {
                    foreach (string item in strArr)
                    {
                        string p = string.Format("({0} is not null AND {0} <> '')", item);
                        end += p + "&";
                        p = null;
                    }
                    return start.Replace("&", " and ") + end.Remove(end.Length - 1, 1).Replace("&", " and ");
                }
                else { }
                return start.Replace("&", "");
            }
            return string.Empty;
        }

        private string GetSQL(string finalText, string finalText2, bool musthave)
        {
            if (!string.IsNullOrEmpty(finalText) && !string.IsNullOrEmpty(finalText2))
            {
                string[] strArr = finalText2.Split(',');
                string start = "SELECT " + finalText2 + " from VxDataPoints WHERE Source in (" + finalText + ") &";
                string end = "";
                if (musthave)
                {
                    foreach (string item in strArr)
                    {
                        string p = string.Format("({0} is not null AND {0} <> '')", item);
                        end += p + "&";
                        p = null;
                    }
                    return start.Replace("&", " and ") + end.Remove(end.Length - 1, 1).Replace("&", " and ");
                }
                else { }
                return start.Replace("&", "");
            }
            return string.Empty;
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
                msgBox1.alert("Please select a Database!");
                return;
            }
            string finalText = selected.Remove(selected.Length - 1, 1);
           // Label1.Text = finalText;
        }

        protected void chkPickColumns_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPickColumns.Checked)
            {
                plnColumn.Visible = true;
                checkAll();
            }
            else
            {
                plnColumn.Visible = false;
                chkDbColumns.ClearSelection();
            }
        }

        protected void lnkBtnchkAll_Click(object sender, EventArgs e)
        {
            checkAll();
        }

        private void unCheckAll()
        {
            foreach (ListItem item in chkDbColumns.Items)
            {
                item.Selected = false;
            }
        }

        protected void lnkBtnUnchkAll_Click(object sender, EventArgs e)
        {
            unCheckAll();
        }

        private void checkAll()
        {
            foreach (ListItem item in chkDbColumns.Items)
            {
                item.Selected = true;
            }
        }

        protected void chkDbColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = "";
            if (chkDbColumns.SelectedIndex != -1)
            {
                foreach (ListItem item in chkDbColumns.Items)
                {
                    if (item.Selected)
                    {
                        selected += item.Value + ",";
                    }
                }
            }
            else
            {
                msgBox1.alert("Please select a Column!");
                return;
            }
            string finalText = selected.Remove(selected.Length - 1, 1);
            //Label2.Text = finalText;
        }

      
    }
}
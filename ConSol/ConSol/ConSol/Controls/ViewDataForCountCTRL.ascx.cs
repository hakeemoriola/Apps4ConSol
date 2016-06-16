using ConSolHWeb.Data;
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
    public partial class ViewDataForCountCTRL : BaseControl
    {
        private string SQLQuery = ""; private string ColName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["PageSize"] = 500;
                Session["PageIndex"] = 1;
                if (Request.Params["cln"] != null)
                {
                    ColName = Request.Params["cln"];
                    if (!IsPostBack)
                    {
                        this.GetCustomersPageWise(1, ColName);
                        int recordCount = int.Parse(Session["RecordCount"].ToString());
                        Label1.Text = string.Format("Showing {0} - {1} of {2}", 1, (int.Parse(Session["PageIndex"].ToString()) * int.Parse(ddlPageSize.SelectedValue)), recordCount);
                    }
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                }
            }
        }

        private void BuildControls(List<VxExportDataObj> list)
        {
            /*
            StringBuilder sb = new StringBuilder();
            plhResultofSearch.Controls.Clear();
            foreach (VxExportDataObj item in list)
            {
                sb.Append(string.Format(@"
                           <tr>
                                <td>{0}</td>
                                <td>{1}</td>
                                <td>{2}</td>
                                <td>{3}</td>
                                <td>{4}</td>
                                <td>{5}</td>
                                <td>{6}</td>
                                <td>{7}</td>
                                <td>{8}</td>
                                <td>{9}</td>
                                <td>{10}</td>
                                <td>{11}</td>
                           </tr>
                           ", item.NAME.ToString(), item.GENDER.ToString(), item.AGE.ToString(), item.ADDRESS.ToString(), item.TOWN.ToString(), item.MOBILENUMBER1.ToString(), item.OCCUPATION.ToString(), item.JOBSTATUS.ToString(), item.EMAIL.ToString(), item.INDUSTRY.ToString(), item.STATE.ToString(), item.LGA.ToString()));
            }
            plhResultofSearch.Controls.Add(new LiteralControl(sb.ToString()));
            sb = null;
            */
        }

        protected void PageSize_Changed(object sender, EventArgs e)
        {
            if (Request.Params["cln"] != null)
            {
                ColName = Request.Params["cln"];
                if (Session["PageSize"] != null && Session["PageIndex"] != null)
                {
                    Session["PageSize"] = ddlPageSize.SelectedValue;
                    this.GetCustomersPageWise(int.Parse(Session["PageIndex"].ToString()), ColName);
                }
            }
        }

        private void GetCustomersPageWise(int pageIndex, string ClName)
        {
            string constring = new SqlDataProvider().connectionString("me");
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand(string.Format("GetResult{0}Wise", ClName), con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", int.Parse(Session["PageSize"].ToString()));
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.CommandTimeout = 0;
                    IDataReader idr = cmd.ExecuteReader();
                    GridView1.DataSource = idr;
                    GridView1.DataBind();
                    idr.Close();
                    con.Close();
                    int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    Session["RecordCount"] = recordCount;
                    int pcount = recordCount / int.Parse(Session["PageSize"].ToString());
                    Session["PageCount"] = pcount;
                }
            }
        }

        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.GetCustomersPageWise(pageIndex, ColName);
        }

        private void PopulatePager(int recordCount, int currentPage)
        {
            double dblPageCount = (double)((decimal)recordCount / decimal.Parse(ddlPageSize.SelectedValue));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                pages.Add(new ListItem("First", "1", currentPage > 1));
                for (int i = 1; i <= 6; i++) //pageCount
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }

        protected void rptPager_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            if (Request.Params["cln"] != null)
            {
                ColName = Request.Params["cln"];
                this.GetCustomersPageWise(1, ColName);
                Session["PageIndex"] = 1;
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnLast.Enabled = true;
                btnNext.Enabled = true;
                int recordCount = int.Parse(Session["RecordCount"].ToString());
                Label1.Text = string.Format("Showing {0} - {1} of {2}", 1, (int.Parse(Session["PageIndex"].ToString()) * int.Parse(ddlPageSize.SelectedValue)), recordCount);
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            if (Request.Params["cln"] != null)
            {
                ColName = Request.Params["cln"];
                int PageCount = int.Parse(Session["PageCount"].ToString());
                int PageIndex = int.Parse(Session["PageIndex"].ToString());
                if (PageIndex > 0)
                {
                    this.GetCustomersPageWise(PageIndex - 1, ColName);
                    btnLast.Enabled = true;
                    Session["PageIndex"] = PageIndex - 1;

                    int recordCount = int.Parse(Session["RecordCount"].ToString());
                    Label1.Text = string.Format("Showing {0} - {1} of {2}", ((PageIndex - 2) * int.Parse(ddlPageSize.SelectedValue)) + 1, ((PageIndex - 1) * int.Parse(ddlPageSize.SelectedValue)), recordCount);
                }

                if (PageIndex == 0)
                {
                    btnFirst.Enabled = false;
                }
                if (PageCount - 1 == PageIndex + 1)
                {
                    btnNext.Enabled = true;
                }
                if (PageIndex == 0)
                {
                    btnPrevious.Enabled = false;
                }
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (Request.Params["cln"] != null)
            {
                ColName = Request.Params["cln"];
                int i = int.Parse(Session["PageIndex"].ToString()) + 1;
                Session["PageIndex"] = i;

                if (i <= int.Parse(Session["PageCount"].ToString()))
                {
                    btnLast.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnFirst.Enabled = true;
                }

                if (int.Parse(Session["PageCount"].ToString()) - 1 == int.Parse(Session["PageIndex"].ToString()))
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }

                this.GetCustomersPageWise(i, ColName);
                Session["PageIndex"] = i;
                int recordCount = int.Parse(Session["RecordCount"].ToString());
                Label1.Text = string.Format("Showing {0} - {1} of {2}", ((i - 1) * int.Parse(ddlPageSize.SelectedValue)) + 1, ((i) * int.Parse(ddlPageSize.SelectedValue)), recordCount);
            }
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            if (Request.Params["cln"] != null)
            {
                ColName = Request.Params["cln"];
                if (Session["PageCount"] != null)
                {
                    this.GetCustomersPageWise(int.Parse(Session["PageCount"].ToString()) + 1, ColName);
                    Session["PageIndex"] = int.Parse(Session["PageCount"].ToString()) + 1;
                    btnLast.Enabled = false;
                    btnFirst.Enabled = true;
                    int recordCount = int.Parse(Session["RecordCount"].ToString());
                    Label1.Text = string.Format("Showing {0} - {1} of {2}", (int.Parse(Session["PageCount"].ToString()) * int.Parse(ddlPageSize.SelectedValue)) + 1, recordCount, recordCount);
                }
            }
        }
    }
}
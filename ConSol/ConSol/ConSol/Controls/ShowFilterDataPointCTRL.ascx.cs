using ConSol.DAL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class ShowFilterDataPointCTRL : System.Web.UI.UserControl
    {
        private static string prevPage = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
            }

            string[] allowedUsers = ConfigurationManager.AppSettings["AllowedToExport"].ToString().Trim().Split(',');
            string current = HttpContext.Current.Session["UserRole"].ToString();
            bool result = allowedUsers.Contains(current);
            BtnExportToExcel.Enabled = result;
            BtnExportToExcel.Visible = result;
        }

        protected void ddPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // handle event
            DropDownList ddpagesize = sender as DropDownList;
            gvCustomers.PageSize = Convert.ToInt32(ddpagesize.SelectedItem.Text);
            ViewState["PageSize"] = ddpagesize.SelectedItem.Text;
            gvCustomers.DataBind();
        }

        protected void gvCustomers_DataBound(object sender, EventArgs e)
        {
            if (gvCustomers.Rows.Count > 0)
            {
                if (ViewState["PageSize"] != null)
                {
                    DropDownList ddPagesize = gvCustomers.BottomPagerRow.FindControl("ddPageSize") as DropDownList;
                    ddPagesize.Items.FindByText((ViewState["PageSize"].ToString())).Selected = true;
                }

                Label lblCount = gvCustomers.BottomPagerRow.FindControl("lblPageCount") as Label;
                int totRecords = (gvCustomers.PageIndex * gvCustomers.PageSize) + gvCustomers.PageSize;
                int totCustomerCount = AdvWorksDB.GetFilteredCount("");

                totRecords = totRecords > totCustomerCount ? totCustomerCount : totRecords;
                lblCount.Text = ((gvCustomers.PageIndex * gvCustomers.PageSize) + 1).ToString("N00", CultureInfo.InvariantCulture) + " to " + (totRecords).ToString("N00", CultureInfo.InvariantCulture) + " of " + totCustomerCount.ToString("N00", CultureInfo.InvariantCulture);
                gvCustomers.BottomPagerRow.Visible = true;
            }
            else
            {
            }
        }

        protected void BtnRefine_Click(object sender, EventArgs e)
        {
            // Response.Redirect(ConSolHWeb.Data.Util.BaseSiteUrl + "p.aspx?p=filter-by-dp", true);
            Response.Redirect(prevPage, true);
        }

        protected void BtnExportToExcel_Click(object sender, EventArgs e)
        {
            var products = GetFilteredSearch();
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("FilteredSearch");
            var totalCols = products.Columns.Count;
            var totalRows = products.Rows.Count;

            for (var col = 1; col <= totalCols; col++)
            {
                workSheet.Cells[1, col].Value = products.Columns[col - 1].ColumnName;
            }
            for (var row = 1; row <= totalRows; row++)
            {
                for (var col = 0; col < totalCols; col++)
                {
                    workSheet.Cells[row + 1, col + 1].Value = products.Rows[row - 1][col];
                }
            }
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=FilteredSearch.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        private DataTable GetFilteredSearch()
        {
            if (Session["FITER_SQL"] != null)
            {
                string SQL = Session["FITER_SQL"].ToString();
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
                using (var cmd = new SqlCommand(SQL, conn))
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    var products = new DataTable();
                    adapter.Fill(products);
                    return products;
                }
            }
            return null;
        }
    }
}
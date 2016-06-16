using ConSol.DAL;
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
    public partial class HarmonizedViewCTRL : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                pager.Visible = true;

                if (ViewState["PageSize"] != null)
                {
                    DropDownList ddPagesize = gvCustomers.BottomPagerRow.FindControl("ddPageSize") as DropDownList;
                    ddPagesize.Items.FindByText((ViewState["PageSize"].ToString())).Selected = true;
                }

                Label lblCount = gvCustomers.BottomPagerRow.FindControl("lblPageCount") as Label;
                int totRecords = (gvCustomers.PageIndex * gvCustomers.PageSize) + gvCustomers.PageSize;
                int totCustomerCount = AdvWorksDB.GetCustomersCount(hfSearchCriteria.Value);

                totRecords = totRecords > totCustomerCount ? totCustomerCount : totRecords;
                lblCount.Text = ((gvCustomers.PageIndex * gvCustomers.PageSize) + 1).ToString() + " to " + Convert.ToString(totRecords) + " of " + totCustomerCount.ToString();
                gvCustomers.BottomPagerRow.Visible = true;

            }
            else
            {
                pager.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bool isString = false;
            string searchCriteria = "";
            lblSearchError.Text = "";

            if (txtSearchValue.Text.Trim() != "")
            {
                switch (ddSearchField.SelectedValue)
                {
                    case "City":
                    case "Title":
                    case "FirstName":
                    case "LastName":
                    case "BirthDate":
                        isString = true;
                        break;
                    case "customerkey":
                    case "CustomerAlternateKey":
                        isString = false;
                        break;
                    default:
                        break;
                }

                switch (ddSearchCriteria.SelectedItem.Text)
                {
                    case "Contains":
                        searchCriteria = " like ";
                        break;
                    case "DoesNotContain":
                        searchCriteria = " not like ";
                        break;
                    case "EqualTo":
                        searchCriteria = " = ";
                        break;
                    case "NotEqualTo":
                        searchCriteria = " <> ";
                        break;
                    case "GreaterThan":
                        searchCriteria = " > ";
                        break;
                    case "LessThan":
                        searchCriteria = " < ";
                        break;
                    case "GreaterThanOrEqualTo":
                        searchCriteria = " >= ";
                        break;
                    case "LessThanOrEqualTo":
                        searchCriteria = " <= ";
                        break;
                }

                if (isString)
                {
                    if (searchCriteria == " like " || searchCriteria == " not like ")
                    {
                        hfSearchCriteria.Value = ddSearchField.SelectedValue + searchCriteria + "'%" + txtSearchValue.Text + "%'";

                    }
                    else if (searchCriteria == " = ")
                        hfSearchCriteria.Value = ddSearchField.SelectedValue + searchCriteria + "'" + txtSearchValue.Text + "'";
                    else
                    {
                        lblSearchError.Visible = true;
                        lblSearchError.Text = "Can't use " + ddSearchCriteria.SelectedItem.Text + " to " + ddSearchField.SelectedItem.Text;
                        return;
                    }
                }
                else
                {
                    if (searchCriteria == " like " || searchCriteria == " not like ")
                    {
                        lblSearchError.Visible = true;
                        lblSearchError.Text = "Can't use " + ddSearchCriteria.SelectedItem.Text + " to " + ddSearchField.SelectedItem.Text;
                        return;
                    }
                    else
                        hfSearchCriteria.Value = ddSearchField.SelectedValue + searchCriteria + txtSearchValue.Text;
                }
            }
            else
                hfSearchCriteria.Value = "";
            gvCustomers.DataBind();
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            hfSearchCriteria.Value = "";
            txtSearchValue.Text = "";
            lblSearchError.Text = "";
            gvCustomers.DataBind();
        }
    }
}

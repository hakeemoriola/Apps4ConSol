using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class OSearchCTRL : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {

        }
    }
}

using ConSolHWeb.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class HarmonizedReportsCTRL : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SESSION_CACHE"] != null)
            {
                BuildControl2();
            }
            else { BuildControl(); }
        }

        private void BuildControl2()
        {
            List<RawData> list1 = new List<RawData>();
            StringBuilder sb = new StringBuilder();
            list1 = (List<RawData>)Session["SESSION_CACHE"];
            plhRecordCount.Controls.Clear();
            long total = 0;
            foreach (var item in list1)
            {
                sb.Append(string.Format(@"
                                            <tr>
                                                <td>{0}</td>
                                                <td>{1}</td>
                                                <td>{2}</td>
                                                <td>{3}</td>
                                                <td><strong>{4}</strong></td>
                                            </tr>", item.DbId, item.IPAddress, item.DatabaseName.Remove(item.DatabaseName.Length - 2, 2), item.TableName, item.TotalRecordCount.ToString("N00", CultureInfo.InvariantCulture)));
                total += item.TotalRecordCount;
            }
            plhRecordCount.Controls.Add(new LiteralControl(sb.ToString()));
            Label1.Text = total.ToString("N00", CultureInfo.InvariantCulture);
        }

        private void BuildControl()
        {
            StringBuilder sb = new StringBuilder();
            List<RawData> list0 = DataService.Provider.GetPrepHarmonizedData();
            if (list0 != null)
            {
                foreach (var item in list0)
                {
                    sb.Append(string.Format(@"
                                            <tr>
                                                <td>{0}</td>
                                                <td>{1}</td>
                                                <td>{2}</td>
                                                <td>{3}</td>
                                                <td>{4}</td>
                                            </tr>", item.DbId, item.IPAddress, item.DatabaseName.Remove(item.DatabaseName.Length - 2, 2), item.TableName, item.TotalRecordCount.ToString("N00", CultureInfo.InvariantCulture)));
                }
                plhRecordCount.Controls.Add(new LiteralControl(sb.ToString()));
            }
        }

        protected void BtnRefresh_Click(object sender, EventArgs e)
        {
            BtnRefresh.Enabled = false;

            StringBuilder sb = new StringBuilder();
            List<RawData> list1 = new List<RawData>();
            List<RawData> list0 = DataService.Provider.GetPrepHarmonizedData();
            foreach (RawData item in list0)
            {
                RawData corr = new RawData();
                corr.DatabaseName = item.DatabaseName;
                corr.DbId = item.DbId;
                corr.ColumnName = item.ColumnName;
                corr.IPAddress = item.IPAddress;
                corr.TableName = DataService.Provider.GetDBTableByDBId(item.DbId);
                corr.TotalRecordCount = DataService.Provider.GetUniqueRecordCountPerDBByDbId(int.Parse(item.DbId.Trim()), corr.ColumnName, corr.TableName);
                list1.Add(corr);
            }

            list0 = null;
            Session["SESSION_CACHE"] = list1;
            plhRecordCount.Controls.Clear();
            long total = 0;
            foreach (var item in list1)
            {
                sb.Append(string.Format(@"
                                            <tr>
                                                <td>{0}</td>
                                                <td>{1}</td>
                                                <td>{2}</td>
                                                <td>{3}</td>
                                                <td><strong>{4}</strong></td>
                                            </tr>", item.DbId, item.IPAddress, item.DatabaseName.Remove(item.DatabaseName.Length - 2, 2), item.TableName, item.TotalRecordCount.ToString("N00", CultureInfo.InvariantCulture)));
                total += item.TotalRecordCount;
            }
            plhRecordCount.Controls.Add(new LiteralControl(sb.ToString()));
            Label1.Text = total.ToString("N00", CultureInfo.InvariantCulture);

            list1 = null;
            BtnRefresh.Enabled = true;
        }
    }
}
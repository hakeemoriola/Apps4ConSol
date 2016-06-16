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
    public partial class RawDataReportCTRL : BaseControl
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
                                                 <td class='{5}'>{0}</td>
                                                <td class='{5}'>{1}</td>
                                                <td class='{5}'>{2}</td>
                                                <td class='{5}'>{3}</td>
                                                <td class='{5}'><strong>{4}</strong></td>
                                                <td class='{5}'><strong>{6}</strong></td>
                                                <td class='{5}'></td>
                                            </tr>", item.DbId, item.IPAddress, item.DatabaseName.Remove(item.DatabaseName.Length - 2, 2), item.TableName, item.TotalRecordCount.ToString("N00", CultureInfo.InvariantCulture), getStyle(item.NowSyching), item.TotalUniqueCount));
                total += item.TotalRecordCount;
            }
            plhRecordCount.Controls.Add(new LiteralControl(sb.ToString()));
            Label1.Text = total.ToString("N00", CultureInfo.InvariantCulture);
        }

        private void BuildControl()
        {
            StringBuilder sb = new StringBuilder();
            List<RawData> list0 = DataService.Provider.GetPrepRawData();
            if (list0 != null)
            {
                foreach (var item in list0)
                {
                    sb.Append(string.Format(@"
                                            <tr>
                                                <td class='{5}'>{0}</td>
                                                <td class='{5}'>{1}</td>
                                                <td class='{5}'>{2}</td>
                                                <td class='{5}'>{3}</td>
                                                <td class='{5}'><strong>{4}</strong></td>
                                                <td class='{5}'><strong>{6}</strong></td>
                                                <td class='{5}'></td>
                                            </tr>", item.DbId, item.IPAddress, item.DatabaseName.Remove(item.DatabaseName.Length - 2, 2), item.TableName, item.TotalRecordCount.ToString("N00", CultureInfo.InvariantCulture), getStyle(item.NowSyching), item.TotalUniqueCount));
                }
                plhRecordCount.Controls.Add(new LiteralControl(sb.ToString()));
            }
        }

        protected void BtnRefresh_Click(object sender, EventArgs e)
        {
            BtnRefresh.Enabled = false;

            StringBuilder sb = new StringBuilder();
            List<RawData> list1 = new List<RawData>();
            List<RawData> list0 = DataService.Provider.GetPrepRawData();
            foreach (RawData item in list0)
            {
                RawData corr = new RawData();
                corr.DatabaseName = item.DatabaseName;
                corr.DbId = item.DbId;
                corr.IPAddress = item.IPAddress;
                corr.TableName = DataService.Provider.GetDBTableByDBId(item.DbId);
                corr.TotalRecordCount = DataService.Provider.GetRecordCountPerDBByDbId(int.Parse(item.DbId.Trim()), corr.TableName);
                corr.NowSyching = item.NowSyching;
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
                                                <td class='{5}'>{0}</td>
                                                <td class='{5}'>{1}</td>
                                                <td class='{5}'>{2}</td>
                                                <td class='{5}'>{3}</td>
                                                <td class='{5}'><strong>{4}</strong></td>
                                                <td class='{5}'><strong>{6}</strong></td>
                                                <td class='{5}'></td>
                                            </tr>", item.DbId, item.IPAddress, item.DatabaseName.Remove(item.DatabaseName.Length - 2, 2), item.TableName, item.TotalRecordCount.ToString("N00", CultureInfo.InvariantCulture), getStyle(item.NowSyching), item.TotalUniqueCount));
                total += item.TotalRecordCount;
            }
            plhRecordCount.Controls.Add(new LiteralControl(sb.ToString()));
            Label1.Text = total.ToString("N00", CultureInfo.InvariantCulture);

            list1 = null;
            BtnRefresh.Enabled = true;
        }

        private string getStyle(bool? nowSyching)
        {
            if (nowSyching == null)
                return "red";
            if (nowSyching == true)
                return "green";
            if (nowSyching == false)
                return "orange";
            return "";
        }
    }
}
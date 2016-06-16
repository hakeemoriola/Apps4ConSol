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
    public partial class ApplicationDataReportedCTRL : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildControl();
        }

        private void BuildControl()
        {
            //DataDashBoard
            StringBuilder sb = new StringBuilder();
            List<RawDataDashBoard> data = DataService.Provider.GetRawDataDashBoard().OrderBy(c => c.Id).ToList();
            long totalRecord = 0, totalUnique = 0;
            foreach (var item in data)
            {
                string row = string.Format(@"<tr>
                                                    <td>{0}</td>
                                                    <td>{1}</td>
                                                    <td>{2}</td>
                                                    <td>{3}</td>
                                                    <td style='text-align: center'>
                                                      {4}</td>
                                                    <td style='text-align: center'>{5}</td>
                                                    <td></td>
                                            </tr>", item.Id, item.IPAddress, item.DatabaseName, item.TableName, item.TotalRecordCount, item.TotalUniqueCount);
                sb.Append(row);
                row = null;
                totalRecord += item.TotalRecordCount;
                totalUnique += item.TotalUniqueCount;
            }
            plhRecordCount.Controls.Add(new LiteralControl(sb.ToString()));
            lblTotalRecord.Text = totalRecord.ToString("N00", CultureInfo.InvariantCulture);
            lblTotalUnique.Text = totalUnique.ToString("N00", CultureInfo.InvariantCulture);
        }
    }
}
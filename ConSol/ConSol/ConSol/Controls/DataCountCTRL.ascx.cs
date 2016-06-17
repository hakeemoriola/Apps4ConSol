using ConSolHWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConSol.Controls
{
    public partial class DataCountCTRL : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //select count(*) from [VxTelephones]
            DataCount item = DataService.Provider.GetDataCount();
            if (item != null)
            {
                StringBuilder sb = new StringBuilder();
                plhResultofSearch.Controls.Clear();
                sb.Append(string.Format(@"
                       <tr>
                            <td><a href='{11}p.aspx?p=view-data-for-count&cnt={0}&cln=NAME'>{0}</a></td>
                            <td><a href='{11}p.aspx?p=view-data-for-count&cnt={1}&cln=GENDER'>{1}</a></td>
                            <td><a href='{11}p.aspx?p=view-data-for-count&cnt={2}&cln=AGE'>{2}</a></td>
                            <td><a href='{11}p.aspx?p=view-data-for-count&cnt={3}&cln=TOWN'>{3}</a></td>
                            <td><a href='{11}p.aspx?p=view-data-for-count&cnt={4}&cln=PHONENO'>{4}</a></td>
                            <td><a href='{11}p.aspx?p=view-data-for-count&cnt={5}&cln=OCCUPATION'>{5}</a></td>
                            <td><a href='{11}p.aspx?p=view-data-for-count&cnt={6}&cln=JOBSTATUS'>{6}</a></td>
                            <td><a href='{11}p.aspx?p=view-data-for-count&cnt={7}&cln=EMAIL'>{7}</a></td>
                            <td><a href='{11}p.aspx?p=view-data-for-count&cnt={8}&cln=INDUSTRY'>{8}</a></td>
                            <td><a href='{11}p.aspx?p=view-data-for-count&cnt={9}&cln=STATE'>{9}</a></td>
                            <td><a href='{11}p.aspx?p=view-data-for-count&cnt={10}&cln=LGA'>{10}</a></td>
                       </tr>"
                           , item.NAMECount.ToString()
                           , item.GENDERCount.ToString()
                           , item.AGECount.ToString()
                           , item.TOWNCount.ToString()
                           , item.PhoneNoCount.ToString()
                           , item.OCCUPATIONCount.ToString()
                           , item.JOBSTATUSCount.ToString()
                           , item.EMAILCount.ToString()
                           , item.INDUSTRYCount.ToString()
                           , item.STATECount.ToString()
                           , item.LGACount.ToString()
                           , Util.BaseSiteUrl));

                plhResultofSearch.Controls.Add(new LiteralControl(sb.ToString()));
                sb = null;
            }
        }
    }
}
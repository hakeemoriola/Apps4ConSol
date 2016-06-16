using ConSolHWeb.Data.Models;
using ExcelLibrary.SpreadSheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace ConSolHWeb.Data
{
    /// <summary>
    /// Summary description for Util
    /// </summary>
    public class Util
    {
        public static int ConfigFilesize = 525000; //512kb; 1050000;//1M

        public static string DateFormat = "dd/MM/yyyy";

        public static DateTime ExpiryDate = DateTime.ParseExact("30/06/2016", DateFormat, null);

        public static bool CheckColumnExits(string columnName, int? tableId, int? dbId)
        {
            return DataService.Provider.CheckColumnExits(columnName, (int)tableId, (int)dbId);
        }

        public static bool CheckTableExits(string tableName, int? dBId)
        {
            return DataService.Provider.CheckTableExits(tableName, (int)dBId);
        }

        public static string From = ConfigurationManager.AppSettings["EmailSender"];

        public static string Host = ConfigurationManager.AppSettings["EmailSenderHost"];

        public static string MailTitle = ConfigurationManager.AppSettings["MailTitle"];

        public static string SenderUrl = ConfigurationManager.AppSettings["SenderUrl"];

        public static string Password = ConfigurationManager.AppSettings["EmailSenderPassword"];

        public static string SenderCompany = ConfigurationManager.AppSettings["SenderCompany"];

        public static string EmailSenderPort = ConfigurationManager.AppSettings["EmailSenderPort"];

        public static string EmailSenderEnableSsl = ConfigurationManager.AppSettings["EmailSenderEnableSsl"];

        public static string BaseSiteUrl
        {
            get
            {
                HttpContext context = HttpContext.Current;
                string baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + '/';
                return baseUrl;
            }
        }

        public static string BaseVirtualAppPath
        {
            get
            {
                HttpContext context = HttpContext.Current;
                string url = context.Request.ApplicationPath;
                if (url.EndsWith("/"))
                    return url;
                else
                    return url + "/";
            }
        }

        public static List<List<float[]>> splitList(List<float[]> locations, int nSize = 30)
        {
            var list = new List<List<float[]>>();

            for (int i = 0; i < locations.Count; i += nSize)
            {
                list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
            }

            return list;
        }

        public static string BeautifyDate(string colon_date)
        {
            Hashtable ht = new Hashtable();
            ht.Add("01", "January");
            ht.Add("02", "February");
            ht.Add("03", "March");
            ht.Add("04", "April");
            ht.Add("05", "May");
            ht.Add("06", "June");
            ht.Add("07", "July");
            ht.Add("08", "August");
            ht.Add("09", "September");
            ht.Add("10", "October");
            ht.Add("11", "November");
            ht.Add("12", "December");
            string new_date = ""; string word_month = "";
            if (!string.IsNullOrEmpty(colon_date.Trim()))
            {
                string[] op = colon_date.Split(';');
                word_month = ht[op[0]].ToString();
                new_date = word_month + ", " + op[1];
            }
            return new_date;
        }

        public static string checkZero(string p)
        {
            string me = "";
            if (p.StartsWith("0"))
            {
                me = p.Remove(0, 1);
                return me;
            }
            else
            {
                return p;
            }
        }

        public static string checkZeroBoth(string p)
        {
            string month, day, year = "";
            string[] arr = p.Split('/');
            if (arr[0].StartsWith("0"))
            {
                month = arr[0].Remove(0, 1);
            }
            else
            {
                month = arr[0];
            }
            if (arr[1].StartsWith("0"))
            {
                day = arr[1].Remove(0, 1);
            }
            else
            {
                day = arr[1];
            }
            year = arr[2];
            return month + "/" + day + "/" + year; // +" 12:00:00 AM";
        }

        public static void ExportToExcel(IList list, Type type, string filename)
        {
            StringBuilder str = new StringBuilder();
            string strFile = filename;
            string strcontentType = "application/excel";
            if (list != null)
            {
                str.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                str.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                str.Append("<DIV  style='font-size:12px;'>");
                str.Append("<table  align='center' border='1' bordercolor='#00aeef' width='99%' class='reporttable1' cellspacing='0' cellpadding='0' style='font-size:10;'>");

                PropertyInfo[] mi = type.GetProperties();
                str.Append("<tr style='font-size:16px;'>");
                str.Append("<td><b>S/N</b></td>");
                foreach (PropertyInfo item in mi)
                {
                    str.Append("<td><b>");
                    str.Append(item.Name);
                    str.Append(" </b></td>");
                }
                str.Append("</tr>");
                int me = 1;
                foreach (object itemk in list)
                {
                    str.Append("<tr>");
                    str.Append("<td>" + (me++) + "</td>");
                    foreach (PropertyInfo itk in itemk.GetType().GetProperties())
                    {
                        str.Append("<td>");
                        str.Append(itk.GetValue(itemk, null)).ToString();
                        str.Append("</td>");
                    }
                    str.Append("</tr>");
                }
            }
            str.Append("</table>".ToString());
            str.Append("</div></body></html>");
            // ShowMessage("Excel Generated!");
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.BufferOutput = true;
            HttpContext.Current.Response.ContentType = strcontentType;
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
            HttpContext.Current.Response.Write(str.ToString());
            HttpContext.Current.Response.Flush();
            ////HttpContext.Current.Response.Close();
            HttpContext.Current.Response.End();
        }

        public static bool CKE()
        {
            return (System.DateTime.Now > ExpiryDate);
        }

        public static bool EducationalHistory(int p)
        {
            // To Do
            return true;
        }

        public static string Encrypt(string cleanString)
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);
            return BitConverter.ToString(hashedBytes);
        }

        public static void ExecuteDisablingFeature()
        {
            //SqlHelper.ExecuteNonQuery(new SqlDataProvider().connectionString("me"), "ExecuteDisablingFeature", null);
        }

        public static void GenerateAndSendEmail(Models.User supplier, string EMAIL_TYPE)
        {
            string Name = supplier.LastName + " " + supplier.FirstName;
            string cypher = PortalSecurity.Encrypt("Activate");
            string actvateLink = SenderUrl + "jobportal.aspx?job=activate&a=" + cypher + "&u=" + supplier.UserName + "&t=" + supplier.tempData;
            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            try
            {
                m.From = new MailAddress(From, SenderCompany);
                m.To.Add(new MailAddress(supplier.UserName, Name));
                //similarly BCC
                m.Subject = "Account Creation Successful";
                m.IsBodyHtml = true;
                m.Body = string.Format(
                @"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
                        <link href='{5}/mail.css' type='text/css' rel='stylesheet' />
                        <title></title>
                    </head>
                    <body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
                        <center>
                                <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='backgroundTable'>
                                    <tr>
                                        <td align='center' valign='top'>
                                            <table border='0' cellpadding='10' cellspacing='0' width='600' id='templatePreheader'>
                                                <tr>
                                                    <td valign='top' class='preheaderContent'>

                                                        <table border='0' cellpadding='10' cellspacing='0' width='100%'>
                                                            <tr>
                                                                <td valign='top'>
                                                                    &nbsp;</td>
                                                                <td valign='top' width='190'>
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>

                                                    </td>
                                                </tr>
                                            </table>
                                            <table border='0' cellpadding='0' cellspacing='0' width='600' id='templateContainer'>
                                                <tr>
                                                    <td align='center' valign='top'>
                                                        <table border='0' cellpadding='0' cellspacing='0' width='600' id='templateHeader'>
                                                            <tr>
                                                                <td class='headerContent'>

                                                                    <img src='{5}/logo.gif'
                                                                        style='max-width:100%; float: left;' id='headerImage campaign-icon'
                                                                        mc:label='header_image' mc:edit='header_image' mc:allowdesigner mc:allowtext />

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='center' valign='top'>
                                                        <table border='0' cellpadding='0' cellspacing='0' width='600' id='templateBody'>
                                                            <tr>
                                                                <td valign='top' class='bodyContent'>

                                                                     <table border='0' cellpadding='20' cellspacing='0' width='100%'>
                                                                                        <tr>
                                                                                            <td valign='top'>
                                                                                                <div mc:edit='std_content00'>
                                                                                                    <h5 class='h5'>Account Creation Successful</h5>
                                                                                                     <h5 class='h5'> Dear {0},</h5>
                                                                                                   <p> You are welcome to {3} Recruitment Portal.&nbsp;
                                                                                                       Your account has been successfully registered.<br />
                                                                                                       Please click on the link below to activate your account.<br />
                                                                                                       <a href='{1}' target='_blank'>Activate your Account</a>   <br />
                                                                                                    if you encounter any trouble doing this, kindly send an email to {4}</p>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='center' valign='top'>
                                                        <table border='0' cellpadding='10' cellspacing='0' width='600' id='templateFooter'>
                                                            <tr>
                                                                <td valign='top' class='footerContent'>

                                                                    <table border='0' cellpadding='10' cellspacing='0' width='100%'>
                                                                        <tr>
                                                                            <td colspan='2' valign='middle' id='social'>
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign='top' width='350'>
                                                                                <div mc:edit='std_footer'>
                                                                                    <span class='bodyContent'
                                                                                        style='color: rgb(6, 41, 3); font-family: Arial, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 18px; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;'>
                                                                                    &copy; {2} &nbsp; {3}. All rights reserved</span><em><span
                                                                                        class='bodyContent'>.</span></em>
                                                                                    <br />
                                                                                </div>
                                                                            </td>
                                                                            <td valign='top' width='190' id='monkeyRewards'>
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan='2' valign='middle' id='utility'>
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                    </table>

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </center>
                    </body>
                    </html>

                ", Name, actvateLink, DateTime.Now.Year.ToString(), SenderCompany, From, SenderUrl);
                sc.Host = Util.Host;
                sc.Port = int.Parse(Util.EmailSenderPort);
                sc.Credentials = new System.Net.NetworkCredential(Util.From, Util.Password);
                sc.EnableSsl = Convert.ToBoolean(int.Parse(Util.EmailSenderEnableSsl));
                sc.Send(m);
            }
            catch (Exception ex)
            {
            }
        }

        public static void ExporttoCSVFromSQL(string sql, string filPath)
        {
            //Create the data set and table
            DataSet ds = new DataSet("New_DataSet");
            DataTable dt = new DataTable("New_DataTable");

            //Set the locale for each
            ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
            dt.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

            //Open a DB connection (in this example with OleDB)
            using (SqlConnection con = new SqlConnection(new SqlDataProvider().connectionString("me")))
            {
                con.Open();

                //Create a query and fill the data table with the data from the DB
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 0;
                SqlDataAdapter adptr = new SqlDataAdapter();

                adptr.SelectCommand = cmd;
                adptr.Fill(dt);
                con.Close();

                //Here's the easy part. Create the Excel worksheet from the data set
                dt.ToCSV(filPath);
            }
        }

        public static string GenerateDigits(Random rng, int length)
        {
            char[] chArray = new char[length];
            int i = 0;
            for (i = 0; i <= length - 1; i++)
            {
                chArray[i] = Convert.ToChar(rng.Next(10) + 0x30);
            }
            return new string(chArray);
        }

        public static Hashtable GetConfig()
        {
            return DataService.Provider.GetConfig();
        }

        public static void getModulesinRole(string p, Repeater ModuleList)
        {
            List<Models.Module> modules = DataService.Provider.GetModulesInRole(p);
            if (modules != null) //&& (modules != string.Empty) && (modules.Length > 0))
            {
                //string[] arrs = modules.Split(',');
                foreach (Models.Module i in modules)
                {
                    for (int j = 0; j < ModuleList.Items.Count; j++)
                    {
                        Label lblModuleID = ((Label)ModuleList.Items[j].FindControl("lblModuleID"));
                        if (i.ModuleID == lblModuleID.Text)
                        {
                            CheckBox SelectTime = ((CheckBox)ModuleList.Items[j].FindControl("chkSelected"));
                            SelectTime.Checked = true;
                            break;
                        }
                    }
                }
            }
        }

        public static void getModulesPrivForRole(string p, Repeater ModuleList)
        {
            List<Models.Module> modules = DataService.Provider.GetModulesInRole(p);
            if (modules != null)
            {
                // if (modules.Contains(","))
                //{
                //string[] arrs = modules.Split(',');

                for (int j = 0; j < ModuleList.Items.Count; j++)
                {
                    Label lblModuleID = ((Label)ModuleList.Items[j].FindControl("lblModuleID"));

                    foreach (Models.Module i in modules)
                    {
                        if (i.ModuleID == lblModuleID.Text)
                        {
                            CheckBox SelectTime = ((CheckBox)ModuleList.Items[j].FindControl("chkSelected"));
                            SelectTime.Checked = true;
                            break;
                        }
                        else
                        {
                            CheckBox SelectTime = ((CheckBox)ModuleList.Items[j].FindControl("chkSelected"));
                            SelectTime.Checked = false;
                            SelectTime.Enabled = false;
                            CheckBox chkadd = ((CheckBox)ModuleList.Items[j].FindControl("chkadd"));
                            chkadd.Checked = false;
                            chkadd.Enabled = false;
                            CheckBox chkedit = ((CheckBox)ModuleList.Items[j].FindControl("chkedit"));
                            chkedit.Checked = false;
                            chkedit.Enabled = false;
                            CheckBox chkview = ((CheckBox)ModuleList.Items[j].FindControl("chkview"));
                            chkview.Checked = false;
                            chkview.Enabled = false;
                            CheckBox chkdelete = ((CheckBox)ModuleList.Items[j].FindControl("chkdelete"));
                            chkdelete.Checked = false;
                            chkdelete.Enabled = false;
                        }
                    }
                }
            }
        }

        public static void GetPermUserDetail(string usename)
        {
            HttpContext.Current.Session["UserRoleName"] = DataService.Provider.getUserRoleByUserName(usename);
            if (HttpContext.Current.Session["UserRoleName"] != null)
            {
                List<Models.Module> modulelist = DataService.Provider.GetModulesInRole(HttpContext.Current.Session["UserRoleName"].ToString());
                Hashtable modules = new Hashtable();
                if (modulelist != null)// && modulelist != string.Empty)
                {
                    //string[] strarr = modulelist.Split(',');
                    foreach (Models.Module i in modulelist)
                    {
                        modules[i.ModuleID] = i.ModuleID;
                    }
                    HttpContext.Current.Session["UserModules"] = modules;
                }
                //get module permissions based on role
                List<ModulePermmission> mdperm = DataService.Provider.GetRoleModulePermissions(HttpContext.Current.Session["UserRoleName"].ToString());
                if (mdperm != null)
                {
                    Hashtable permissions = new Hashtable();
                    foreach (ModulePermmission var in mdperm)
                    {
                        permissions[var.ModuleID] = var;
                    }
                    HttpContext.Current.Session["Permissions"] = permissions;
                }
            }
        }

        public static int GetUserID(string username)
        {
            return DataService.Provider.GetUserID(username);
        }

        public static bool HasModule(string moduleid)
        {
            if (HttpContext.Current.Session["UserRoleName"] != null)
            {
                if (HttpContext.Current.Session["UserRoleName"].ToString() != "Administrator")
                {
                    if (HttpContext.Current.Session["UserModules"] != null)
                    {
                        Hashtable dos = (Hashtable)HttpContext.Current.Session["UserModules"];
                        if (dos.ContainsKey(moduleid)) { return true; }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public static Hashtable ListItemToHashtable(ListItemCollection listItemCollection)
        {
            Hashtable ds = new Hashtable();
            if (listItemCollection != null)
            {
                foreach (ListItem var in listItemCollection)
                {
                    ds[var.Value] = var.Text;
                }
            }
            return ds;
        }

        /*
          int stepdee = int.Parse(checkarr[0]);
                        int stepf = stepdee * 11;
                        int checkdigit = (int)totalvalue - stepf;
         */

        public static System.IO.Stream getAppResource(string filename)
        {
            System.IO.Stream file = null;
            System.Reflection.Assembly thisExe = null;
            thisExe = System.Reflection.Assembly.GetCallingAssembly();
            string[] resourceFiles = thisExe.GetManifestResourceNames();
            string filo = filename;
            string matchedFile = (from c in resourceFiles where c.Contains(filo) select c).FirstOrDefault();
            file = thisExe.GetManifestResourceStream(matchedFile);
            return file;
        }

        public static string GetSQLString(string strProc, object[] Parameters)
        {
            try
            {
                string Ret = null;
                string strMsg = "";
                System.IO.Stream file = getAppResource(strProc + ".txt");
                string strSQL = "";
                if (file == null)
                {
                    strMsg = "SQL File - " + strProc + " does not exist";
                    // m_ParentAddon.SboApplication.SetStatusBarMessage(strMsg, SAPbouiCOM.BoMessageTime.bmt_Medium, true);
                    return "";
                }
                System.IO.StreamReader SReader = new System.IO.StreamReader(file);

                strSQL = SReader.ReadToEnd();

                int i = 0;
                string strHolder = "OWAPARAM";
                int length = 0;
                length = Parameters.Length;
                if (length > 0)
                {
                    for (i = 0; i <= length - 1; i++)
                    {
                        strSQL = strSQL.Replace(strHolder + (i + 1).ToString(), Parameters[i].ToString());
                    }
                }
                Ret = strSQL;
                return Ret;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static int MonthDiff(DateTime d1, DateTime d2)
        {
            int retVal = 0;

            if (d1.Month < d2.Month)
            {
                retVal = (d1.Month + 12) - d2.Month;
                retVal += ((d1.Year - 1) - d2.Year) * 12;
            }
            else
            {
                retVal = d1.Month - d2.Month;
                retVal += (d1.Year - d2.Year) * 12;
            }
            //// Calculate the number of years represented and multiply by 12
            //// Substract the month number from the total
            //// Substract the difference of the second month and 12 from the total
            //retVal = (d1.Year - d2.Year) * 12;
            //retVal = retVal - d1.Month;
            //retVal = retVal - (12 - d2.Month);

            return retVal;
        }

        public static void ResizeMemberImage(string OriginalFile, string NewFile, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
        {
            try
            {
                NewWidth = 480;
                MaxHeight = 480;
                System.Drawing.Image FullsizeImage = System.Drawing.Image.FromFile(OriginalFile);

                //Prevent using images internal thumbnail
                FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

                if (OnlyResizeIfWider)
                {
                    if (FullsizeImage.Width <= NewWidth)
                    {
                        NewWidth = FullsizeImage.Width;
                    }
                }

                int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
                if (NewHeight > MaxHeight)
                {
                    // Resize with height instead
                    NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
                    NewHeight = MaxHeight;
                }

                System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

                // Clear handle to original file so that we can overwrite it if necessary
                FullsizeImage.Dispose();

                // Save resized picture
                NewImage.Save(NewFile);
            }
            catch (Exception)
            {
            }
        }

        public static void ResizeImage(string OriginalFile, string NewFile, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
        {
            try
            {
                NewWidth = 480;
                MaxHeight = 480;
                System.Drawing.Image FullsizeImage = System.Drawing.Image.FromFile(OriginalFile);

                //Prevent using images internal thumbnail
                FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

                if (OnlyResizeIfWider)
                {
                    if (FullsizeImage.Width <= NewWidth)
                    {
                        NewWidth = FullsizeImage.Width;
                    }
                }

                int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
                if (NewHeight > MaxHeight)
                {
                    // Resize with height instead
                    NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
                    NewHeight = MaxHeight;
                }

                System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

                // Clear handle to original file so that we can overwrite it if necessary
                FullsizeImage.Dispose();

                // Save resized picture
                NewImage.Save(NewFile);
            }
            catch (Exception)
            {
            }
        }

        public static void SendMyPassword(string Username)
        {
            User curr = DataService.Provider.getUserDetailByUsername(Username);
            if (curr != null)
            {
                string L_Name = curr.LastName + " " + curr.FirstName;
                if (string.IsNullOrEmpty(curr.Text_Pass))
                {
                    string new_p = DateTime.Now.ToString("yyyyMMdd") + curr.UserId;
                    string enc_p = PortalSecurity.Encrypt(new_p);
                    int result = DataService.Provider.UpdatePassword(Username, curr.UserId, enc_p, new_p);
                    sendPass(Username, new_p, L_Name);
                    // generate and save pass and send user a copy
                }
                else
                {
                    sendPass(Username, curr.Text_Pass, L_Name);
                }
            }
        }

        public static void SendSubmitEmail(string UserName, string job_title, string company_name, User user)
        {
            string Name = user.LastName + " " + user.FirstName;
            string cypher = PortalSecurity.Encrypt("Activate");
            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            try
            {
                m.From = new MailAddress(From, SenderCompany);
                m.To.Add(new MailAddress(user.UserName, Name));
                //similarly BCC
                m.Subject = "Account Creation Successful";
                m.IsBodyHtml = true;
                m.Body = string.Format(
                @"
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <link href='{5}mail.css' type='text/css' rel='stylesheet' />
    <title></title>
</head>
<body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
    <center>
            <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='backgroundTable'>
                <tr>
                    <td align='center' valign='top'>
                        <table border='0' cellpadding='10' cellspacing='0' width='600' id='templatePreheader'>
                            <tr>
                                <td valign='top' class='preheaderContent'>

                                    <table border='0' cellpadding='10' cellspacing='0' width='100%'>
                                        <tr>
                                            <td valign='top'>
                                                &nbsp;</td>
                                            <td valign='top' width='190'>
                                                &nbsp;</td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                        </table>
                        <table border='0' cellpadding='0' cellspacing='0' width='600' id='templateContainer'>
                            <tr>
                                <td align='center' valign='top'>
                                    <table border='0' cellpadding='0' cellspacing='0' width='600' id='templateHeader'>
                                        <tr>
                                            <td class='headerContent'>

                                                <img src='{5}/logo.gif'
                                                    style='max-width:100%; float: left;' id='headerImage campaign-icon'
                                                    mc:label='header_image' mc:edit='header_image' mc:allowdesigner mc:allowtext />

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align='center' valign='top'>
                                    <table border='0' cellpadding='0' cellspacing='0' width='600' id='templateBody'>
                                        <tr>
                                            <td valign='top' class='bodyContent'>

                                                 <table border='0' cellpadding='20' cellspacing='0' width='100%'>
                                                                    <tr>
                                                                        <td valign='top'>
                                                                            <div mc:edit='std_content00'>
                                                                                <h5 class='h5'>Successful Submission of Application</h5>
                                                                                 <h5 class='h5'> Dear {0},</h5>
                                                                               <p> Thank you for applying for {1} role in the {2} Recruitment exercise. <br/> This email serve as your confirmation
                                                                                    that we have received your application.<br />
                                                                                    Please note that only shortlisted candidates will be invited for further assessment.<br />
                                                                                    Thank you.
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align='center' valign='top'>
                                    <table border='0' cellpadding='10' cellspacing='0' width='600' id='templateFooter'>
                                        <tr>
                                            <td valign='top' class='footerContent'>

                                                <table border='0' cellpadding='10' cellspacing='0' width='100%'>
                                                    <tr>
                                                        <td colspan='2' valign='middle' id='social'>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td valign='top' width='350'>
                                                            <div mc:edit='std_footer'>
                                                                <span class='bodyContent'
                                                                    style='color: rgb(6, 41, 3); font-family: Arial, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 18px; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;'>
                                                                &copy; {3} &nbsp; {4}. All rights reserved</span><em><span
                                                                    class='bodyContent'>.</span></em>
                                                                <br />
                                                            </div>
                                                        </td>
                                                        <td valign='top' width='190' id='monkeyRewards'>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan='2' valign='middle' id='utility'>
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </td>
                </tr>
            </table>
        </center>
</body>
</html>

                ", Name, job_title, company_name, DateTime.Now.Year.ToString(), SenderCompany, SenderUrl);
                sc.Host = Util.Host;
                sc.Port = int.Parse(Util.EmailSenderPort);
                sc.Credentials = new System.Net.NetworkCredential(Util.From, Util.Password);
                sc.EnableSsl = Convert.ToBoolean(int.Parse(Util.EmailSenderEnableSsl));
                sc.Send(m);
            }
            catch (Exception ex)
            {
            }
        }

        public static bool StatementofCapability(int p)
        {
            return false;
        }

        public static int UpdateTansactionCount(int seed)
        {
            return 0;
        }

        public static bool WorkHistory(int p)
        {
            // to do
            return true;
        }

        public static bool CheckIfEmailExists(string p)
        {
            return DataService.Provider.CheckIfEmailExists(p);
        }

        private static void sendPass(string Username, string new_p, string Name)
        {
            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            try
            {
                m.From = new MailAddress(From, Util.MailTitle);
                m.To.Add(new MailAddress(Username, Name));
                //similarly BCC
                m.Subject = "Account Information Recovery";
                m.IsBodyHtml = true;
                m.Body = string.Format(
                @"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
                    <link href='{4}mail.css' type='text/css' rel='stylesheet' />
                    <title></title>
                </head>
                <body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0' offset='0'>
                    <center>
                            <table border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' id='backgroundTable'>
                                <tr>
                                    <td align='center' valign='top'>
                                        <table border='0' cellpadding='10' cellspacing='0' width='600' id='templatePreheader'>
                                            <tr>
                                                <td valign='top' class='preheaderContent'>

                                                    <table border='0' cellpadding='10' cellspacing='0' width='100%'>
                                                        <tr>
                                                            <td valign='top'>
                                                                &nbsp;</td>
                                                            <td valign='top' width='190'>
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>

                                                </td>
                                            </tr>
                                        </table>
                                        <table border='0' cellpadding='0' cellspacing='0' width='600' id='templateContainer'>
                                            <tr>
                                                <td align='center' valign='top'>
                                                    <table border='0' cellpadding='0' cellspacing='0' width='600' id='templateHeader'>
                                                        <tr>
                                                            <td class='headerContent'>

                                                                <img src='{4}logo.gif'
                                                                    style='max-width:100%; float: left;' id='headerImage campaign-icon'
                                                                    mc:label='header_image' mc:edit='header_image' mc:allowdesigner mc:allowtext />

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='center' valign='top'>
                                                    <table border='0' cellpadding='0' cellspacing='0' width='600' id='templateBody'>
                                                        <tr>
                                                            <td valign='top' class='bodyContent'>

                                                                 <table border='0' cellpadding='20' cellspacing='0' width='100%'>
                                                                                    <tr>
                                                                                        <td valign='top'>
                                                                                            <div mc:edit='std_content00'>
                                                                                                 <h5 class='h5'> Dear {0},</h5>
                                                                                               <p>  Your login details are as follows;</p>
                                                                                                     <ul>
                                                                                                          <li>Username: {1}</li>
                                                                                                          <li>Password: {2}</li>
                                                                                                    </ul>
                                                                                                    Thank you.
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='center' valign='top'>
                                                    <table border='0' cellpadding='10' cellspacing='0' width='600' id='templateFooter'>
                                                        <tr>
                                                            <td valign='top' class='footerContent'>

                                                                <table border='0' cellpadding='10' cellspacing='0' width='100%'>
                                                                    <tr>
                                                                        <td colspan='2' valign='middle' id='social'>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign='top' width='350'>
                                                                            <div mc:edit='std_footer'>
                                                                                <span class='bodyContent'
                                                                                    style='color: rgb(6, 41, 3); font-family: Arial, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 18px; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;'>
                                                                                &copy; {3} &nbsp; {5}. All rights reserved</span><em><span
                                                                                    class='bodyContent'>.</span></em>
                                                                                <br />
                                                                            </div>
                                                                        </td>
                                                                        <td valign='top' width='190' id='monkeyRewards'>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan='2' valign='middle' id='utility'>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                </table>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </center>
                </body>
                </html>

                ", Name, Username, new_p, DateTime.Now.Year.ToString(), SenderUrl, SenderCompany);
                sc.Host = Util.Host;
                sc.Port = int.Parse(Util.EmailSenderPort);
                sc.Credentials = new System.Net.NetworkCredential(Util.From, Util.Password);
                sc.EnableSsl = Convert.ToBoolean(int.Parse(Util.EmailSenderEnableSsl));
                sc.Send(m);
            }
            catch (Exception ex)
            {
            }
        }

        public static Hashtable GroupActions(List<ModulesAction> moduleactions)
        {
            Hashtable ht = new Hashtable();
            return ht;
        }

        //public static Hashtable ListItemToHashtable(List<PageControl> list)
        //{
        //    Hashtable ds = new Hashtable();
        //    if (list != null)
        //    {
        //        foreach (PageControl var in list)
        //        {
        //            ds[var.Name] = var.Src;
        //        }
        //    }
        //    return ds;
        //}

        public static string AdminUrl
        {
            get
            {
                return Util.BaseSiteUrl + "a.aspx?p=";
            }
        }

        public static string WebUrl
        {
            get
            {
                return Util.BaseSiteUrl + "p.aspx?p=";
            }
        }

        public static bool UserExist(string username)
        {
            return false;
        }

        public static string Word3Parser(string wordtoParse)
        {
            if (!string.IsNullOrEmpty(wordtoParse))
            {
                string[] words = wordtoParse.Split(' ');
                StringBuilder sb = new StringBuilder();
                sb.Append("<i>");
                int count = 1;
                foreach (string item in words)
                {
                    sb.Append(item); sb.Append(" ");
                    count++;
                    if (count == 4)
                    {
                        sb.Append("</i><br /><i>");
                        count = 1;
                    }
                }
                sb.Append("</i>");
                return (sb.ToString().Replace("<i></i>", "")).Replace("<i> </i>", "");
            }
            return string.Empty;
        }

        public static int NotifyPrincipals(List<DataProviders.Enrollee2> completed)
        {
            foreach (var item in completed)
            {
                if (item.Email != string.Empty)
                {
                    if (Util.CheckIfEmailExists(item.Email))
                    {
                        Util.SendMyPassword(item.Email);
                    }
                }
            }
            return 1;
        }

        public static void ExporttoExcelFromSQL(string sql, string filePath)
        {
            //Create the data set and table
            DataSet ds = new DataSet("New_DataSet");
            DataTable dt = new DataTable("New_DataTable");

            //Set the locale for each
            ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
            dt.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

            //Open a DB connection (in this example with OleDB)
            using (SqlConnection con = new SqlConnection(new SqlDataProvider().connectionString("me")))
            {
                con.Open();

                //Create a query and fill the data table with the data from the DB
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandTimeout = 0;
                SqlDataAdapter adptr = new SqlDataAdapter();

                adptr.SelectCommand = cmd;
                adptr.Fill(dt);
                con.Close();

                //Add the table to the data set
                ds.Tables.Add(dt);

                //Here's the easy part. Create the Excel worksheet from the data set
                Util.CreateWorkbook(filePath, ds);
            }
        }

        public static void CreateWorkbook(String filePath, DataSet dataset)
        {
            if (dataset.Tables.Count == 0)
                throw new ArgumentException("DataSet needs to have at least one DataTable", "dataset");

            Workbook workbook = new Workbook();
            foreach (DataTable dt in dataset.Tables)
            {
                Worksheet worksheet = new Worksheet(dt.TableName);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    // Add column header
                    worksheet.Cells[0, i] = new Cell(dt.Columns[i].ColumnName);

                    // Populate row data
                    for (int j = 0; j < dt.Rows.Count; j++)
                        //See here??
                        worksheet.Cells[j + 1, i] = new Cell(dt.Rows[j][i] == DBNull.Value ? "" : dt.Rows[j][i].ToString());
                }
                workbook.Worksheets.Add(worksheet);
            }
            workbook.Save(filePath);
        }

        public static int SubmitToHMO(List<DataProviders.Enrollee2> completed)
        {
            foreach (var item in completed)
            {
                //write code to check and submit to hr here
            }
            return 1;
        }
    }

    public static class ListExtensions
    {
        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        public static void ToCSV(this DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}
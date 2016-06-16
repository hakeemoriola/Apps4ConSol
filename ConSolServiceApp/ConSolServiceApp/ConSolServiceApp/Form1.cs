using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace ConSolServiceApp
{
    public partial class Form1 : Form
    {
        //Dim ConnectionString As String = "Data Source=192.168.1.84; Initial Catalog=KeedakLive; User ID=sa; Password=sql2k;"
        private string ConnectionString;

        private string senderemail;
        private string hostaddress;
        private string loginname;
        private string password;
        private string mailsubject;
        private string mailbody;
        private int mailport;
        private string datasource;
        private string catalog;
        private string userid;
        private string baseurlPath;
        private string upwd;
        private string MAILER_ATTACHMENT_FOLDER;

        private void LoadSettings()
        {
            try
            {
                System.IO.StreamReader objReader = new System.IO.StreamReader(Application.StartupPath + "/config.ini");
                string[] baseurl = Regex.Split(objReader.ReadLine(), "==");
                baseurlPath = baseurl[1];
                string[] dsource = Regex.Split(objReader.ReadLine(), "==");
                datasource = dsource[1];
                string[] clog = Regex.Split(objReader.ReadLine(), "==");
                catalog = clog[1];
                string[] usrid = Regex.Split(objReader.ReadLine(), "==");
                userid = usrid[1];
                string[] usrpwd = Regex.Split(objReader.ReadLine(), "==");
                upwd = usrpwd[1];
                ConnectionString = "Data Source=" + datasource + "; Initial Catalog=" + catalog + "; User ID=" + userid + "; Password=" + upwd + ";";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString)
                writeOutput("LoadSettingsError_Log.txt", ex.ToString());
            }
        }

        private void writeOutput(string fname, string fcontent)
        {
            StreamWriter objWriter = new StreamWriter(Application.StartupPath + "\\" + fname, true);
            objWriter.WriteLine(fcontent);
            objWriter.Flush();
            objWriter.Close();
            objWriter = null;
        }
        public Form1()
        {
            InitializeComponent();
            Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSettings();
            ProcessDBs();
        }

        private void ProcessDBs()
        {
            try
            {
                //connect to database
                List<ConSetting> dbs = GetAllConSettings();

                //load connections to databases
                foreach (ConSetting conseting in dbs)
                {
                    ConSetting gb = conseting;
                    string ConStr = string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3};", gb.ServerIP, gb.Database, gb.Username, gb.Password);
                    SqlConnection con = GetConnection(gb);
                    string cur_ConStr = ConStr;

                    DBMetaTable table = GetDBMetaTablesByDbId(gb.Id);
                    string columns = GetColumnsByTableId_DbId(gb.Id, table.Id);
                    string QUERY = "SELECT " + columns + "  FROM " + table.TableName;

                    //work on each database
                    List<DbData> listToPro = GetDbData(QUERY, cur_ConStr);
                    ProcessData(listToPro);



                    gb = null;
                }

                dbs = null;
            }
            catch (Exception ex)
            {
                writeOutput("IDGenerationError_Log.txt", ex.ToString());
            }
            Application.Exit();
        }

        private void ProcessData(List<DbData> listToPro)
        {
            int cnt = 0;
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConnectionString;
                con.Open();
                SqlCommand cmd = null;
                foreach (DbData cur_row in listToPro)
                {
                    cnt++;
                    string[] namesPk = null;
                    //phoneNo
                    string Pno = cur_row.PhoneNo;
                    if (!string.IsNullOrEmpty(Pno))
                    {
                        cmd = new SqlCommand("AddPhone", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                        cmd.Parameters["@PhoneNo"].Value = Pno;
                        cmd.ExecuteNonQuery();
                    }

                    cmd = null;
                    //Name
                    if (cnt == 8) { }
                    string Pname = cur_row.Fullname;
                    if (!string.IsNullOrEmpty(Pname))
                    {

                        if (Pname.Contains(" "))
                        {
                            namesPk = Pname.Split(' ');
                        }
                        else { namesPk = Pname.Split('.'); }
                        int NameLength = namesPk.Length;
                        string title = (namesPk[0].Trim() == "NO") ? "" : namesPk[0].Trim();
                        string gender = "";
                        string surname = "";
                        string middlename = ""; string firstname = "";
                        if (NameLength == 2)
                        {                            
                            if (!string.IsNullOrEmpty(title))
                            {
                                if (title.Contains("COPORAL"))
                                {
                                    title = "COPORAL";
                                    gender = "MALE";
                                }
                                if (title.Contains("MR"))
                                {
                                    title = "MR";
                                    gender = "MALE";
                                }
                                else { gender = "FEMALE"; }
                                surname = namesPk[1].Trim();
                                firstname = "";
                            }
                            else { gender = ""; }
                        }
                        else if (NameLength == 3)
                        {
                            if (!string.IsNullOrEmpty(title))
                            {
                                if (title.Contains("COPORAL"))
                                {
                                    title = "COPORAL";
                                    gender = "MALE";
                                }
                                if (title.Contains("MR"))
                                {
                                    title = "MR";
                                    gender = "MALE";
                                }
                                else { gender = "FEMALE"; }
                            }
                            else { gender = ""; }
                            surname = namesPk[1].Trim();
                            firstname = namesPk[2].Trim();
                        }
                        else if (NameLength == 4)
                        {
                            if (!string.IsNullOrEmpty(title))
                            {
                                if (title.Contains("COPORAL"))
                                {
                                    title = "COPORAL";
                                    gender = "MALE";
                                }
                                if (title.Contains("MR"))
                                {
                                    title = "MR";
                                    gender = "MALE";
                                }
                                else { gender = "FEMALE"; }
                            }
                            else { gender = ""; }
                            surname = namesPk[1].Trim();
                            firstname = namesPk[2].Trim();
                            middlename = namesPk[3].Trim();
                        }
                        else { }

                           

                            cmd = new SqlCommand("AddNames", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                            cmd.Parameters["@PhoneNo"].Value = Pno;
                            cmd.Parameters.Add("@Title", SqlDbType.VarChar);
                            cmd.Parameters["@Title"].Value = title;
                            cmd.Parameters.Add("@Firstname", SqlDbType.VarChar);
                            cmd.Parameters["@Firstname"].Value = "";
                            cmd.Parameters.Add("@Middlename", SqlDbType.VarChar);
                            cmd.Parameters["@Middlename"].Value = "";
                            cmd.Parameters.Add("@Surname", SqlDbType.VarChar);
                            cmd.Parameters["@Surname"].Value = surname;
                            cmd.Parameters.Add("@Gender", SqlDbType.VarChar);
                            cmd.Parameters["@Gender"].Value = gender;
                            cmd.ExecuteNonQuery();
                        
                    }


                    //Address

                    //Customer

                    //Product

                    Pno = null;
                }
            }
        }

        private List<DbData> GetDbData(string QUERY, string con_Str)
        {
            List<DbData> list = new List<DbData>();
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = con_Str;
                    con.Open();
                    SqlCommand cmd = new SqlCommand(QUERY, con);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        DbData curr = new DbData();
                        curr.Fullname = (r["Fullname"] is DBNull) ? String.Empty : (string)r["Fullname"];
                        curr.PhoneNo = (r["PhoneNo"] is DBNull) ? String.Empty : (string)r["PhoneNo"];
                        list.Add(curr);
                    }
                    r.Close();
                }
            }
            catch (Exception generatedExceptionName)
            {
                writeOutput("IDGenerationError_Log.txt", generatedExceptionName.ToString());
            }
            return list;
        }

        private string GetColumnsByTableId_DbId(int DbId, int TableId)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConnectionString;
                con.Open();
                StringBuilder sb = new StringBuilder();
                SqlCommand cmd = new SqlCommand(string.Format("select DBColumn + ' AS ' + BaseDbColumn AS BaseColumn from MatchMetaColumns where TableId={0} and DbId={1}", TableId, DbId), con);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    sb.Append((r["BaseColumn"] is DBNull) ? String.Empty : (string)r["BaseColumn"]);
                    sb.Append(",");
                }
                r.Close();
                return sb.ToString().Remove(sb.ToString().Length - 1, 1);
            }
        }

        private DBMetaTable GetDBMetaTablesByDbId(int DbId)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConnectionString;
                con.Open();
                DBMetaTable curr = null;
                SqlCommand cmd = new SqlCommand(string.Format("SELECT Id, [TableName] FROM [DBMetaTables] where DBId = {0}",DbId), con);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    curr = new DBMetaTable();
                    curr.Id = (r["Id"] is DBNull) ? 0 : int.Parse(r["Id"].ToString());
                    curr.TableName = (r["TableName"] is DBNull) ? String.Empty : (string)r["TableName"];                   
                }
                r.Close();
                return curr;
            }
        }

        private SqlConnection GetConnection(ConSetting gb)
        {
            string ConStr = string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3};", gb.ServerIP, gb.Database, gb.Username, gb.Password);
            SqlConnection con = new SqlConnection();            
                con.ConnectionString = ConStr;
                con.Open();
                if (con.State == ConnectionState.Open)
                    return con;
                else
                    return null;
        }

        private List<ConSetting> GetAllConSettings()
        {
            List<ConSetting> ml = new List<ConSetting>();
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = ConnectionString;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ConSettings where IsActive=1", con);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        ConSetting curr = new ConSetting();
                        curr.Id = (r["Id"] is DBNull) ? 0 : int.Parse(r["Id"].ToString());
                        curr.Database = (r["Database"] is DBNull) ? String.Empty : (string)r["Database"];
                        curr.Username = (r["Username"] is DBNull) ? String.Empty : (string)r["Username"];
                        curr.Password = (r["Password"] is DBNull) ? String.Empty : (string)r["Password"];
                        curr.Port = (r["Port"] is DBNull) ? String.Empty : (string)r["Port"];
                        curr.ProjectName = (r["ProjectName"] is DBNull) ? String.Empty : (string)r["ProjectName"];
                        curr.ServerIP = (r["ServerIP"] is DBNull) ? String.Empty : (string)r["ServerIP"];
                        curr.IsActive = true;
                        ml.Add(curr);
                    }
                    r.Close();
                }
            }
            catch (Exception generatedExceptionName)
            {
                writeOutput("IDGenerationError_Log.txt", generatedExceptionName.ToString());
            }
            return ml;
        }

    }
}

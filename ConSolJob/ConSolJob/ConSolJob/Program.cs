using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading.Tasks;

namespace ConSolJob
{
    class Program
    {
        public static string ConnectionString;
        public static int Db_Count;
        public static string senderemail;
        public static string hostaddress;
        public static string loginname;
        public static string password;
        public static string mailsubject;
        public static string mailbody;
        public static int mailport;
        public static int Current_DbID;
        public static int parsedId;
        public static string datasource;
        public static string catalog;
        public static string userid; 
        public static string baseurlPath;
        public static string upwd;
        public static string MAILER_ATTACHMENT_FOLDER;
        static string StartupPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);


        static void Main(string[] args)
        {
            writeOutput("Service_Log.txt", "Service Called in elapsed! on " + DateTime.Now.ToString());
            RunHarmonizeService();
        }

        private static void RunHarmonizeService()
        {
            writeOutput("Service_Log.txt", "Service Started! on " + DateTime.Now.ToString());

            LoadSettings();
            ProcessDBs();

            writeOutput("Service_Log.txt", "Service Completed! on " + DateTime.Now.ToString());

            RunHarmonizeService();
        }

        private static int GetDbCount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = ConnectionString;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT count(*) FROM ConSettings", con);
                    int totaldb = int.Parse(cmd.ExecuteScalar().ToString());
                    return totaldb;
                }
            }
            catch (Exception generatedExceptionName)
            {
                writeOutput("IDGenerationError_Log.txt", generatedExceptionName.ToString());
            }
            return 0;
        }

        private static void ProcessDBs()
        {
            try
            {
                //connect to database
                // List<ConSetting>
                var dbs = GetAllConSettings().FirstOrDefault();

                //load connections to databases
                // foreach (ConSetting conseting in dbs)
                //  {
                //ConSetting gb = conseting;
                ConSetting gb = dbs;
                string ConStr = string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3};", gb.ServerIP, gb.Database.Remove(gb.Database.Length - 2, 2), gb.Username, gb.Password);
                SqlConnection con = GetConnection(gb);
                string cur_ConStr = ConStr;
                Current_DbID = gb.Id;


                DBMetaTable table = GetDBMetaTablesByDbId(gb.Id);
                string dcolumn = GetUniqueColumnsByTableId_DbId(gb.Id, table.Id);
                string columns = GetColumnsByTableId_DbId(gb.Id, table.Id);
                string QUERY = "SELECT " + dcolumn + columns + "  FROM " + table.TableName;

                //work on each database
                List<DbData> listToPro = GetDbData(QUERY, cur_ConStr);

                writeOutput("Service_Log.txt", "Records loaded to process : " + listToPro.Count.ToString());

                ProcessData(listToPro);

                //set next Active
                SetActive(gb.Id);

                gb = null;
                // }

                dbs = null;
            }
            catch (Exception ex)
            {
                writeOutput("IDGenerationError_Log.txt", ex.ToString());
            }
        }

        private static string GetUniqueColumnsByTableId_DbId(int DbId, int TableId)
        {
            Hashtable ht = null;
            SqlCommand cmd = null;

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConnectionString;
                con.Open();

                ht = new Hashtable();
                cmd = new SqlCommand();
                StringBuilder sb = new StringBuilder();
                cmd = new SqlCommand(string.Format(@"SELECT        c.DBColumn AS UniqueColum
                                                        FROM            MatchMetaColumns AS c INNER JOIN
                                                                                 DBMetaTables AS t ON c.DbId = t.DBId INNER JOIN
                                                                                 ConSettings AS d ON c.DbId = d.Id
                                                        WHERE        (c.BaseDbColumn = 'PhoneNo') AND (t.Id = {0})", TableId), con);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    sb.Append(" DISTINCT ");
                    sb.Append((r["UniqueColum"] is DBNull) ? string.Empty : (string)r["UniqueColum"]);
                    sb.Append(",");
                }
                r.Close();
                return sb.ToString().Remove(sb.ToString().Length - 1, 1) + ", ";
            }
        }

        private static void SetActive(int id)
        {
            int TOTAL_DB = 0, CURRENT_DB_ID = id;
            TOTAL_DB = GetDbCount();

            if (TOTAL_DB > CURRENT_DB_ID)
            {
                MakeActive(++CURRENT_DB_ID);
            }
            else if (TOTAL_DB == CURRENT_DB_ID)
            {
                MakeActive(1);
            }
            else { MakeActive(1); }
        }

        private static void MakeActive(int v)
        {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConnectionString;
                con.Open();
                SqlCommand cmd = null;
                string SQL = string.Format(@" 
                                              update ConSettings set IsActive = 1 where id={0}
                                              update ConSettings set IsActive = 0 where id<>{0}
                                              update vxNames set fullname = '' where fullname = 'NO NAME'
                                              UPDATE VxTelephones SET TPhoneNumber = RIGHT(REPLICATE('0', 11) + CAST(PhoneNo AS VARCHAR(11)), 11),
                                              PrefixNo = LEFT(RIGHT(REPLICATE('0', 11) + CAST(PhoneNo AS VARCHAR(11)), 11),4)
                                            ", v);
                cmd = new SqlCommand(SQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        private static void ProcessData(List<DbData> listToPro) {
            int just = 0;
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConnectionString;
                con.Open();
                SqlCommand cmd = null;
                foreach (DbData cur_row in listToPro)
                {
                    ++just;
                    string[] namesPk = null;

                    //phoneNo
                    string Pno = cur_row.PhoneNo;
                    if (!string.IsNullOrEmpty(Pno))
                    {
                        cmd = new SqlCommand("AddPhone", con);
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                        cmd.Parameters["@PhoneNo"].Value = Pno;
                        cmd.ExecuteNonQuery();
                    }

                    cmd = null;

                    //Name
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


                        string phoneno = cur_row.PhoneNo;
                        if (!string.IsNullOrEmpty(phoneno) && !string.IsNullOrEmpty(Pno))
                        {
                            cmd = new SqlCommand("AddNames", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                            cmd.Parameters["@PhoneNo"].Value = phoneno;
                            cmd.Parameters.Add("@Title", SqlDbType.VarChar);
                            cmd.Parameters["@Title"].Value = title;
                            cmd.Parameters.Add("@fullName", SqlDbType.VarChar);
                            cmd.Parameters["@fullName"].Value = Pname;
                            cmd.Parameters.Add("@Gender", SqlDbType.VarChar);
                            cmd.Parameters["@Gender"].Value = gender;
                            cmd.ExecuteNonQuery();
                        }
                    }


                    //Address
                    string Paddress = cur_row.Address;
                    if (!string.IsNullOrEmpty(Paddress) && !string.IsNullOrEmpty(Pno))
                    {
                        cmd = new SqlCommand("AddAddress", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                        cmd.Parameters["@PhoneNo"].Value = Pno;
                        cmd.Parameters.Add("@Address", SqlDbType.VarChar);
                        cmd.Parameters["@Address"].Value = Paddress;
                        cmd.ExecuteNonQuery();
                    }

                    cmd = null;

                    //Age
                    string pAge = cur_row.Age;
                    if (!string.IsNullOrEmpty(pAge) && !string.IsNullOrEmpty(Pno))
                    {
                        cmd = new SqlCommand("AddAge", con);
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                        cmd.Parameters["@PhoneNo"].Value = Pno;
                        cmd.Parameters.Add("@Age", SqlDbType.VarChar);
                        cmd.Parameters["@Age"].Value = pAge;
                        cmd.ExecuteNonQuery();
                    }

                    cmd = null;

                    //DateOfBirth
                    string pDateOfBirth = cur_row.DateOfBirth;
                    if (!string.IsNullOrEmpty(pDateOfBirth) && !string.IsNullOrEmpty(Pno))
                    {
                        cmd = new SqlCommand("AddDateOfBirth", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                        cmd.Parameters["@PhoneNo"].Value = Pno;
                        cmd.Parameters.Add("@DateOfBirth", SqlDbType.VarChar);
                        cmd.Parameters["@DateOfBirth"].Value = pDateOfBirth;
                        cmd.ExecuteNonQuery();
                    }
                    cmd = null;


                    //occupation
                    string pOccupation = cur_row.Occupation;
                    if (!string.IsNullOrEmpty(pOccupation) && !string.IsNullOrEmpty(Pno))
                    {
                        cmd = new SqlCommand("AddOccupation", con);
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                        cmd.Parameters["@PhoneNo"].Value = Pno;
                        cmd.Parameters.Add("@Occupation", SqlDbType.VarChar);
                        cmd.Parameters["@Occupation"].Value = pOccupation;
                        cmd.ExecuteNonQuery();
                    }

                    cmd = null;

                    //EmploymentStatus
                    string pEmploymentStatus = cur_row.EmploymentStatus;
                    if (!string.IsNullOrEmpty(pEmploymentStatus) && !string.IsNullOrEmpty(Pno))
                    {
                        cmd = new SqlCommand("AddEmploymentStatus", con);
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                        cmd.Parameters["@PhoneNo"].Value = Pno;
                        cmd.Parameters.Add("@EmploymentStatus", SqlDbType.VarChar);
                        cmd.Parameters["@EmploymentStatus"].Value = pEmploymentStatus;
                        cmd.ExecuteNonQuery();
                    }

                    cmd = null;

                    //State
                    string pState = cur_row.State;
                    if (!string.IsNullOrEmpty(pState) && !string.IsNullOrEmpty(Pno))
                    {
                        cmd = new SqlCommand("AddState", con);
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                        cmd.Parameters["@PhoneNo"].Value = Pno;
                        cmd.Parameters.Add("@State", SqlDbType.VarChar);
                        cmd.Parameters["@State"].Value = pState;
                        cmd.ExecuteNonQuery();
                    }

                    cmd = null;

                    //State1
                    string pState1 = cur_row.State1;
                    if (!string.IsNullOrEmpty(pState1) && !string.IsNullOrEmpty(Pno))
                    {
                        cmd = new SqlCommand("AddState1", con);
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                        cmd.Parameters["@PhoneNo"].Value = Pno;
                        cmd.Parameters.Add("@State1", SqlDbType.VarChar);
                        cmd.Parameters["@State1"].Value = pState1;
                        cmd.ExecuteNonQuery();
                    }

                    cmd = null;

                    //Town
                    string pTown = cur_row.Town;
                    if (!string.IsNullOrEmpty(pTown) && !string.IsNullOrEmpty(Pno))
                    {
                        cmd = new SqlCommand("AddTown", con);
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                        cmd.Parameters["@PhoneNo"].Value = Pno;
                        cmd.Parameters.Add("@Town", SqlDbType.VarChar);
                        cmd.Parameters["@Town"].Value = pTown;
                        cmd.ExecuteNonQuery();
                    }

                    cmd = null;

                    //Industry
                    string pIndustry = cur_row.Industry;
                    if (!string.IsNullOrEmpty(pIndustry) && !string.IsNullOrEmpty(Pno))
                    {
                        cmd = new SqlCommand("AddIndustry", con);
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                        cmd.Parameters["@PhoneNo"].Value = Pno;
                        cmd.Parameters.Add("@Industry", SqlDbType.VarChar);
                        cmd.Parameters["@Industry"].Value = pIndustry;
                        cmd.ExecuteNonQuery();
                    }

                    cmd = null;


                    //Email
                    string pEmail = cur_row.Email;
                    if (!string.IsNullOrEmpty(pEmail) && !string.IsNullOrEmpty(Pno))
                    {
                        cmd = new SqlCommand("AddEmail", con);
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                        cmd.Parameters["@PhoneNo"].Value = Pno;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar);
                        cmd.Parameters["@Email"].Value = pEmail;
                        cmd.ExecuteNonQuery();
                    }

                    cmd = null;



                    //Location
                    string pLocation = cur_row.Location;
                    if (!string.IsNullOrEmpty(pLocation) && !string.IsNullOrEmpty(Pno))
                    {
                        cmd = new SqlCommand("AddLocation", con);
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                        cmd.Parameters["@PhoneNo"].Value = Pno;
                        cmd.Parameters.Add("@Location", SqlDbType.VarChar);
                        cmd.Parameters["@Location"].Value = pLocation;
                        cmd.ExecuteNonQuery();
                    }

                    cmd = null;


                    //Region
                    string pRegion = cur_row.Region;
                    if (!string.IsNullOrEmpty(pRegion) && !string.IsNullOrEmpty(Pno))
                    {
                        cmd = new SqlCommand("AddRegion", con);
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                        cmd.Parameters["@PhoneNo"].Value = Pno;
                        cmd.Parameters.Add("@Region", SqlDbType.VarChar);
                        cmd.Parameters["@Region"].Value = pRegion;
                        cmd.ExecuteNonQuery();
                    }

                    cmd = null;


                    //Database
                    string pDatabase = Current_DbID.ToString();
                    if (!string.IsNullOrEmpty(pDatabase) && !string.IsNullOrEmpty(Pno))
                    {
                        cmd = new SqlCommand("AddDatabase", con);
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar);
                        cmd.Parameters["@PhoneNo"].Value = Pno;
                        cmd.Parameters.Add("@DbSource", SqlDbType.VarChar);
                        cmd.Parameters["@DbSource"].Value = pDatabase;
                        cmd.ExecuteNonQuery();
                    }

                    cmd = null;

                    //Product

                    Pno = null;
                }
            }
        }

        private static List<DbData> GetDbData(string QUERY, string con_Str)
        {
            List<DbData> list = new List<DbData>();
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = con_Str;
                    con.Open();
                    SqlCommand cmd = new SqlCommand(QUERY, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 0;
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        DbData curr = new DbData();
                        curr.Fullname = (r["Fullname"] is DBNull) ? string.Empty : (string)r["Fullname"].ToString();
                        curr.PhoneNo = (r["PhoneNo"] is DBNull) ? string.Empty : (string)r["PhoneNo"].ToString();
                        curr.Address = (r["Address"] is DBNull) ? string.Empty : (string)r["Address"].ToString();
                        curr.Email = (r["Email"] is DBNull) ? string.Empty : (string)r["Email"].ToString();
                        curr.Industry = (r["Industry"] is DBNull) ? string.Empty : (string)r["Industry"].ToString();
                        curr.Occupation = (r["Occupation"] is DBNull) ? string.Empty : (string)r["Occupation"].ToString();
                        curr.State = (r["State"] is DBNull) ? string.Empty : (string)r["State"].ToString();
                        curr.State1 = (r["State1"] is DBNull) ? string.Empty : (string)r["State1"].ToString();
                        curr.Location = (r["Location"] is DBNull) ? string.Empty : (string)r["Location"].ToString();
                        curr.Region = (r["Region"] is DBNull) ? string.Empty : (string)r["Region"].ToString();
                        curr.DateOfBirth = (r["DateOfBirth"] is DBNull) ? string.Empty : (string)r["DateOfBirth"].ToString();
                        curr.Age = (r["Age"] is DBNull) ? string.Empty : (string)r["Age"].ToString();
                        curr.EmploymentStatus = (r["EmploymentStatus"] is DBNull) ? string.Empty : (string)r["EmploymentStatus"].ToString();
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

        private static string GetColumnsByTableId_DbId(int DbId, int TableId)
        {
            Hashtable ht = null;
            SqlCommand cmd = null;
            string OtherColumns = GetOtherFeilds(DbId, TableId);

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConnectionString;
                con.Open();

                ht = new Hashtable();
                cmd = new SqlCommand();
                StringBuilder sb = new StringBuilder();
                cmd = new SqlCommand(string.Format("select DBColumn + ' AS ' + BaseDbColumn AS BaseColumn from MatchMetaColumns where TableId={0} and DbId={1}", TableId, DbId), con);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    sb.Append((r["BaseColumn"] is DBNull) ? string.Empty : (string)r["BaseColumn"]);
                    sb.Append(",");
                }
                r.Close();
                return sb.ToString().Remove(sb.ToString().Length - 1, 1) + ", " + OtherColumns;
            }
        }

        private static string GetOtherFeilds(int DbId, int TableId)
        {
            StringBuilder sb = new StringBuilder();
            Hashtable ht = new Hashtable();
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand("",con);
                cmd.CommandText = string.Format(@"Select BaseColumnName BaseColumn from
                                                  BaseMetaColumns
                                                  where BaseColumnName not in (SELECT BaseDbColumn FROM MatchMetaColumns where DbId={0} and TableId={1})
                                                  order by BaseColumnName", DbId, TableId);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    ht.Add((r["BaseColumn"] is DBNull) ? string.Empty : (string)r["BaseColumn"], (r["BaseColumn"] is DBNull) ? string.Empty : (string)r["BaseColumn"]);
                }
                r.Close();
            }
            foreach (string item in ht.Keys)
            {
                sb.Append(" '' AS " + item + ",");
            }
            return sb.ToString().Remove(sb.ToString().Length - 1, 1);
        }

   
        private static DBMetaTable GetDBMetaTablesByDbId(int DbId)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConnectionString;
                con.Open();
                DBMetaTable curr = null;
                SqlCommand cmd = new SqlCommand(string.Format("SELECT Id, [TableName] FROM [DBMetaTables] where DBId = {0}", DbId), con);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    curr = new DBMetaTable();
                    curr.Id = (r["Id"] is DBNull) ? 0 : int.Parse(r["Id"].ToString());
                    curr.TableName = (r["TableName"] is DBNull) ? string.Empty : (string)r["TableName"];
                }
                r.Close();
                return curr;
            }
        }

        private static SqlConnection GetConnection(ConSetting gb)
        {
            string ConStr = string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3};", gb.ServerIP, gb.Database.Remove(gb.Database.Length - 2, 2), gb.Username, gb.Password);
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConStr;
            con.Open();
            if (con.State == ConnectionState.Open)
                return con;
            else
                return null;
        }

        private static List<ConSetting> GetAllConSettings()
        {
            List<ConSetting> ml = new List<ConSetting>();
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = ConnectionString;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("", con);
                    cmd.CommandText = "SELECT * FROM ConSettings where IsActive=1";
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        ConSetting curr = new ConSetting();
                        curr.Id = (r["Id"] is DBNull) ? 0 : int.Parse(r["Id"].ToString());
                        curr.Database = (r["Database"] is DBNull) ? string.Empty : (string)r["Database"];
                        curr.Username = (r["Username"] is DBNull) ? string.Empty : (string)r["Username"];
                        curr.Password = (r["Password"] is DBNull) ? string.Empty : (string)r["Password"];
                        curr.Port = (r["Port"] is DBNull) ? string.Empty : (string)r["Port"];
                        curr.ProjectName = (r["ProjectName"] is DBNull) ? string.Empty : (string)r["ProjectName"];
                        curr.ServerIP = (r["ServerIP"] is DBNull) ? string.Empty : (string)r["ServerIP"];
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

        private static void writeOutput(string fname, string fcontent)
        {
            StreamWriter objWriter = new StreamWriter(StartupPath + "\\" + fname, true);
            objWriter.WriteLine(fcontent);
            objWriter.Flush();
            objWriter.Close();
            objWriter = null;
        }
        

        private static void LoadSettings()
        {
            try
            {
                System.IO.StreamReader objReader = new System.IO.StreamReader(StartupPath + "/config.ini");
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
                string[] parsedIdstr = Regex.Split(objReader.ReadLine(), "==");
                parsedId = int.Parse(parsedIdstr[1]);

                ConnectionString = "Data Source=" + datasource + "; Initial Catalog=" + catalog + "; User ID=" + userid + "; Password=" + upwd + ";";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString)
                writeOutput("LoadSettingsError_Log.txt", ex.ToString());
            }
        }


        enum ExitCode : int
        {
            Success = 0,
            InvalidLogin = 1,
            InvalidFilename = 2,
            UnknownError = 10
        }

    }
}

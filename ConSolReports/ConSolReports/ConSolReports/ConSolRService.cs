using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;

namespace ConSolReports
{
    public partial class ConSolRService : ServiceBase
    {
        private System.Timers.Timer timer;

        /*
         * New Approach
         SELECT *
        FROM DataCount
          PIVOT(SUM(ColumnCount)
          FOR ColumnName IN([LGA], [PhoneNo])
          ) AS DataCount;
        */

        public ConSolRService()
        {
            string allotedTime = ConfigurationManager.AppSettings["AllotedTime"];
            Double DAlloted = double.Parse(allotedTime);
            InitializeComponent();
            timer = new System.Timers.Timer(DAlloted);
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(timer_elasped);
        }

        private async void timer_elasped(object sender, ElapsedEventArgs e)
        {
            /*
             * Step 1
             *
             * Get all table data in DataDashBoard and their connection details then run count on each of the tables
             * and save the result of each count in the  TotalRecordCount.
             *
             * Step 2
             * Get all table data in DataDashBoard and their connection details then run unique count on each of the tables
             * and save the result of each count in the  TotalUniqueCount.
             *
             */
            List<Configsetting> list = await GetAllDatabaseSettingAsync();
            if (list != null)
            {
                foreach (var currentdb in list)
                {
                    int TotalDataCount = 0, UniqueDataCount = 0;
                    try
                    {
                        using (SqlConnection con = new SqlConnection())
                        {
                            string ConStr = string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3};", currentdb.ServerIP, currentdb.Database.Remove(currentdb.Database.Length - 2, 2), currentdb.Username, currentdb.Password);
                            con.ConnectionString = ConStr;
                            con.Open();

                            SqlCommand cmd = new SqlCommand("", con);
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = "SELECT count(*) TotalDataCount, count(distinct " + currentdb.DBColumn + ") UniqueDataCount from " + currentdb.TableName;
                            SqlDataReader r = cmd.ExecuteReader();

                            while (r.Read())
                            {
                                TotalDataCount = (r["TotalDataCount"] is DBNull) ? 0 : int.Parse(r["TotalDataCount"].ToString());
                                UniqueDataCount = (r["UniqueDataCount"] is DBNull) ? 0 : int.Parse(r["UniqueDataCount"].ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        TotalDataCount = 0; UniqueDataCount = 0;
                        writeOutput("Service_Log.txt", ex.ToString());
                    }
                    UpDateDashBoard(currentdb.Id, TotalDataCount, UniqueDataCount);
                }
                writeOutput("Service_Log.txt", "Service Updated Successfully! " + DateTime.Now.ToString());
            }
        }

        private void UpDateDashBoard(int id, int totalDataCount, int uniqueDataCount)
        {
            string SQL = "UPDATE   DataDashBoard SET    TotalRecordCount =" + totalDataCount + ", TotalUniqueCount = " + uniqueDataCount + " where DbId = " + id;
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = "Data Source=(local);Initial Catalog=ConSolHDB;User ID=ConsolHDBUsr; Password=123abc,./;MultipleActiveResultSets=True";
                con.Open();
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandTimeout = 0;
                int result = cmd.ExecuteNonQuery();
                cmd = null;
            }
        }

        private async Task<List<Configsetting>> GetAllDatabaseSettingAsync()
        {
            var result = await Task.Run(() => GetAllDatabaseSetting());
            return result;
        }

        private List<Configsetting> GetAllDatabaseSetting()
        {
            List<Configsetting> ml = new List<Configsetting>();
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = "Data Source=(local);Initial Catalog=ConSolHDB;User ID=ConsolHDBUsr; Password=123abc,./;MultipleActiveResultSets=True";
                    con.Open();
                    SqlCommand cmd = new SqlCommand("", con);
                    cmd.CommandText = @"SELECT DISTINCT a.Id, a.ProjectName, a.IsActive, a.Password, a.Port, a.ServerIP, a.Username, b.TableName, c.DBColumn,a.[Database]
                                        FROM            ConSettings AS a INNER JOIN
                                                                 DBMetaTables AS b ON a.Id = b.DBId
                                        join MatchMetaColumns c on b.Id = c.TableId
                                        WHERE        (a.Id IN  (SELECT        DbId FROM DataDashBoard) and c.IsDColumn=1)";
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        Configsetting curr = new Configsetting();
                        curr.Id = (r["Id"] is DBNull) ? 0 : int.Parse(r["Id"].ToString());
                        curr.Database = (r["Database"] is DBNull) ? string.Empty : (string)r["Database"];
                        curr.Username = (r["Username"] is DBNull) ? string.Empty : (string)r["Username"];
                        curr.Password = (r["Password"] is DBNull) ? string.Empty : (string)r["Password"];
                        curr.Port = (r["Port"] is DBNull) ? string.Empty : (string)r["Port"];
                        curr.ProjectName = (r["ProjectName"] is DBNull) ? string.Empty : (string)r["ProjectName"];
                        curr.ServerIP = (r["ServerIP"] is DBNull) ? string.Empty : (string)r["ServerIP"];
                        curr.TableName = (r["TableName"] is DBNull) ? string.Empty : (string)r["TableName"];
                        curr.DBColumn = (r["DBColumn"] is DBNull) ? string.Empty : (string)r["DBColumn"];
                        curr.IsActive = true;
                        ml.Add(curr);
                    }
                    r.Close();
                }
            }
            catch (Exception ex)
            {
                writeOutput("Service_Log.txt", ex.ToString());
            }
            return ml;
        }

        public static void writeOutput(string fname, string fcontent)
        {
            using (StreamWriter objWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\" + fname, true))
            {
                objWriter.WriteLine(fcontent);
                objWriter.Flush();
                objWriter.Close();
                // objWriter = null;
            }
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                timer.Start();
                writeOutput("Service_Log.txt", "Service Started! on " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                //log anywhere
                writeOutput("Service_Log.txt", ex.ToString());
            }
        }

        protected override void OnStop()
        {
            try
            {
                if (timer != null)
                {
                    timer.Stop();
                    writeOutput("Service_Log.txt", "Service Stopped! on " + DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                //log anywhere
                writeOutput("Service_Log.txt", ex.ToString());
            }
        }
    }
}
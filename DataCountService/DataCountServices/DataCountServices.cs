using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DataCountServices
{
    public partial class DataCountServices : ServiceBase
    {
        private Hashtable columns = new Hashtable();
        private string ConStr = "";

        private System.Timers.Timer timer;

        /*
         *
         * New Approach
         SELECT *
        FROM DataCount
          PIVOT(SUM(ColumnCount)
          FOR ColumnName IN([LGA], [PhoneNo])
          ) AS DataCount;
        */

        public DataCountServices()
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
             * Truncate the Datacount table;
             *
             * Get all table data in DataDashBoard and their connection details then run count on each of the tables
             * and save the result of each count in the  TotalRecordCount.
             *
             * Step 2
             * Get all table data in DataDashBoard and their connection details then run unique count on each of the tables
             * and save the result of each count in the  TotalUniqueCount.
             *
             */
            using (SqlConnection con = new SqlConnection())
            {
                ConStr = ConfigurationManager.AppSettings["DbConnectionString"];
                con.ConnectionString = ConStr;
                con.Open();

                SqlCommand cmd = new SqlCommand("", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"    truncate table DataCount
                                        insert into DataCount(ColumnName,ColumnCount)
                                        select COLUMN_NAME,0 from INFORMATION_SCHEMA.COLUMNS where TABLE_Name='VxDataPoints'
                                        select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_Name='VxDataPoints' and COLUMN_NAME not in ('AddrSet','ADDRESS')
                                   ";
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    if (r["COLUMN_NAME"].ToString() != "ADDRESS" || r["COLUMN_NAME"].ToString() != "AddrSet")
                    {
                        columns[r["COLUMN_NAME"].ToString()] = (r["COLUMN_NAME"] is DBNull) ? "" : r["COLUMN_NAME"].ToString();
                    }
                }
                cmd = null;
                GenerateSQLThenExecute(columns);
            }

            Hashtable columnList = await GetAllColumnListAsync();
            if (columnList != null)
            {
                foreach (string columnData in columnList.Keys)
                {
                    int TotalDataCount = Convert.ToInt32(columnList[columnData].ToString());
                    try
                    {
                        using (SqlConnection con = new SqlConnection())
                        {
                            con.ConnectionString = ConStr;
                            con.Open();

                            SqlCommand cmd = new SqlCommand("", con);
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = string.Format(@"    UPDATE DataCount SET ColumnCount = {1} where ColumnName = '{0}'
                                                                  UPDATE DataCountDash set {0}Count = {1}
                                                             ", columnData, TotalDataCount);
                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        writeOutput("Service_Log.txt", ex.ToString());
                    }

                    writeOutput("Service_Log.txt", "Service Updated Successfully! " + DateTime.Now.ToString());
                }
            }
        }

        private void GenerateSQLThenExecute(Hashtable columns)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DROP TABLE [dbo].[DataCountDash]");
            sb.Append("CREATE TABLE [dbo].[DataCountDash](");
            sb.Append("[Id] [int] IDENTITY(1,1) NOT NULL,");
            foreach (string item in columns.Keys)
            {
                string line = string.Format("[{0}Count] [int] NULL default 0,", item);
                sb.Append(line);
            }
            sb.Append("CONSTRAINT [PK_DataCountDash] PRIMARY KEY CLUSTERED ([Id]))");
            sb.Append("insert into DataCountDash(NAMECount) values(0)");
            string SQL = sb.ToString();
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                con.Open();
                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.CommandTimeout = 0;
                int result = cmd.ExecuteNonQuery();
                cmd = null;
            }
        }

        private async Task<Hashtable> GetAllColumnListAsync()
        {
            var result = await Task.Run(() => GetAllDataAndGetCount());
            return result;
        }

        private async Task<Hashtable> GetAllDataAndGetCount()
        {
            Hashtable ht = new Hashtable();
            foreach (string column in columns.Keys)
            {
                if (!string.IsNullOrEmpty(column))
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection())
                        {
                            con.ConnectionString = ConStr;
                            con.Open();
                            SqlCommand cmd = new SqlCommand("", con);
                            cmd.CommandText = string.Format(@"select count({0}) from VxDataPoints", column);
                            cmd.CommandType = CommandType.Text;
                            object r = await cmd.ExecuteScalarAsync();
                            ht[column] = Convert.ToInt32(r.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        ht[column] = 0;
                        writeOutput("Service_Log.txt", ex.ToString());
                    }
                }
            }
            return ht;
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
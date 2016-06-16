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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace ConSol
{
    public partial class ConSolHarmonizer : ServiceBase
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
        private System.Timers.Timer timer;


        public ConSolHarmonizer()
        {
            string allotedTime = ConfigurationManager.AppSettings["AllotedTime"];
            Double DAlloted = double.Parse(allotedTime);
            InitializeComponent();
            timer = new System.Timers.Timer(DAlloted);
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(timer_elasped);
        }

        private void timer_elasped(object sender, ElapsedEventArgs e)
        {
            Process thisProc = Process.GetCurrentProcess();
            if (IsProcessOpen("ConSolJob.exe") == false)
            {
                string appLine = ConfigurationManager.AppSettings["AppPath"];
                Process.Start(appLine);
            }
            else
            {
                // Check how many total processes have the same name as the current one
                if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1)
                {
                    // If ther is more than one, than it is already running.
                    writeOutput("Service_Log.txt", "Application is already running.");
                    //System.Windows.Application.Current.Shutdown();
                    return;
                }
            }
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        public bool IsProcessOpen(string name)
        {
            Process[] list = Process.GetProcesses();
            foreach (Process clsProcess in list)
            {
                string nameChk = name.Remove(name.Length - 4, 4);
                string ProcessName = clsProcess.ProcessName;
                if (clsProcess.ProcessName == nameChk)
                {
                    return true;
                }
                ProcessName = null;
            }
            return false;
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

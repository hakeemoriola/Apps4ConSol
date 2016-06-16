using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace ConSolReports
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            try
            {
                #if DEBUG
                                ConSolRService service = new ConSolRService();
                                service.OnDebug();
                                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
                #else

                                                                if (Environment.UserInteractive)
                                                                    ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
                                                                else
                                                                    ServiceBase.Run(new ConSolRService());

                                    /*


                                        ServiceBase[] ServicesToRun;
                                        ServicesToRun = new ServiceBase[]
                                        {
                                           new ConSolRService()
                                        };
                                        ServiceBase.Run(ServicesToRun);
                                */
                #endif
            }
            catch (Exception ex)
            {
              ConSolRService.writeOutput("Service_Log.txt", ex.ToString());
            }
        }
    }
}
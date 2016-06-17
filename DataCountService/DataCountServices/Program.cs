using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace DataCountServices
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///

        private static void Main()
        {
            try
            {
#if DEBUG
                DataCountServices service = new DataCountServices();
                service.OnDebug();
                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else

                                                                if (Environment.UserInteractive)
                                                                    ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
                                                                else
                                                                    ServiceBase.Run(new DataCountServices());

#endif
            }
            catch (Exception ex)
            {
                DataCountServices.writeOutput("Service_Log.txt", ex.ToString());
            }
        }
    }
}
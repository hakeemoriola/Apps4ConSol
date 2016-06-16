using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;

namespace ConSol
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            ConSolHarmonizer service = new ConSolHarmonizer();
            service.OnDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
            if (Environment.UserInteractive)
                ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
            else
                ServiceBase.Run(new ConSolHarmonizer());
#endif
        }
    }
}

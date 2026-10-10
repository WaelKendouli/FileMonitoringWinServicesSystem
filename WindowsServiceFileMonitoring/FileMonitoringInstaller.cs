using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace WindowsServiceFileMonitoring
{
    [RunInstaller(true)]
    public partial class FileMonitoringInstaller : System.Configuration.Install.Installer
    {

        private ServiceProcessInstaller serviceProcessInstaller;
        private ServiceInstaller serviceInstaller;
        public FileMonitoringInstaller()
        {
            InitializeComponent();

            // Configure the Service Process Installer
            serviceProcessInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem // Adjust as needed (e.g., NetworkService, LocalService)
            };

            // Configure the Service Installer
            serviceInstaller = new ServiceInstaller
            {
                ServiceName = "FileMonitoringService", // Must match the ServiceName in your ServiceBase class
                DisplayName = "File Monitoring Service",
                StartType = ServiceStartMode.Manual // Or Automatic, depending on requirements
            };

            // Add installers to the installer collection
            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);
        }

    }
}

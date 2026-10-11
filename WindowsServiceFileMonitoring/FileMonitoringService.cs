using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace WindowsServiceFileMonitoring
{
    public partial class FileMonitoringService : ServiceBase
    {
        clsFilesMonitor monitor;
        public FileMonitoringService()
        {
            InitializeComponent();
        }

        

        protected override void OnStart(string[] args)
        {
            string src = ConfigurationManager.AppSettings["SourceFolder"];
            string dist = ConfigurationManager.AppSettings["DestinationFolder"];
            string LogsPath = ConfigurationManager.AppSettings["LogFolder"];

            monitor = new clsFilesMonitor(src, dist, LogsPath);
            monitor.Log("Service started");
            monitor.StartMonitoring();
           
        }

        protected override void OnStop()
        {
            monitor.StopMonitoring();
            File.AppendAllText(Path.Combine(ConfigurationManager.AppSettings["LogFolder"], "FileMonitoringLog.txt"), $"[{DateTime.UtcNow}] Service stopped\n");

        }
    }
}

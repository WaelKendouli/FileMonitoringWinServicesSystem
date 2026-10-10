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

namespace WindowsServiceFileMonitoring
{
    public partial class FileMonitoringService : ServiceBase
    {
        public FileMonitoringService()
        {
            InitializeComponent();
        }

        

        protected override void OnStart(string[] args)
        {
            string src = ConfigurationManager.AppSettings["SourceFolder"];
            string dist = ConfigurationManager.AppSettings["DestinationFolder"];
            string LogsPath = ConfigurationManager.AppSettings["LogFolder"];

            clsFilesMonitor monitor = new clsFilesMonitor(src, dist, LogsPath);
            monitor.StartMonitoring();
           
        }

        protected override void OnStop()
        {
        }
    }
}

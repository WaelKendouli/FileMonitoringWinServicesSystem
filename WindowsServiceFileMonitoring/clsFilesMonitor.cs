using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsServiceFileMonitoring
{
    public class clsFilesMonitor
    {
        private FileSystemWatcher watcher;

        private string sourcePath { get; set; }

        private string destinationPath { get; set; }
        private string logsPath { get; set; }

        private string LogsFileName { get; set; } = "FileMonitoringLog.txt";
        public clsFilesMonitor(string source, string destination , string LogsPath , string LogFileName = "FileMonitoringLog.txt")
        {
            watcher = new FileSystemWatcher(source);
            sourcePath = source;
            destinationPath = destination;
            logsPath = LogsPath;
            watcher.Filter = "*.*";
            this.LogsFileName = LogFileName;
            watcher.EnableRaisingEvents = true;
           
        }


        private void Log(string message) {
            string FullLogPath = Path.Combine(this.logsPath , this.LogsFileName);

            if (!File.Exists(FullLogPath))
            {
                File.Create(FullLogPath).Dispose();
            }
            File.WriteAllText(FullLogPath, $"[{DateTime.UtcNow}] {message}");
        }


        private void OnFileAdded(object sender, FileSystemEventArgs e)
        {
            Log($"File was created at :{e.FullPath}");
            string sourceFilePath = this.sourcePath;
            string sourceFileName = Path.GetFileName(e.FullPath);
            string NewFileName = Guid.NewGuid().ToString() + Path.GetExtension(e.FullPath);
            try
            {
                File.Copy(Path.Combine(sourceFilePath, sourceFileName), Path.Combine(destinationPath, NewFileName), true);    
                    Log($"File {sourceFileName} copied to {NewFileName} successfully with New GUID [{NewFileName}].");
            }
            catch (Exception ex)
            {
               Log($"Error copying file {sourceFileName} to {NewFileName}: {ex.Message}");
            }
        }

        public void StartMonitoring()
        {
            watcher.Created += OnFileAdded;
        }

    }
}

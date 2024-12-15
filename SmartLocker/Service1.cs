using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Text;

namespace SmartLocker
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        private Dictionary<string, DateTime> appStartTimes = new Dictionary<string, DateTime>();
        private TimeSpan allowedUsageTime = TimeSpan.FromMinutes(30); // Example: 30 minutes

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("Parental Control Service Started");

            // Start a timer with a 10-second interval
            timer = new Timer();
            timer.Interval = 10000; // 10 seconds
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
            EventLog.WriteEntry("Parental Control Service Stopped");
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            // Close the notepad application
            CloseNotepad();
        }

        private void CloseNotepad()
        {
            Process[] processList = Process.GetProcessesByName("notepad");
            foreach (Process process in processList)
            {
                try
                {
                    process.Kill();
                    EventLog.WriteEntry($"Closed application: {process.ProcessName}");
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry($"Failed to close application: {process.ProcessName}. Error: {ex.Message}");
                }
            }
        }

        public string MonitorProcesses()
        {
            Process[] processList = Process.GetProcesses();
            StringBuilder processInfo = new StringBuilder();
            foreach (Process process in processList)
            {
                processInfo.AppendLine($"Process: {process.ProcessName} ID: {process.Id}");
            }
            return processInfo.ToString();
        }

        public string MonitorApplications()
        {
            Process[] processList = Process.GetProcesses();
            StringBuilder appInfo = new StringBuilder();
            foreach (Process process in processList)
            {
                if (!string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    appInfo.AppendLine($"Application: {process.ProcessName} Title: {process.MainWindowTitle} ID: {process.Id}");
                }
            }
            return appInfo.ToString();
        }

        private void BlockApplications(string[] blockedApps)
        {
            Process[] processList = Process.GetProcesses();
            foreach (Process process in processList)
            {
                if (blockedApps.Contains(process.ProcessName))
                {
                    try
                    {
                        process.Kill();
                        EventLog.WriteEntry($"Blocked application: {process.ProcessName}");
                    }
                    catch (Exception ex)
                    {
                        EventLog.WriteEntry($"Failed to block application: {process.ProcessName}. Error: {ex.Message}");
                    }
                }
            }
        }

        private void MonitorAndLimitUsage()
        {
            Process[] processList = Process.GetProcesses();
            foreach (Process process in processList)
            {
                if (!appStartTimes.ContainsKey(process.ProcessName))
                {
                    appStartTimes[process.ProcessName] = DateTime.Now;
                }
                else
                {
                    TimeSpan usageTime = DateTime.Now - appStartTimes[process.ProcessName];
                    if (usageTime > allowedUsageTime)
                    {
                        try
                        {
                            process.Kill();
                            EventLog.WriteEntry($"Terminated application due to time limit: {process.ProcessName}");
                            appStartTimes.Remove(process.ProcessName);
                        }
                        catch (Exception ex)
                        {
                            EventLog.WriteEntry($"Failed to terminate application: {process.ProcessName}. Error: {ex.Message}");
                        }
                    }
                }
            }
        }

        public void StartInteractive()
        {
            OnStart(null);
        }

        public void StopInteractive()
        {
            OnStop();
        }
    }
}
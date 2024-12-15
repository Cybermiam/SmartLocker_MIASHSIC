using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using System.Linq;
using System.Collections.Generic;
using System;

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

            timer = new Timer();
            timer.Interval = 60000; // Check every minute
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
            string[] blockedApps = { "notepad", "calc" }; // Example blocked applications
            MonitorProcesses();
            BlockApplications(blockedApps);
            MonitorAndLimitUsage();
        }

        private void MonitorProcesses()
        {
            Process[] processList = Process.GetProcesses();
            foreach (Process process in processList)
            {
                Console.WriteLine($"Process: {process.ProcessName} ID: {process.Id}");
            }
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
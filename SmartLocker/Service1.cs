﻿using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Security.Principal;
using System.Xml.Linq;
using System.Runtime.CompilerServices;

using Microsoft.Toolkit.Uwp.Notifications;


namespace SmartLocker
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        private String user;
        private EventLog eventLog;
        private DataService dataService = new DataService();

        public Service1()
        {
            InitializeComponent();


        }

        protected override void OnStart(string[] args)
        {
            try
            {
                this.user = WindowsIdentity.GetCurrent().Name;
                addSampleData();
                EventLog.WriteEntry("Parental Control Service Started");
                timer = new Timer();
                timer.Interval = 5000; // 1 minute
                timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
                timer.Start();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry($"Error in OnStart: {ex.Message}", EventLogEntryType.Error);
                throw;
            }
        }

        protected override void OnStop()
        {
            try
            {
                timer.Stop();
                EventLog.WriteEntry("Parental Control Service Stopped");
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry($"Error in OnStop: {ex.Message}", EventLogEntryType.Error);
            }
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            try
            {
                EventLog.WriteEntry("Monitoring processes...");
                ApplyTimeConstraints();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry($"Error in OnTimer: {ex.Message}", EventLogEntryType.Error);
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

        public List<string> MonitorApplications()
        {
            Process[] processList = Process.GetProcesses();
            List<string> appInfo = new List<string>();
            foreach (Process process in processList)
            {
                if (!string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    appInfo.Add(process.ProcessName);
                }
            }
            return appInfo;
        }

        public void addSampleData()
        {
            dataService.addSampleApps();
            dataService.createSampleConstraints();
        }

        public void StartInteractive()
        {
            OnStart(null);
        }

        public void StopInteractive()
        {
            OnStop();
        }

        private void ApplyTimeConstraints()
        {
            try
            {
                // Récupérer les contraintes horaires
                List<ContrainteHoraire> contraintesHoraires = dataService.getAllContraintesHoraire();
                // Récupérer les contraintes journalières
                List<ContrainteJour> contraintesJours = dataService.getAllContraintesJour();
                // Récupérer les contraintes hebdomadaires
                List<ContrainteSemaine> contraintesSemaines = dataService.getAllContraintesSemaine();

                // Récupérer les applications en cours d'exécution
                Process[] processList = Process.GetProcesses();

                foreach (Process process in processList)
                {
                    // Vérifier les contraintes horaires
                    foreach (var contrainte in contraintesHoraires)
                    {
                        if (process.ProcessName == dataService.getAppNameFromId(contrainte.AppId))
                        {
                            
                            if (contrainte.UsedTime >= contrainte.MaxTime)
                            {
                                try
                                {
                                    process.Kill();
                                    EventLog.WriteEntry($"Closed application due to hourly constraint: {process.ProcessName}");
                                }
                                catch (Exception ex)
                                {
                                    EventLog.WriteEntry($"Failed to close application: {process.ProcessName}. Error: {ex.Message}");
                                }
                            }
                            else
                            {
                                contrainte.UsedTime += 1; // Incrementer le temps utilisé
                            }
                        }
                    }

                    // Vérifier les contraintes journalières
                    foreach (var contrainte in contraintesJours)
                    {
                        if (process.ProcessName == dataService.getAppNameFromId(contrainte.AppId))
                        {
                            
                            if (contrainte.UsedTime >= contrainte.MaxTime)
                            {
                                try
                                {
                                    process.Kill();
                                    EventLog.WriteEntry($"Closed application due to daily constraint: {process.ProcessName}");
                                }
                                catch (Exception ex)
                                {
                                    EventLog.WriteEntry($"Failed to close application: {process.ProcessName}. Error: {ex.Message}");
                                }
                            }
                            else
                            {
                                contrainte.UsedTime += 1; // Incrementer le temps utilisé
                            }
                        }
                    }

                    // Vérifier les contraintes hebdomadaires
                    foreach (var contrainte in contraintesSemaines)
                    {
                        if (process.ProcessName == dataService.getAppNameFromId(contrainte.AppId))
                        {
                            int maxTime = 0;
                            switch (DateTime.Now.DayOfWeek)
                            {
                                case DayOfWeek.Monday:
                                    maxTime = contrainte.MondayTime;
                                    break;
                                case DayOfWeek.Tuesday:
                                    maxTime = contrainte.TuesdayTime;
                                    break;
                                case DayOfWeek.Wednesday:
                                    maxTime = contrainte.WednesdayTime;
                                    break;
                                case DayOfWeek.Thursday:
                                    maxTime = contrainte.ThursdayTime;
                                    break;
                                case DayOfWeek.Friday:
                                    maxTime = contrainte.FridayTime;
                                    break;
                                case DayOfWeek.Saturday:
                                    maxTime = contrainte.SaturdayTime;
                                    break;
                                case DayOfWeek.Sunday:
                                    maxTime = contrainte.SundayTime;
                                    break;
                            }

                            
                            if (contrainte.UsedTime >= maxTime)
                            {
                                try
                                {
                                    process.Kill();
                                    EventLog.WriteEntry($"Closed application due to weekly constraint: {process.ProcessName}");
                                }
                                catch (Exception ex)
                                {
                                    EventLog.WriteEntry($"Failed to close application: {process.ProcessName}. Error: {ex.Message}");
                                }
                            }
                            else
                            {
                                contrainte.UsedTime += 1; // Incrementer le temps utilisé
                            }
                        }
                    }
                }

                // Mettre à jour les contraintes dans la base de données
                foreach (var contrainte in contraintesHoraires)
                {
                    dataService.updateContrainteHoraire(contrainte);
                }
                foreach (var contrainte in contraintesJours)
                {
                    dataService.updateContrainteJour(contrainte);
                }
                foreach (var contrainte in contraintesSemaines)
                {
                    dataService.updateContrainteSemaine(contrainte);
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry($"Error in ApplyTimeConstraints: {ex.Message}", EventLogEntryType.Error);
            }
        }


        
    }
}
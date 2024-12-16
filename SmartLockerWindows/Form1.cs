using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Windows.Forms;
using SmartLocker;

namespace SmartLockerWindows
{
    public partial class Form1 : Form
    {
        private ServiceController? serviceController; // Declare as nullable
        private Service1 service1;
        private EventLog eventLog;

        public Form1()
        {
            InitializeComponent();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                serviceController = new ServiceController("SmartLocker");
            }
            else
            {
                MessageBox.Show("ServiceController is not supported on this operating system.");
            }
            service1 = new Service1(); // Create an instance of Service1
            eventLog = new EventLog();
            if (!EventLog.SourceExists("SmartLockerApp"))
            {
                EventLog.CreateEventSource("SmartLockerApp", "Application");
            }
            eventLog.Source = "SmartLockerApp";
            eventLog.Log = "Application";

            // Update label1 with application information
            label1.Text = service1.MonitorApplications();
        }

        private void btnStartService_Click(object sender, EventArgs e)
        {

            try
            {
                if (serviceController != null && serviceController.Status != ServiceControllerStatus.Running)
                {
                    service1.StartInteractive(); // Start the service interactively
                    MessageBox.Show("Service démarré avec succès.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du démarrage du service : {ex.Message}");
            }
        }

        private void btnStopService_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceController != null && serviceController.Status != ServiceControllerStatus.Stopped)
                {
                    service1.StopInteractive(); // Stop the service interactively
                    MessageBox.Show("Service arrêté avec succès.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'arrêt du service : {ex.Message}");
            }
        }
    }
}
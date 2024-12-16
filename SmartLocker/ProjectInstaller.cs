﻿using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

[RunInstaller(true)]
public partial class ProjectInstaller : Installer
{
    private ServiceProcessInstaller serviceProcessInstaller1;
    private ServiceInstaller serviceInstaller1;

    public ProjectInstaller()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
        this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();

        // 
        // serviceProcessInstaller1
        // 
        this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
        this.serviceProcessInstaller1.Password = null;
        this.serviceProcessInstaller1.Username = null;

        // 
        // serviceInstaller1
        // 
        this.serviceInstaller1.ServiceName = "SmartLocker";
        this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;

        // 
        // ProjectInstaller
        // 
        this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.serviceInstaller1});
    }
}
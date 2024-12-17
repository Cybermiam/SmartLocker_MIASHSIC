using System;
using System.Windows.Forms;
using SmartLockerData;

namespace SmartLockerWindows
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DataService dataService = new DataService();
            List<Utilisateur> users = dataService.ObtenirTousLesUtilisateurs();
            String usersString = "";
            for (int i = 0; i < users.Count; i++)
            {
                usersString += users[i].Name + " ";
            }
            MessageBox.Show("utilisateurs avant suppression : "+ usersString);

            
            users = dataService.ObtenirTousLesUtilisateurs();
            usersString = "";
            for (int i = 0; i < users.Count; i++)
            {
                usersString += users[i].Name + " ";
            }
            MessageBox.Show("utilisateurs apr�s suppression : " + usersString);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
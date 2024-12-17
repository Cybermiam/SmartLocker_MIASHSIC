using System;
using System.Windows.Forms;


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
            dataService.AjouterUtilisateur("admin", true);
            dataService.AjouterUtilisateur("user", false);
            List<Utilisateur> users = dataService.ObtenirTousLesUtilisateurs();
            String usersString = "";
            for (int i = 0; i < users.Count; i++)
            {
                usersString += users[i].Name + " ";
            }
            MessageBox.Show("utilisateurs avant suppression : "+ usersString);

            dataService.deleteAll();
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
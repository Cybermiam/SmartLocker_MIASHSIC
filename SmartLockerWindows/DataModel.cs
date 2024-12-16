using System;
using System.Windows.Forms;

namespace SmartLockerWindows
{
    public partial class DataModel
    {
        private readonly DataService _dataService;

        public DataModel()
        {
            _dataService = new DataService();
        }

        private void AjouterUtilisateurButton_Click(object sender, EventArgs e)
        {
            _dataService.AjouterUtilisateur("John Doe", true);
            MessageBox.Show("Utilisateur ajouté avec succès !");
        }

        private void AfficherTousLesUtilisateursButton_Click(object sender, EventArgs e)
        {
            var utilisateurs = _dataService.ObtenirTousLesUtilisateurs();
            foreach (var utilisateur in utilisateurs)
            {
                MessageBox.Show($"ID: {utilisateur.Id}, Name: {utilisateur.Name}, Role: {utilisateur.Role}");
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace SmartLockerWindows
{
    public class DataService()
    {
        public void AjouterUtilisateur(string name, bool role)
        {
            using (var context = new AppDbContext())
            {
                var utilisateur = new Utilisateur { Name = name, Role = role };
                context.Utilisateurs.Add(utilisateur);
                context.SaveChanges();
            }
        }

        public Utilisateur ObtenirUtilisateur(int id)
        {
            using (var context = new AppDbContext())
            {
                return context.Utilisateurs.FirstOrDefault(u => u.Id == id);
            }
        }

        public List<Utilisateur> ObtenirTousLesUtilisateurs()
        {
            using (var context = new AppDbContext())
            {
                return context.Utilisateurs.ToList();
            }
        }
    }
}
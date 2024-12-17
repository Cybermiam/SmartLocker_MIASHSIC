using System.Collections.Generic;
using System.Linq;

namespace SmartLockerWindows
{
    public class DataService
    {
        // Fonctions pour la classe Utilisateur
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

        public void SupprimerUtilisateur(int id)
        {
            using (var context = new AppDbContext())
            {
                var utilisateur = context.Utilisateurs.FirstOrDefault(u => u.Id == id);
                if (utilisateur != null)
                {
                    context.Utilisateurs.Remove(utilisateur);
                    context.SaveChanges();
                }
            }
        }

        // Fonctions pour la classe App
        public void AjouterApp(string name)
        {
            using (var context = new AppDbContext())
            {
                var app = new App { Name = name };
                context.Apps.Add(app);
                context.SaveChanges();
            }
        }

        public App ObtenirApp(int id)
        {
            using (var context = new AppDbContext())
            {
                return context.Apps.FirstOrDefault(a => a.Id == id);
            }
        }

        public List<App> ObtenirToutesLesApps()
        {
            using (var context = new AppDbContext())
            {
                return context.Apps.ToList();
            }
        }

        public void SupprimerApp(int id)
        {
            using (var context = new AppDbContext())
            {
                var app = context.Apps.FirstOrDefault(a => a.Id == id);
                if (app != null)
                {
                    context.Apps.Remove(app);
                    context.SaveChanges();
                }
            }
        }

        // Fonctions pour la classe StatistiquesUtilisation
        public void AjouterStatistiquesUtilisation(int utilisateurId, int appId, DateTime date, int duree)
        {
            using (var context = new AppDbContext())
            {
                var stats = new StatistiquesUtilisation { UserId = utilisateurId, AppId = appId, Date = date, Time = duree };
                context.StatistiquesUtilisations.Add(stats);
                context.SaveChanges();
            }
        }

        public StatistiquesUtilisation ObtenirStatistiquesUtilisation(int id)
        {
            using (var context = new AppDbContext())
            {
                return context.StatistiquesUtilisations.FirstOrDefault(s => s.Id == id);
            }
        }

        public List<StatistiquesUtilisation> ObtenirToutesLesStatistiquesUtilisation()
        {
            using (var context = new AppDbContext())
            {
                return context.StatistiquesUtilisations.ToList();
            }
        }

        public void SupprimerStatistiquesUtilisation(int id)
        {
            using (var context = new AppDbContext())
            {
                var stats = context.StatistiquesUtilisations.FirstOrDefault(s => s.Id == id);
                if (stats != null)
                {
                    context.StatistiquesUtilisations.Remove(stats);
                    context.SaveChanges();
                }
            }
        }

        // Fonctions pour la classe ContrainteHoraire
        public void AjouterContrainteHoraire(ContrainteHoraire ch)
        {
            using (var context = new AppDbContext())
            {
                var contrainte = ch;
                context.ContrainteHoraires.Add(contrainte);
                context.SaveChanges();
            }
        }

        public ContrainteHoraire ObtenirContrainteHoraire(int id)
        {
            using (var context = new AppDbContext())
            {
                return context.ContrainteHoraires.FirstOrDefault(c => c.Id == id);
            }
        }

        public List<ContrainteHoraire> ObtenirToutesLesContraintesHoraires()
        {
            using (var context = new AppDbContext())
            {
                return context.ContrainteHoraires.ToList();
            }
        }

        public void SupprimerContrainteHoraire(int id)
        {
            using (var context = new AppDbContext())
            {
                var contrainte = context.ContrainteHoraires.FirstOrDefault(c => c.Id == id);
                if (contrainte != null)
                {
                    context.ContrainteHoraires.Remove(contrainte);
                    context.SaveChanges();
                }
            }
        }

        // Fonctions pour la classe ContrainteJour
        public void AjouterContrainteJour(ContrainteJour cr)
        {
            using (var context = new AppDbContext())
            {
                var contrainte = cr;
                context.ContrainteJours.Add(contrainte);
                context.SaveChanges();
            }
        }

        public ContrainteJour ObtenirContrainteJour(int id)
        {
            using (var context = new AppDbContext())
            {
                return context.ContrainteJours.FirstOrDefault(c => c.Id == id);
            }
        }

        public List<ContrainteJour> ObtenirToutesLesContraintesJours()
        {
            using (var context = new AppDbContext())
            {
                return context.ContrainteJours.ToList();
            }
        }

        public void SupprimerContrainteJour(int id)
        {
            using (var context = new AppDbContext())
            {
                var contrainte = context.ContrainteJours.FirstOrDefault(c => c.Id == id);
                if (contrainte != null)
                {
                    context.ContrainteJours.Remove(contrainte);
                    context.SaveChanges();
                }
            }
        }

        // Fonctions pour la classe ContrainteSemaine
        public void AjouterContrainteSemaine(ContrainteSemaine cs)
        {
            using (var context = new AppDbContext())
            {
                var contrainte = cs;
                context.ContrainteSemaines.Add(contrainte);
                context.SaveChanges();
            }
        }

        public ContrainteSemaine ObtenirContrainteSemaine(int id)
        {
            using (var context = new AppDbContext())
            {
                return context.ContrainteSemaines.FirstOrDefault(c => c.Id == id);
            }
        }

        public List<ContrainteSemaine> ObtenirToutesLesContraintesSemaines()
        {
            using (var context = new AppDbContext())
            {
                return context.ContrainteSemaines.ToList();
            }
        }

        public void SupprimerContrainteSemaine(int id)
        {
            using (var context = new AppDbContext())
            {
                var contrainte = context.ContrainteSemaines.FirstOrDefault(c => c.Id == id);
                if (contrainte != null)
                {
                    context.ContrainteSemaines.Remove(contrainte);
                    context.SaveChanges();
                }
            }
        }
        public void deleteAll()
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }

    
}

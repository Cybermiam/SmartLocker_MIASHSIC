using Microsoft.EntityFrameworkCore;

namespace SmartLockerData
{
    public class AppDbContext : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<App> Apps { get; set; }
        public DbSet<StatistiquesUtilisation> StatistiquesUtilisations { get; set; }
        public DbSet<ContrainteJour> ContrainteJours { get; set; }
        public DbSet<ContrainteHoraire> ContrainteHoraires { get; set; }
        public DbSet<ContrainteSemaine> ContrainteSemaines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=CYBERMIAM;Database=SmartLockerDB; Integrated Security=True; TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utilisateur>().HasKey(u => u.Id);
            modelBuilder.Entity<App>().HasKey(u => u.Id);
            modelBuilder.Entity<StatistiquesUtilisation>().HasKey(u => u.Id);
            modelBuilder.Entity<ContrainteJour>().HasKey(u => u.Id);
            modelBuilder.Entity<ContrainteHoraire>().HasKey(u => u.Id);
            modelBuilder.Entity<ContrainteSemaine>().HasKey(u => u.Id);

        }
    }
}
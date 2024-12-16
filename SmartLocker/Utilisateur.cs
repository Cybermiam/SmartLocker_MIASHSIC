using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLockerWindows
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Role { get; set; }
        public ICollection<StatistiquesUtilisation> StatistiquesUtilisations { get; set; }
        public ICollection<ContrainteJour> ContrainteJours { get; set; }
        public ICollection<ContrainteHoraire> ContrainteHoraires { get; set; }
        public ICollection<ContrainteSemaine> ContrainteSemaines { get; set; }
    }
}

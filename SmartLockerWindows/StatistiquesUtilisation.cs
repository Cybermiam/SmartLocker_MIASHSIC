using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLockerWindows
{
    public class StatistiquesUtilisation
    {
        public int Id { get; set; }
        public int Time { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public int AppId { get; set; }
        public App Application { get; set; }
    }
}

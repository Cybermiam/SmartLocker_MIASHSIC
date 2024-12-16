using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLockerWindows
{
    public class ContrainteSemaine
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public int AppId { get; set; }
        public Application Application { get; set; }
        public int MondayTime { get; set; }
        public int TuesdayTime { get; set; }
        public int WednesdayTime { get; set; }
        public int ThursdayTime { get; set; }
        public int FridayTime { get; set; }
        public int SaturdayTime { get; set; }
        public int SundayTime { get; set; }
        public int UsedTime { get; set; }
    }
}

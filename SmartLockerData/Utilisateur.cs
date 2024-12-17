using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLockerData
{
    
    public class Utilisateur
    {
        
        public int Id { get; set; } 
        public required string Name { get; set; }
        public bool Role { get; set; }
        
    }
}

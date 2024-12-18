﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLocker
{
    public class ContrainteHoraire
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AppId { get; set; }
        public int MaxTime { get; set; }
        public int BlockTime { get; set; }
        public int UsedTime { get; set; }

        // Propriétés pour les valeurs initiales
        public int InitialBlockTime { get; set; } = 1000000;
        public int InitialUsedTime { get; set; } = 1000000;
    }
}

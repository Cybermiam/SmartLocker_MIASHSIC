﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLockerData
{
    public class ContrainteJour
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AppId { get; set; }
        public int MaxTime { get; set; }
        public int UsedTime { get; set; }
    }
}

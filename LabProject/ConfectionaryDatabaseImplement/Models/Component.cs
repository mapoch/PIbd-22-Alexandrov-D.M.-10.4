﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfectionaryDatabaseImplement.Models
{
    public class Component
    {
        public int Id { get; set; }
        
        [Required]
        public string ComponentName { get; set; }
        
        [ForeignKey("ComponentId")]
        public virtual List<PastryComponent> PastryComponents { get; set; }
    }
}
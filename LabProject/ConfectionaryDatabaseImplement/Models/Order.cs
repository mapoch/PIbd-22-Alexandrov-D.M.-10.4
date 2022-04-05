﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConfectionaryContracts.Enums;

namespace ConfectionaryDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int PastryId { get; set; }
        public int? ClientId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public decimal Sum { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }

        public virtual Pastry Pastry { get; set; }

        public virtual Client Client { get; set; }
    }
}

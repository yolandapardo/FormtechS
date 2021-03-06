﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FormtechS.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        [Index(IsUnique =true)]
        [Display(Name = "Company")]
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        [StringLength(15)]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(80)]
        public string Address { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
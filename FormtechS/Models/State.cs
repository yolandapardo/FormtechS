using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FormtechS.Models
{
    public class State
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="State")]
        [StringLength(50)]
        public string Name { get; set; }
        public virtual ICollection<County> County { get; set; }
    }
}
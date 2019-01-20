using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FormtechS.Models
{
    public class TypeOfSurvey
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        [Display(Name = "Type of Survey")]
        public string Type { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FormtechS.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "City")]
        public string Name { get; set; }
        public int CountyId { get; set; }
        public virtual County County { get; set; }
        public virtual ICollection<Community> Communities { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FormtechS.Models
{
    public class County
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "County")]
        [StringLength(50)]
        public string Name { get; set; }
        public int StateId { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}
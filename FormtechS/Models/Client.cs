using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FormtechS.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        [Index("IX_NameAndPhone", 1,IsUnique = true)]
        [StringLength(80)]
        [Display(Name = "Client Name")]
        public string Name { get; set; }
        [Index("IX_NameAndPhone",2,IsUnique =true)]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15)]
        public string Phone { get; set; }
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
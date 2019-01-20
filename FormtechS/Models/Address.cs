using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FormtechS.Models
{
    public class Address
    {

        public int Id { get; set; }
        [Required]
        [Index("IX_NameAndZipCode", 1, IsUnique = true)]
        [StringLength(80)]
        [Display(Name = "Address")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Zip Code")]
        [DataType(DataType.PostalCode)]
        [Index("IX_NameAndZipCode", 2, IsUnique = true)]
        public int ZipCode { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public int CommunityId { get; set; }
        public virtual Community Community { get; set; }
    }
}